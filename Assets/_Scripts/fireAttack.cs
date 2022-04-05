using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireAttack : MonoBehaviour
{
    protected BoxCollider2D boxCollider;

    protected List<Collider2D> hits = new List<Collider2D>();
    protected ContactFilter2D contact = new ContactFilter2D();

    public float lifetime = 1f;
    public int fireDamage = 1;

    protected void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        contact.layerMask = LayerMask.GetMask("Enemy");
    }

    
}
