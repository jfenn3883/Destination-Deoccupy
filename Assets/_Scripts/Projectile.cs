using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Projectile : MonoBehaviour
{
  
        public Transform target = GameObject.Find("player_0").transform;// the hero's transform for the enemy to chase
        protected BoxCollider2D boxCollider; //for the enemy's box collider 
        protected Vector3 moveDelta; //the enemy's direction
        protected RaycastHit2D hit; //to know if the projectile has hit
        public float speed = 2f; //the projectile's speed
  
    protected virtual void Start()
    {
         boxCollider = GetComponent<BoxCollider2D>(); //getting the projectile's box collider 
         //Reset the projectile direction
         moveDelta = (target.position - transform.position); //Setting the projectile to move toward the player's initial location
         moveDelta = moveDelta.normalized; //normalize the vector 
    }


    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
    
       //if enemy collides with wall/ in y-direction
       hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Player", "Blocking"));
       if(hit.collider == null) //if there is nothing in the player's way in the y-direction
       {
           //move object in y
         transform.Translate(0,moveDelta.y * Time.deltaTime * speed,0);
       }
       if(hit.collider != null){
           Destroy(this.gameObject); // destroy the projectile if it hits the player or a wall
       }
        //if enemy collides with wall/player/enemy in x- direction
       hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x,0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Player", "Blocking"));
       if(hit.collider == null) //if there is nothing in the player's way in the y-direction
       {
           //move object in x
         transform.Translate(moveDelta.x * Time.deltaTime * speed,0,0);
       }
       if(hit.collider != null){
           Destroy(this.gameObject); //destroy the projectile if it hits the player or a wall
       }
    }
}
