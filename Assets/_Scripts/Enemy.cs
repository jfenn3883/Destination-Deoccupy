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
    protected float radius = 2f; //within this distance the enemy will chase
    protected float lastHit; //to store the time of the last hit
    protected float lastHitTimer = 1f; //the cooldown between hits from the hero to the enemy
    protected bool isHit = false; //a boolean to store if the enemy is currently hit
    protected Vector3 lastPos;



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

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, moveDelta.y), Mathf.Abs(moveDelta.magnitude * Time.deltaTime * speed), LayerMask.GetMask("Enemy", "Blocking", "Player"));
        if (hit.collider == null)
        {
            lastPos = transform.position;
            //move object in x
            transform.Translate(moveDelta.x * Time.deltaTime * speed, moveDelta.y * Time.deltaTime * speed, 0);
        }

   }

    void OnCollisionStay2D(Collision2D collision)
    {
        print("fuck");
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy") transform.position = lastPos;
    }


    protected virtual void EnemyHit(){
    isHit = true; //setting the boolean variable to show that the enemy has just been hit
    lastHit = Time.time; //saving the time the last hit occured
    health -= damage; //damaging the enemy
    if(health <= 0){ //if the enemy is out of health
        //Destroy(this.gameObject); //destroy the enemy 
    }
}

    
}
