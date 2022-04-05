using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cowgirl : Player
{
    // gun variables
    public int bullets = 6;
    public float reloadSpeed;
    public bool isReloading = false;
    protected int loaded;
    public float shotCooldown = 0.5f;
    protected float nextShot;
    public float reloadCooldown = 4f;
    public int gunDamage = 2;
    

    // private vars
    protected RaycastHit2D[] raycast = new RaycastHit2D[10];

    protected override void Start()
    {
        base.Start();
        loaded = bullets;
        reloadSpeed = speed / 2;
    }

    protected override void Update()
    {
        inputs = GetInputs();

        moveDelta = new Vector2(inputs["x"], inputs["y"]);

        if (inputs["right"] != 0) shootGun(0);
        else if (inputs["up"] != 0) shootGun(1);
        else if (inputs["left"] != 0) shootGun(2);
        else if (inputs["down"] != 0) shootGun(3);
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

        if(isReloading)
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
            isReloading = false;
            fireGun(dir); // dir math
            nextShot = Time.time + shotCooldown;
            loaded--;
            if (loaded < 1) reloadGun();
        }
    }

    protected void reloadGun()
    {
        isReloading = true;
        nextShot = Time.time + reloadCooldown;
        loaded = bullets;
    }

    protected void fireGun(int dir)
    {
        if(dir == 0 || dir == 2) raycast = Physics2D.BoxCastAll(boxCollider.bounds.center, new Vector2(.16f, .04f), 0, new Vector2(moveDelta.x, 0), LayerMask.GetMask("Enemy"));
        else if(dir == 1) raycast = Physics2D.BoxCastAll(boxCollider.bounds.center, new Vector2(.16f, .04f), 90, new Vector2(0, 1), LayerMask.GetMask("Enemy"));
        else if (dir == 3) raycast = Physics2D.BoxCastAll(boxCollider.bounds.center, new Vector2(.16f, .04f), 90, new Vector2(0, -1), LayerMask.GetMask("Enemy"));

        foreach(RaycastHit2D hit in raycast)
        {
            if (hit.collider == null) continue;
            hit.collider.gameObject.GetComponent<Enemy>().damage(gunDamage);
        }

        raycast = new RaycastHit2D[10];
    }
}
