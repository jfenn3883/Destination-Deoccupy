using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Player : MonoBehaviour
{
    // player attributes
    public int health;
    public int coins;
    public float speed;
    public float experience;
    public Weapon weapon;

    // last occurrence
    public float lastTakeDmg;
    public float lastAttack; 
    // cooldowns
    public float takeDmgCooldown = 1f;
    public float attackCooldown = 1f;

    // private components
    protected BoxCollider2D boxCollider;
    
    // private vars
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    protected Dictionary<string, int> inputs;
    protected bool isHit = false;

    protected Vector3 lastPos;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        inputs = GetInputs();
    }

    
    protected virtual void Update()
    {
        inputs = GetInputs();
        moveDelta = new Vector3(inputs["x"], inputs["y"]);
    }

    protected virtual void FixedUpdate() {
        move();
    }
      

    protected virtual void move() {
      
       //Swap Sprite direction, when moving
       if(moveDelta.x > 0)
       {
           transform.localScale = Vector3.one;
       }
       else if( moveDelta.x < 0)
       {
            transform.localScale = new Vector3(-1, 1);// double check
       }

        // Use BoxCast to see if our BoxCollider will run into a BoxCollider on the Enemy or Blocking Layers
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, moveDelta.y), Mathf.Abs(moveDelta.magnitude * Time.deltaTime * speed), LayerMask.GetMask("Enemy", "Blocking"));
        if (hit.collider == null)
        {
            lastPos = transform.position;
            //move object in x
            transform.Translate(moveDelta.x * Time.deltaTime * speed, moveDelta.y * Time.deltaTime * speed, 0);
        }

    }

    protected virtual void attack()
    {

    }

    protected virtual void takeDmg(int dmg) {
        //***if statement missing collision with enemny or projectile ***
       if(checkAlive()) 
       {
            if(Time.time - lastTakeDmg > takeDmgCooldown) // check last time taken damage against the cooldown time 
            {
                lastTakeDmg = Time.time; //set new last damage timer 
                this.health -= dmg; // decrease health;
            }
       }
    }

    protected virtual void GetExperience(int xpAmount) {
        if(experience < 50)
        {
            experience += xpAmount;
        }
    }
    protected virtual void Leveling() // REDO
    {
        int level = 1;
        if(experience == 50)
        {
            
          health += 5;
          experience = 0;
          level++;
          //text display the levelup and increase in health "Level up gained 5 health points

        }
    }

    protected virtual bool checkAlive() {
      bool alive = true;
        if(health > 0)
        {
            alive = true; 
        }
        else if(health <= 0)
        {
            alive = false;
            Destroy(this.gameObject); // destroy the player
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);// reloads game ***needs to be changed****
        }
        return alive; 
        
    }
    protected virtual void GetCoins(int coinAmount) {
        coins += coinAmount;
    }
    protected virtual void SpendCoins(int price) {
        if(coins >= price && coins > 0)
        {
            coins -= price;
        }
        else 
        {
            return; // need to use floating text to display messeage!
        }
       
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

    void OnCollisionStay2D(Collision2D collision)
    {
        print("fuck");
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy") transform.position = lastPos;
    }
}
