using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cowgirl : Player
{
    // gun variables
    public int bullets = 6;
    public float reloadSpeed;
    protected bool isReloading = false;
    protected int loaded;
    public float shotCooldown = 0.5f;
    protected float nextShot;
    public float reloadCooldown = 4f;
    public int gunDamage = 2;
    public float rollCooldown = 20f;
    public float rollDuration = 1f;
    protected float nextRoll;
    protected bool isRolling = false;
    protected ContactFilter2D contact = new ContactFilter2D();
    

    // private vars
    protected List<RaycastHit2D> raycast = new List<RaycastHit2D>();

    protected override void Start()
    {
        base.Start();
        loaded = bullets;
        reloadSpeed = speed / 2;
        contact.layerMask = LayerMask.GetMask("Enemy");
    }

    protected override void Update()
    {
        inputs = GetInputs();

        moveDelta = new Vector2(inputs["x"], inputs["y"]);

        if (inputs["right"] != 0) shootGun(0);
        else if (inputs["up"] != 0) shootGun(1);
        else if (inputs["left"] != 0) shootGun(2);
        else if (inputs["down"] != 0) shootGun(3);

        if (isReloading && Time.time > nextShot) isReloading = false; 
    }

    protected override void FixedUpdate()
    {
        if (inputs["space"] == 1 && Time.time > nextRoll)
        {
            isRolling = true;
            nextRoll = Time.time + rollCooldown;
        }
        else if (isRolling && Time.time > nextRoll - (rollCooldown - rollDuration))
        {
            isRolling = false;
            transform.localRotation = new Quaternion();
        }
        move();
    }

    protected override void move()
    {
        if (moveDelta.x > 0)
        {
            transform.localScale = Vector2.one;
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector2(-1, 1);// double check
        }

        if(isRolling)
        {
            roll();
            hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.size, 0, new Vector2(moveDelta.x, moveDelta.y), Mathf.Abs(moveDelta.magnitude * Time.deltaTime * speed), LayerMask.GetMask("Enemy", "Blocking"));
            if (hit.collider == null) transform.Translate(moveDelta * Time.deltaTime * speed);
        } else if(isReloading)
        {
            hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.size, 0, new Vector2(moveDelta.x, moveDelta.y), Mathf.Abs(moveDelta.magnitude * Time.deltaTime * reloadSpeed), LayerMask.GetMask("Enemy", "Blocking"));
            if (hit.collider == null) transform.Translate(moveDelta * Time.deltaTime * reloadSpeed);
        } else
        {
            hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.size, 0, new Vector2(moveDelta.x, moveDelta.y), Mathf.Abs(moveDelta.magnitude * Time.deltaTime * speed), LayerMask.GetMask("Enemy", "Blocking"));
            if (hit.collider == null) transform.Translate(moveDelta * Time.deltaTime * speed);
        }
    }

    protected void shootGun(int dir)
    {
        if(Time.time > nextShot && loaded > 0)
        {
            fireGun(dir); // dir math
            nextShot = Time.time + shotCooldown;
            loaded--;
            if (loaded < 1) reloadGun();
        }
    }

    protected void reloadGun()
    {
        nextShot = Time.time + reloadCooldown;
        isReloading = true;
        loaded = bullets;
    }

    protected void fireGun(int dir)
    {
        raycast.Clear();
        
        if (dir == 0) Physics2D.Raycast(boxCollider.bounds.center, new Vector2(1, 0), contact, raycast, Mathf.Infinity);
        else if(dir == 1) Physics2D.Raycast(boxCollider.bounds.center, new Vector2(0, 1), contact, raycast, Mathf.Infinity);
        else if (dir == 2) Physics2D.Raycast(boxCollider.bounds.center, new Vector2(-1, 0), contact, raycast, Mathf.Infinity);
        else if (dir == 3) Physics2D.Raycast(boxCollider.bounds.center, new Vector2(0, -1), contact, raycast, Mathf.Infinity);

        
        if (raycast.Count != 0)
        {
            foreach (RaycastHit2D hit in raycast)
            {
                if (hit.collider != null && hit.collider.gameObject.tag == "Enemy") hit.collider.gameObject.GetComponent<Enemy>().damage(gunDamage);
            }
        }
    }

    protected void roll()
    {
        loaded = bullets;
        float angle = 360 / rollDuration;
        transform.Rotate(new Vector3(0, 0, -moveDelta.x), angle * Time.deltaTime);
    }

    protected override void levelUp()
    {
        // health += 2;
        // reloadCooldown -= .65f;
        // shotCooldown -= .15f;
    }
}
