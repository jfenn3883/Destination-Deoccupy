using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected BoxCollider2D box;
    protected Animator anim;

    //Damage and knockback
    public int attackDamage = 1;
    public float pushForce = 1;

    //Sword swing
    public float attackCooldown = 1f;
    protected float nextAttack;
    public float animationDuration; // must always be less than attack cooldown

    // private vars
    protected List<Collider2D> hits = new List<Collider2D>();
    protected ContactFilter2D contact = new ContactFilter2D();

    protected void Start()
    {
        box = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        contact.layerMask = LayerMask.GetMask("Enemy");
    }

    protected void FixedUpdate()
    {
        hits.Clear();

        Physics2D.GetContacts(box, contact, hits);

        if (hits.Count != 0)
        {
            foreach (Collider2D coll in hits)
            {
                if (coll.gameObject.tag == "Enemy" && Time.time < nextAttack - (attackCooldown - animationDuration)) coll.gameObject.GetComponent<Enemy>().damage(attackDamage);
            }
        }
    }

    public void attack(int dir)
   {
        if (Time.time > nextAttack)
        {
            if (dir == 0 || dir == 2)
            {
                anim.Play("swordSwing");
            }
            else if (dir == 1)
            {
                anim.Play("swordUp");
            }
            else if (dir == 3)
            {
                anim.Play("swordDown");
            }
            nextAttack = Time.time + attackCooldown;
        }

    }


}
