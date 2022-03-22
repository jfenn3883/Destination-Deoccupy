using System.Collections;
using UnityEngine;


public class Collectable : Collidable
{
    protected bool collected;

    protected override void onCollide(Collider2D other)
    {
        if (other.name == "Player") onCollect(); // check how this works
    }

    protected virtual void onCollect()
    {
        if(!collected) collected = true;
    }
}
