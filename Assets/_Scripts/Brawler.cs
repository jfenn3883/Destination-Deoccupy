using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brawler : Player
{
    // Charge Variables
    public float chargeCooldown = 3f;
    public float chargeDuration = 2f;
    public int chargeMultiplier = 2;
    private float nextCharge;
    private bool isCharging;

    private Animator anim;

    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    protected override void Update() {
        if (!isCharging)
        {
            inputs = GetInputs();
            moveDelta = new Vector3(inputs["x"], inputs["y"]);

            if (inputs["right"] != 0) weapon.attack(0);
            else if (inputs["up"] != 0) weapon.attack(1);
            else if (inputs["left"] != 0) weapon.attack(2);
            else if (inputs["down"] != 0) weapon.attack(3);
        } 
    }

    protected override void FixedUpdate() {
        if (inputs["space"] == 1 && Time.time > nextCharge)
        {
            isCharging = true;
            nextCharge = Time.time + chargeCooldown;
        }
        else if (isCharging && Time.time > nextCharge - (chargeCooldown - chargeDuration)) isCharging = false;

        move();
    }

    protected override void move()
    {
        //Swap Sprite direction, when moving
        if (moveDelta.x > 0)
        {
            transform.localScale = Vector2.one;
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector2(-1, 1);// double check
        }

        hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.size, 0, new Vector2(moveDelta.x, moveDelta.y), Mathf.Abs(moveDelta.magnitude * Time.deltaTime * speed), LayerMask.GetMask("Enemy", "Blocking"));
        if (hit.collider == null) transform.Translate(moveDelta * Time.deltaTime * speed);

        if (isCharging)
        {
            if (hit.collider.gameObject.tag == "Enemy") hit.collider.gameObject.GetComponent<Enemy>().damage(weapon.attackDamage * chargeMultiplier);
            hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.size, 0, new Vector2(moveDelta.x, moveDelta.y), Mathf.Abs(moveDelta.magnitude * Time.deltaTime * speed), LayerMask.GetMask("Enemy", "Blocking"));
            if (hit.collider == null) transform.Translate(moveDelta * Time.deltaTime * speed * 2);
            if (hit.collider.gameObject.tag == "Enemy") hit.collider.gameObject.GetComponent<Enemy>().damage(weapon.attackDamage * chargeMultiplier);
        }
    }

    protected override void levelUp()
    {
        // health += 3;
        // chargeCooldown -= 1f;
        // chargeMultiplier += 1;
        // chargeDuration += .35f;
    }

}
