using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Player
{
    protected bool isFire = true;
    public float attackCooldown = 1.5f;
    public float attackDistance = 5f;

    public GameObject fire;

    protected ContactFilter2D contact = new ContactFilter2D();
    protected List<RaycastHit2D> raycast = new List<RaycastHit2D>();

    protected override void Start()
    {
        base.Start();

        contact.layerMask = LayerMask.GetMask("Enemy");
        fireAttack(0);
    }

    protected void fireAttack(int dir)
    {
        if(dir == 0)
        {
            for(int i = 1; i < 6; i++)
            {
                Instantiate(fire, new Vector2(this.transform.position.x + i, this.transform.position.y), new Quaternion());
            }
        }
    }

    protected void iceAttack(int dir)
    {
        raycast.Clear();

        if (dir == 0) Physics2D.Raycast(boxCollider.bounds.center, new Vector2(1, 0), contact, raycast, attackDistance);
        else if (dir == 1) Physics2D.Raycast(boxCollider.bounds.center, new Vector2(0, 1), contact, raycast, attackDistance);
        else if (dir == 2) Physics2D.Raycast(boxCollider.bounds.center, new Vector2(-1, 0), contact, raycast, attackDistance);
        else if (dir == 3) Physics2D.Raycast(boxCollider.bounds.center, new Vector2(0, -1), contact, raycast, attackDistance);


        if (raycast.Count != 0)
        {
            foreach (RaycastHit2D hit in raycast)
            {
                if (hit.collider != null && hit.collider.gameObject.tag == "Enemy") hit.collider.gameObject.GetComponent<Enemy>().slow();
            }
        }
    }
}
