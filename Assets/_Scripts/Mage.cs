using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Player
{
    protected bool isFire = true;
    public float attackCooldown = 1.5f;
    public float attackDistance = 3f;
    protected float nextAttack;

    public GameObject fire;

    protected ContactFilter2D contact = new ContactFilter2D();
    protected List<RaycastHit2D> raycast = new List<RaycastHit2D>();

    protected override void Start()
    {
        base.Start();

        contact.layerMask = LayerMask.GetMask("Enemy");
        
    }

    protected override void Update()
    {
        inputs = GetInputs();
        moveDelta = new Vector2(inputs["x"], inputs["y"]);

        if (inputs["q"] == 1) isFire = true;
        else if (inputs["e"] == 1) isFire = false;

        if (inputs["right"] != 0 && isFire) fireAttack(0);
        else if (inputs["up"] != 0 && isFire) fireAttack(1);
        else if (inputs["left"] != 0 && isFire) fireAttack(2);
        else if (inputs["down"] != 0 && isFire) fireAttack(3);
        if (inputs["right"] != 0) iceAttack(0);
        else if (inputs["up"] != 0) iceAttack(1);
        else if (inputs["left"] != 0) iceAttack(2);
        else if (inputs["down"] != 0) iceAttack(3);
    }

    protected void fireAttack(int dir)
    {
        if (Time.time > nextAttack)
        {
            nextAttack = Time.time + attackCooldown;
            if (dir == 0)
            {
                for (int i = 1; i < 6; i++)
                {
                    Instantiate(fire, new Vector2(this.transform.position.x + i, this.transform.position.y), new Quaternion());
                }
            }
            else if (dir == 1)
            {
                for (int i = 1; i < 6; i++)
                {
                    Instantiate(fire, new Vector2(this.transform.position.x, this.transform.position.y + i), new Quaternion());
                }
            }
            else if (dir == 2)
            {
                for (int i = 1; i < 6; i++)
                {
                    Instantiate(fire, new Vector2(this.transform.position.x - i, this.transform.position.y), new Quaternion());
                }
            }
            else if (dir == 3)
            {
                for (int i = 1; i < 6; i++)
                {
                    Instantiate(fire, new Vector2(this.transform.position.x, this.transform.position.y - i), new Quaternion());
                }
            }
        }
    }

    protected void iceAttack(int dir)
    {
        raycast.Clear();

        if (dir == 0) Physics2D.Raycast(boxCollider.bounds.center, new Vector2(1, 0), contact, raycast, attackDistance);
        else if (dir == 1) Physics2D.Raycast(boxCollider.bounds.center, new Vector2(0, 1), contact, raycast, attackDistance);
        else if (dir == 2) Physics2D.Raycast(boxCollider.bounds.center, new Vector2(-1, 0), contact, raycast, attackDistance);
        else if (dir == 3) Physics2D.Raycast(boxCollider.bounds.center, new Vector2(0, -1), contact, raycast, attackDistance);


        if (raycast.Count != 0)
        {
            foreach (RaycastHit2D hit in raycast)
            {
                if (hit.collider != null && hit.collider.gameObject.CompareTag("Enemy")) hit.collider.gameObject.GetComponent<Enemy>().slow();
            }
        }
    }
}
