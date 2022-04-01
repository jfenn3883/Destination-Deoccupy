using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player_1 : MonoBehaviour
{
    // player attributes
    public Sprite sprite;
    public int health;
    public int coins;
    public float speed;
    public int damage;

    // cooldowns
    public float takeDmgCooldown;
    public float attackCooldown;

    // private components
    protected BoxCollider2D boxCollider;
    
    // private vars
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    protected Dictionary<string, int> inputs;
    protected Vector3 moveDir ;
    protected bool isHit = false;
    protected Enemy enemy;
    protected virtual void Start()
    {
    
    }

    
    protected virtual void Update()
    {
        inputs = GetInputs();
    }

    protected virtual void FixedUpdate() {

    }

    protected virtual void move(float speed) {
        //Swap Sprite direction, when moving
       if(moveDelta.x > 0)
       {
           transform.localScale = Vector3.one;
       }
       else if( moveDelta.x < 0)
       {
            transform.localScale = new Vector3(-1,1,0);// double check
       }

       moveDir = new Vector3(moveDelta.x,moveDelta.y, 0).normalized;

       hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Enemy", "Blocking"));
       if(hit.collider == null)
       {
           //move object in y
         transform.Translate(0,moveDelta.y * Time.deltaTime*speed,0);
       
       }
       
       hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x,0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Enemy", "Blocking"));
       if(hit.collider == null)
       {
           //move object in x
         transform.Translate(moveDelta.x * Time.deltaTime*speed,0,0);
       }
      
    }

    protected virtual void attack() {

    }

    protected virtual void takeDmg(int dmg) {
        // check to make sure we haven't taken dmg in the last @takeDmgCooldown
        this.health -= dmg;
        checkAlive();
    }

    // probably going to go in weapons
    protected virtual void dealDmg(Enemy enemy, int dmg) {
       // enemy.takeDmg(dmg);
    }

    protected virtual void checkAlive() {
        // write checkAlive
    }

    protected virtual Dictionary<string, int> GetInputs() // gets all the relevent inputs for the player
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
