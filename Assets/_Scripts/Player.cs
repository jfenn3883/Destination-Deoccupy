using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Player : MonoBehaviour
{
    // player attributes
    public int health = 10;
    public int coins = 0;
    public float speed = 1;
    public float experience = 0;
    public Weapon weapon;

    // cooldowns
    public float damageCooldown = 1f;

    // next occurrence
    protected float nextDamage;

    // private components
    protected BoxCollider2D boxCollider;
    protected Rigidbody2D body;
    
    // private vars
    protected Vector2 moveDelta;
    protected RaycastHit2D hit;
    protected Dictionary<string, int> inputs;

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

        if (inputs["right"] != 0) weapon.attack(0);
        else if (inputs["up"] != 0) weapon.attack(1);
        else if (inputs["left"] != 0) weapon.attack(2);
        else if (inputs["down"] != 0) weapon.attack(3);
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
        if (hit.collider == null) transform.Translate(moveDelta * Time.deltaTime * speed);
    }

    public void damage(int damage)
    {
        if(Time.time > nextDamage)
        {
            nextDamage = Time.time + damageCooldown;
            health -= damage;
            checkHealth();
        }
    }

    protected void checkHealth() // TODO: Implement the stage reloading
    {
        if(health <= 0)
        {
            
            Destroy(this.gameObject);
        }
    }

    public void exp(int exp)
    {
        experience += exp;
        if (experience > 50) levelUp();
    }

    protected virtual void levelUp()
    {

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
