using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireAttack : MonoBehaviour
{
    protected BoxCollider2D box;

    protected List<Collider2D> hits = new List<Collider2D>();
    protected ContactFilter2D contact = new ContactFilter2D();

    public float lifetime = 1f;
    public int fireDamage = 1;
    protected float dis;

    protected void Start()
    {
        box = GetComponent<BoxCollider2D>();
        contact.layerMask = LayerMask.GetMask("Enemy");
        dis = Time.time + lifetime;
    }

    protected void FixedUpdate()
    {
        hits.Clear();
        Physics2D.OverlapCollider(box, contact, hits);

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.CompareTag("Enemy")) hit.gameObject.GetComponent<Enemy>().damage(fireDamage);
        }

        if (Time.time > dis) Destroy(this.gameObject);
    }
}
