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
  
  private float chargeCoolDown = 2f;
   private float lastDash;
  private float lastDashTimer = 0.5f ;
  private bool isCharging = false;
  private  Vector3 lastDirection; 
   private Vector3 moveDir ;

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

       hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Block"));
       if(hit.collider == null)
       {
           //move object in y
         transform.Translate(0,moveDelta.y * Time.deltaTime,0);
       
       }
       
       hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x,0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Block"));
       if(hit.collider == null)
       {
           //move object in x
         transform.Translate(moveDelta.x * Time.deltaTime,0,0);
       
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
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Block"));
       if(hit.collider == null &&  isCharging == true)
       {
           //move object in y
         transform.Translate(0,moveDelta.y * Time.deltaTime,0);
       
       }
       
       hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x,0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Block"));
       if(hit.collider == null && isCharging == true)
       {
           //move object in x
         transform.Translate(moveDelta.x * Time.deltaTime,0,0);
       
       } 
    
      
   }
}
