using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    //public field
    public int hitpoint = 10;
    public int maxHitpoint = 10;
    public float pushRecoverySpeed = 0.2f;

    //Imunity
    protected float immuneTime = 1.0f;
    protected float lastImmune;

    //push
    protected Vector3 pushDirection;

    // all with Fighter tag will recieve damage and die 
    protected virtual void ReceiveDamage(Damage dmg)
    {
        if(Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hitpoint -=dmg.damageAmount;
            pushDirection = (transform.position - dmg.Origin).normalized * dmg.pushForce;

            if(hitpoint <= 0)
           {
                hitpoint = 0;
                Death();
            }
        }
    }
    public void Death()
    {
        Destroy(this.gameObject);
    }
}
