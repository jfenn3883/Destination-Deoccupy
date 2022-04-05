using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cowgirl : Player
{
    // gun variables
    public int bullets = 6;
    protected int loaded;
    protected float shotCooldown = 0.5f;
    protected float nextShot;
    protected float reloadCooldown = 4f;

    // private vars
    protected RaycastHit2D[] raycast = new RaycastHit2D[10];

    protected override void Start()
    {
        base.Start();
        loaded = bullets;
    }

    protected void shootGun()
    {
        if(Time.time > nextShot && loaded > 0)
        {
            fireGun(0); // dir math
            nextShot = Time.time + shotCooldown;
            loaded--;
            if (loaded < 1) reloadGun();
        }
    }

    protected void reloadGun()
    {
        nextShot = Time.time + reloadCooldown;
        loaded = bullets;
    }

    protected void fireGun(int dir)
    {
        if(dir == 0 || dir == 2) raycast = Physics2D.BoxCastAll(boxCollider.bounds.center, new Vector2(.16f, .04f), 0, new Vector2(moveDelta.x, 0), LayerMask.GetMask("Enemy"));
        else if(dir == 1) raycast = Physics2D.BoxCastAll(boxCollider.bounds.center, new Vector2(.16f, .04f), 90, new Vector2(0, 1), LayerMask.GetMask("Enemy"));
        else if (dir == 3) raycast = Physics2D.BoxCastAll(boxCollider.bounds.center, new Vector2(.16f, .04f), 90, new Vector2(0, -1), LayerMask.GetMask("Enemy"));
    }
}
