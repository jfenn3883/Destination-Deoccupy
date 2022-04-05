using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brawler : Player
{
    // Charge Variables
    private float chargeCoolDown = 2f; // set as 2 seonds for development, needs to be set as 20 seconds for actual gameplay
    private float lastCharge;
    private float lastChargeTimer = 0.5f;
    private bool isCharging = false;
    private float chargeSpeed; 
    private int chargeDamage;
    
    protected override void Start() {
        base.Start();
   
        chargeSpeed = speed * 2;
        chargeDamage = weapon.damage * 2;
  
   }   
    
    protected override void Update() {
        if (!isCharging)
        {
            inputs = GetInputs();
            moveDelta = new Vector3(inputs["x"], inputs["y"]);
        } 
    }

    protected override void FixedUpdate() {
        base.FixedUpdate();
        
        if(inputs["space"] == 1 && Time.time - lastCharge > chargeCoolDown) //if statement chech charge duration, if space button is press   
       {
         isCharging = true; 
         lastCharge = Time.time;
         Charge();
       } else if(inputs["space"] == 1 && Time.time - lastCharge  < lastChargeTimer) {
         Charge();
       } else {
         isCharging = false;
       }
    }



    public void Charge(){ // charge method, doubles speed and damage , moves player in the direction last used.
        move();
        // if deal dmg double
     }

}
