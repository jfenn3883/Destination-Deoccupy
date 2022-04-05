using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : Collidable
{
    

public class Weapon : Collidable  
{
  //Damage and knockback
   public int damagePoint = 1;
   public float pushForce = 0.5f;

   //Upgrade (Note: needs to be changed)
   public int weaponLevel = 0;
   private SpriteRenderer SpriteRenderer;

   //Sword swing
    public Animator anim;
    private float coolDown = 0.5f;
    private float lastSwing;
    

   protected override void Start()
   {
       base.Start();
       SpriteRenderer = GetComponent<SpriteRenderer>(); //related to old upgrade can be kept and implemented
       anim = GetComponent<Animator>(); 
   }
   protected override void Update()
   {
       base.Update();
       
    

       if(Input.GetKeyDown(KeyCode.Space)) //  Mouse0
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
               damageAmount = damagePoint,
              Origin = transform.position,
              pushForce = pushForce
            };

         coll.SendMessage("ReceiveDamage",dmg); 
       }
   }
    private void Swing()
   {
     if(Input.GetKeyDown(KeyCode.RightArrow))   {
         anim.Play("swordSwing");
     }
     if(Input.GetKeyDown(KeyCode.LeftArrow))   {
         anim.Play("swordSwing");
     }
     if(Input.GetKeyDown(KeyCode.UpArrow))   {
         anim.Play("swordUp");
     }
   } 

}

}
