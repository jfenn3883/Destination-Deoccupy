using System.Collections;
using UnityEngine;


public class Chest : Collectable
{
    public Sprite emptyChest;
    public int coins = 10;

    private SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void onCollect()
    {
        base.onCollect();
        spriteRenderer.sprite = emptyChest;
    }
}