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
    protected Rigidbody2D body;
    
    // private vars
    protected Vector2 moveDelta;
    protected RaycastHit2D hit;
    protected Dictionary<string, int> inputs;
    protected bool isHit = false;

    protected Vector3 lastPos;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        body = GetComponent<Rigidbody2D>();
        inputs = GetInputs();
    }

    
    protected virtual void Update()
    {
        inputs = GetInputs();
        moveDelta = new Vector2(inputs["x"], inputs["y"]);
    }

    protected virtual void FixedUpdate() {
        move();
    }
      

    protected virtual void move() {
      
       //Swap Sprite direction, when moving
       if(moveDelta.x > 0)
       {
           transform.localScale = Vector2.one;
       }
       else if( moveDelta.x < 0)
       {
            transform.localScale = new Vector2(-1, 1);// double check
       }

        hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.size, 0, new Vector2(moveDelta.x, moveDelta.y), Mathf.Abs(moveDelta.magnitude * Time.deltaTime * speed), LayerMask.GetMask("Enemy", "Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(moveDelta * Time.deltaTime * speed);
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
}
