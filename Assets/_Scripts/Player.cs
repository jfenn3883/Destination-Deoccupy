using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  float x;
  float y;
  private BoxCollider2D boxCollider;
  private Vector3 moveDelta;
  private RaycastHit2D hit;
  
  private float chargeCoolDown = 2f; // set as 2 seonds for development, needs to be set as 20 seconds for actual gameplay
   private float lastDash;
  private float lastDashTimer = 0.5f ;
  private bool isCharging = false;
  protected int health = 1; //the player's health
  protected int damage = 1; //the amount of damage the enemy deals to the player
  private  Vector3 lastDirection; 
   private Vector3 moveDir ;
    protected float lastHit; 
      protected float lastHitTimer = 1f;
      protected bool isHit = false;

   private void Start()
   {
       boxCollider = GetComponent<BoxCollider2D>();
   }
   private void FixedUpdate()
   {
     
     
     if(!isCharging) {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
      }
       //Reset MoveDelta
       moveDelta = new Vector3(x,y,0);

       //Swap Sprite direction, when moving
       if(moveDelta.x > 0)
       {
           transform.localScale = Vector3.one;
       }
       else if( moveDelta.x < 0)
       {
            transform.localScale = new Vector3(-1,1,0);
       }
       moveDir = new Vector3(moveDelta.x,moveDelta.y, 0);

       hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Enemy", "Blocking"));
       if(hit.collider == null)
       {
           //move object in y
         transform.Translate(0,moveDelta.y * Time.deltaTime,0);
       
       }
       
       hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x,0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Enemy", "Blocking"));
       if(hit.collider == null)
       {
           //move object in x
         transform.Translate(moveDelta.x * Time.deltaTime,0,0);
       }
       hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Enemy"));
        // if enemy collides with player in y-direction
          if(hit.collider != null && !isHit){
          PlayerGetHit();
        }
         //if enemy collides with player in x-direction
         hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x,0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Enemy"));
        // if enemy collides with player in x direction 
        if(hit.collider != null && !isHit){
            PlayerGetHit();
        }
        if(Time.time - lastHit > lastHitTimer){ 
          lastHit = Time.time;
          isHit = false; //set the enemy to be able to be hit
        } 
       if(Input.GetKeyDown(KeyCode.Space) && Time.time - lastDash > chargeCoolDown)
       {
         isCharging = true; 
         lastDash = Time.time;
         Charge();
       } else if(Time.time - lastDash  < lastDashTimer) {
         Charge();
       } else {
         isCharging = false;
       }

       
     
   }
  
    public void Charge()
   {
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
       if(hit.collider == null &&  isCharging == true)
       {
           //move object in y
         transform.Translate(0,moveDelta.y * Time.deltaTime,0);
       
       }
       
       hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x,0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
       if(hit.collider == null && isCharging == true)
       {
           //move object in x
         transform.Translate(moveDelta.x * Time.deltaTime,0,0);
       
       } 
    
      
   }
   public void PlayerGetHit(){
    isHit = true; //setting the boolean variable to show that the enemy has just been hit
    lastHit = Time.time; //saving the time the last hit occured
    health -= damage; //damaging the enemy
    if(health <= 0){ //if the enemy is out of health
        Destroy(this.gameObject); //destroy the enemy 
    }
   }
   
}
