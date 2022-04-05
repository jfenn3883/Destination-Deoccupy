using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable  
{
    //Damage and knockback
    public int damage;
    public float pushForce;

    //Sword swing
    private float coolDown;
    private float lastSwing;

   protected override void Start()
   {
       base.Start();
   }
   protected override void Update()
   {
       base.Update();

       if(Input.GetKeyDown(KeyCode.Mouse0))
       {
           if(Time.time - lastSwing > coolDown)
           {
               lastSwing = Time.time;
               Swing();
           }
       }
   }
   protected override void onCollide(Collider2D coll)
   {
       if(coll.tag == "Fighter")
       {
           if(coll.name == "Player") 
            return;
            

            Damage dmg = new Damage
            {
               damageAmount = damage,
                Origin = transform.position,
                pushForce = pushForce
            };

            coll.SendMessage("ReceiveDamage",dmg); 
       }
   }
   private void Swing()
   {
       
   } 
}
