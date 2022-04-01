using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brawler : Player_1
{
    //Charge Variables
    private float chargeCoolDown = 2f; // set as 2 seonds for development, needs to be set as 20 seconds for actual gameplay
    private float lastCharge;
    private float lastChargeTimer = 0.5f;
    private bool isCharging = false;
    private float chargeSpeed; 
    private int chargeDamage;

    
    protected override void Start() {
        base.Start();
        speed = 1f;
        chargeSpeed = speed*2;
        chargeDamage = damage*2;
        
   }     
    protected override void Update() {
        base.Update();   
    }
    protected override void FixedUpdate() {
        base.FixedUpdate();
        move(speed);
       if(inputs["space"] == 1 && Time.time - lastCharge > chargeCoolDown) //if statement chech charge duration, if space button is press   
       {
         isCharging = true; 
         lastCharge = Time.time;
         Charge(chargeSpeed, chargeDamage);
       } else if(Time.time - lastCharge  < lastChargeTimer) {
         Charge(chargeSpeed, chargeDamage);
       } else {
         isCharging = false;
       }
    }

    public void Charge(float chargeSpeed, int chargeDamage){ // charge method, doubles speed and damage , moves player in the direction last used.
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Enemy", "Blocking"));
       if(hit.collider == null &&  isCharging == true)
       {
           //move object in y
         transform.Translate(0,moveDelta.y * Time.deltaTime*chargeSpeed,0);
        
         
       }
       hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Enemy")); // check if colliding with enemy only 
        // if enemy collides with player in y-direction
          if(hit.collider != null && !isHit){
          dealDmg(enemy, chargeDamage);
        }
       hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x,0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Enemy", "Blocking"));
       if(hit.collider == null && isCharging == true)
       {
           //move object in x
         transform.Translate(moveDelta.x * Time.deltaTime*chargeSpeed,0,0); 
        
       }   
       //if enemy collides with player in x-direction
         hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x,0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Enemy"));
        // if enemy collides with player in x direction 
        if(hit.collider != null && !isHit){
            dealDmg(enemy, chargeDamage);
        }
   }
    



}
