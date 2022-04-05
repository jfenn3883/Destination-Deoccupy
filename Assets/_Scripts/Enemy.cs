using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // public vars
    public Transform target; // the hero's transform for the enemy to chase
    public int health = 1; //the enemy's health
    public int attackDamage = 1; //the amount of damage the player deals to the enemy
    public float topSpeed = 0.75f; //the enemy's speed
    public float radius = 4f; //within this distance the enemy will chase
    public float speed;

    // private vars
    protected BoxCollider2D boxCollider; //for the enemy's box collider 
    protected Vector2 moveDelta; //the enemy's direction
    protected RaycastHit2D hit; //to know if the enemy has been hit


    // damage
    public int damageCooldown = 1;
    protected float nextDamage;

    // mage
    protected float slowed = 0;


   protected virtual void Start()
   {
       boxCollider = GetComponent<BoxCollider2D>(); //getting the enemies box collider 
        speed = topSpeed;
   }

    protected virtual void Update()
    {
        if(speed < topSpeed)
        {
            speed += .1f;
        }
    }

   protected virtual void FixedUpdate()
   {
        
        //Reset MoveDelta
        moveDelta = (target.position - transform.position); //Setting the enemy to move toward the player
        if (moveDelta.magnitude > radius) moveDelta = new Vector2(0, 0); //if the enemy is far from the player don't chase them 
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
        else if (hit.collider.gameObject.tag == "Player") hit.collider.gameObject.GetComponent<Player>().damage(attackDamage);
       

   }

    public void damage(int damage)
    {
        if (Time.time > nextDamage)
        {
            nextDamage = Time.time + damageCooldown;
            health -= damage;
            checkHealth();
        }
    }

    protected void checkHealth()
    {
        // drop rewards
        if (health <= 0) Destroy(this.gameObject);
    }

    public void slow()
    {
        slowed = speed / 2;
    }
}