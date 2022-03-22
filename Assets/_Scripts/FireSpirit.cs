using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpirit : Enemy{
    protected override void Start()
    {
        base.Start();
        health = 1;
    }
}