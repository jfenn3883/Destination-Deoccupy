using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkMage : Enemy
{
    /*public GameObject blastTemplate;
    protected float lastBlast; //to store the time of the last hit
    protected float lastBlastTimer = 3f; //the cooldown between hits from the hero to the enemy
    protected bool isBlast = true; //to know if the enemy is in blast mode

    protected override void Start()
    {
        base.Start();
        lastBlast = Time.time; //getting the enemies box collider 
    }
    protected override void FixedUpdate()
    {
      *//*
        if (isBlast)
        {
            if (Time.time - lastBlast > lastBlastTimer)
            {
                lastBlast = Time.time;
                GameObject blast = Instantiate(blastTemplate, transform.position, Quaternion.identity);
                blast.GetComponent<Projectile>().target = GameObject.Find("player_0").transform;
                isBlast = false;
            }
        }   
        *//*

       //Reset MoveDelta
       moveDelta = (-target.position + transform.position); //Setting the enemy to move away from the player
       if(moveDelta.magnitude > radius) moveDelta = new Vector3(0,0,0); //if the enemy is far from the player don't chase them 
       moveDelta = moveDelta.normalized; //normalize the vector 


       //Swap Sprite direction, when moving
       if(moveDelta.x > 0)
       {
           transform.localScale = Vector3.one;
       }
       else if( moveDelta.x < 0)
       {
            transform.localScale = new Vector3(-1,1,0);
       }

        //if enemy collides with wall/ in y-direction
       hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Player", "Blocking","Enemy"));
       if(hit.collider == null) //if there is nothing in the player's way in the y-direction
       {
           //move object in y
         transform.Translate(0,moveDelta.y * Time.deltaTime * speed,0);
       }
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Weapons"));
        // if enemy collides with player in y-direction
          if(hit.collider != null && !isHit){
           EnemyHit();
        }
       //if enemy collides with wall/player/enemy in x- direction
       hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x,0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Player", "Blocking","Enemy"));
       if(hit.collider == null) //if there is nothing in the player's way in the y-direction
       {
           //move object in x
         transform.Translate(moveDelta.x * Time.deltaTime * speed,0,0);
       }
       //if enemy collides with player in x-direction
         hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x,0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Weapons"));
        // if enemy collides with player and the enemy is not currently hit
        if(hit.collider != null && !isHit){
            EnemyHit();
        }
        //Checking if it has been long enough between hits
        //if the time between the last hit and the current time is greater than the cooldown
        if(Time.time - lastHit > lastHitTimer){ 
          
          isHit = false; //set the enemy to be able to be hit
        }
        //Set the mage to shoot a blast
        if(Time.time - lastBlast > lastBlastTimer){ shootBlast();}
    }
        void shootBlast(){
            lastBlast = Time.time; //setting the time of the last blast
            GameObject blast = Instantiate(blastTemplate) as GameObject; //creating a blast
            blast.transform.position = transform.position; //setting the blast to the mage's position
            blast.transform.Translate(0,-1,0); //translating the blast to the right
            //give the blast a target
            blast.GetComponent<Projectile>().target = GameObject.Find("player_0").transform;
            //give the blast a force toward the target 
            blast.GetComponent<Rigidbody2D>().AddForce(new Vector2(1,1) * blast.GetComponent<Projectile>().speed, ForceMode2D.Impulse);
            //give the blast a force
            //blast.GetComponent<Rigidbody2D>().AddForce(new Vector2(1,1), ForceMode2D.Impulse);
        }*/
}