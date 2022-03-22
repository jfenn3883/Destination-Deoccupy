using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    protected int health = 1;
    protected int damage = 1;
    public float speed = 0.5f;
    protected float radius = 200f;


   protected virtual void Start()
   {
       boxCollider = GetComponent<BoxCollider2D>();
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
          if(hit.collider != null){
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
        // if enemy collides with player
        if(hit.collider != null){
            EnemyHit();
        }
 
   }
protected virtual void EnemyHit(){
    health -= damage;
    if(health <= 0){
        Destroy(this.gameObject);
    }
}
}
