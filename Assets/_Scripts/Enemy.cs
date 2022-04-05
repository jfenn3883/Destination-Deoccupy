using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target; // the hero's transform for the enemy to chase
    protected BoxCollider2D boxCollider; //for the enemy's box collider 
    protected Vector2 moveDelta; //the enemy's direction
    protected RaycastHit2D hit; //to know if the enemy has been hit
    protected int health = 1; //the enemy's health
    protected int damage = 1; //the amount of damage the player deals to the enemy
    public float speed = 0.5f; //the enemy's speed
    protected float radius = 4f; //within this distance the enemy will chase
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
       if(moveDelta.magnitude > radius) moveDelta = new Vector2(0,0); //if the enemy is far from the player don't chase them 
       moveDelta = moveDelta.normalized; //normalize the vector 


       //Swap Sprite direction, when moving
       if(moveDelta.x > 0)
       {
           transform.localScale = Vector2.one;
       }
       else if( moveDelta.x < 0)
       {
            transform.localScale = new Vector2(-1,1);
       }

        // check if we can move where we want
        hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.size, 0, new Vector2(moveDelta.x, moveDelta.y), Mathf.Abs(moveDelta.magnitude * Time.deltaTime * speed), LayerMask.GetMask("Enemy", "Blocking", "Player"));
        if (hit.collider == null) transform.Translate(moveDelta * Time.deltaTime * speed);

   }
}