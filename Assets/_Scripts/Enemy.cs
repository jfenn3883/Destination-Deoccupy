using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  public Transform target;// the hero's transform for the enemy to chase
    protected BoxCollider2D boxCollider; //for the enemy's box collider 
    protected Vector3 moveDelta; //the enemy's direction
    protected RaycastHit2D hit; //to know if the enemy has been hit
    protected int health = 1; //the enemy's health
    protected int damage = 1; //the amount of damage the player deals to the enemy
    public float speed = 0.5f; //the enemy's speed
    protected float radius = 200f; //within this distance the enemy will chase
    protected float lastHit; //to store the time of the last hit
    protected float lastHitTimer = 1f; //the cooldown between hits from the hero to the enemy
    protected bool isHit = false; //a boolean to store if the enemy is currently hit



   protected virtual void Start()
   {
       boxCollider = GetComponent<BoxCollider2D>(); //getting the enemies box collider 
   }
   protected virtual void FixedUpdate()
   {
       //Reset MoveDelta
       moveDelta = (target.position - transform.position); //Setting the enemy to move toward the player
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
       hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Player", "Blocking","Actor"));
       if(hit.collider == null) //if there is nothing in the player's way in the y-direction
       {
           //move object in y
         transform.Translate(0,moveDelta.y * Time.deltaTime * speed,0);
       }
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Player"));
        // if enemy collides with player in y-direction
          if(hit.collider != null && !isHit){
           EnemyHit();
        }
       //if enemy collides with wall/player/enemy in x- direction
       hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x,0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Player", "Blocking","Actor"));
       if(hit.collider == null) //if there is nothing in the player's way in the y-direction
       {
           //move object in x
         transform.Translate(moveDelta.x * Time.deltaTime * speed,0,0);
       }
       //if enemy collides with player in x-direction
         hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x,0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Player"));
        // if enemy collides with player and the enemy is not currently hit
        if(hit.collider != null && !isHit){
            EnemyHit();
        }
        //Checking if it has been long enough between hits
        //if the time between the last hit and the current time is greater than the cooldown
        if(Time.time - lastHit > lastHitTimer){ 
          isHit = false; //set the enemy to be able to be hit
        }

   }
protected virtual void EnemyHit(){
    isHit = true; //setting the boolean variable to show that the enemy has just been hit
    lastHit = Time.time; //saving the time the last hit occured
    health -= damage; //damaging the enemy
    if(health <= 0){ //if the enemy is out of health
        Destroy(this.gameObject); //destroy the enemy 
    }
}  
}
