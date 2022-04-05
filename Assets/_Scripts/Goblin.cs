using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    protected int coins;

    protected override void FixedUpdate()
    {
        //Reset MoveDelta
        moveDelta = (target.position - transform.position); //Setting the enemy to move toward the player
        if (moveDelta.magnitude > radius) moveDelta = new Vector2(0, 0); //if the enemy is far from the player don't chase them 
        moveDelta = moveDelta.normalized; //normalize the vector 

        //Swap Sprite direction, when moving
        if (moveDelta.x > 0)
        {
            transform.localScale = Vector2.one;
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }

        // check if we can move where we want
        hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.size, 0, new Vector2(moveDelta.x, moveDelta.y), Mathf.Abs(moveDelta.magnitude * Time.deltaTime * speed), LayerMask.GetMask("Enemy", "Blocking", "Player"));
        if (hit.collider == null) transform.Translate(moveDelta * Time.deltaTime * speed);
        else if (hit.collider.gameObject.tag == "Player")
        {
            hit.collider.gameObject.GetComponent<Player>().damage(attackDamage);
            // TODO: steal coins and add to coin count, but only give half back on death
        }
    }
}
