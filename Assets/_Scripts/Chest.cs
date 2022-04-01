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
        if(collected){
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.gameInstance.showText("+" + coins.ToString(), 20, Color.yellow, new Vector3(0,0,0), Vector3.up * 2, 1);
        }
        spriteRenderer.sprite = emptyChest;
    }
}