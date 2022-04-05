using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAnimation : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
     if(Input.GetKeyDown(KeyCode.RightArrow))   {
         anim.Play("swordSwing");
     }
     if(Input.GetKeyDown(KeyCode.LeftArrow))   {
         anim.Play("swordSwing");
     }
     if(Input.GetKeyDown(KeyCode.UpArrow))   {
         anim.Play("swordUp");
     }
     if(Input.GetKeyDown(KeyCode.DownArrow))   {
         anim.Play("swordDown");
    }
}
}
