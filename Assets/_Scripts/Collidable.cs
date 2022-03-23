using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collidable : MonoBehaviour
{
    public ContactFilter2D filter;
    private BoxCollider2D boxCollider;
    private Collider2D[] hits = new Collider2D[10];

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
    {
        // looks for things colliding
        boxCollider.OverlapCollider(filter, hits);

        for (int i = 0; i < hits.Length; i++)
        {
            // if the index is null, skip
            if (hits[i] == null) continue;

            onCollide(hits[i]);

            // reset the colliders
            hits[i] = null;
        }
    }

    protected virtual void onCollide(Collider2D coll)
    {
        Debug.Log(coll.name);
    }
}
