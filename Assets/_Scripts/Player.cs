using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
  protected int health = 6; //the player's health
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
          isHit = false; //set the player to be able to be hit
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

    public void Update()
    {
        Dictionary<string, int> inputs = GetInputs();

        if (!isCharging)
        {
            moveDelta = new Vector3(inputs["x"], inputs["y"], 0);
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
    GameManager.gameInstance.showText("Player took " + damage + " damage",25,Color.black, new Vector3(0f, 0.0f, 0),Vector3.down * 20, 3f);

    if(health <= 0){ //if the enemy is out of health
        Destroy(this.gameObject); //destroy the enemy 
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//reloads game
    }
    
   }

   public Dictionary<string, int> GetInputs() // gets all the relevent inputs for the player
    {
        Dictionary<string, int> inputs = new Dictionary<string, int>();
        inputs.Add("x", 0);
        inputs.Add("y", 0);

        inputs.Add("space", 0);

        inputs.Add("left", 0);
        inputs.Add("right", 0);
        inputs.Add("down", 0);
        inputs.Add("up", 0);

        inputs.Add("e", 0);
        inputs.Add("q", 0);

        if (Input.GetKey(KeyCode.A)) inputs["x"] -= 1;
        if (Input.GetKey(KeyCode.D)) inputs["x"] += 1;
        if (Input.GetKey(KeyCode.S)) inputs["y"] -= 1;
        if (Input.GetKey(KeyCode.W)) inputs["y"] += 1;

        if (Input.GetKey(KeyCode.Space)) inputs["space"] = 1;

        if (Input.GetKey(KeyCode.LeftArrow)) inputs["left"] = 1;
        if (Input.GetKey(KeyCode.RightArrow)) inputs["right"] = 1;
        if (Input.GetKey(KeyCode.DownArrow)) inputs["down"] = 1;
        if (Input.GetKey(KeyCode.UpArrow)) inputs["up"] = 1;

        if (Input.GetKey(KeyCode.E)) inputs["e"] = 1;
        if (Input.GetKey(KeyCode.Q)) inputs["q"] = 1;

        return inputs;
    } 
   
}
