using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        health = 5; // set health to 5
        //GameManager.gameInstance.showText("hello lil boy",25,Color.black, new Vector3(1.15f, 0.9f, 0),Vector3.down * 20, 3f);

  }
}
