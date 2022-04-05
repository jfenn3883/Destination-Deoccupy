using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimation : MonoBehaviour
{
    public Animator anim; // Animator component
    public int showing = 0; // Is the menu showing?
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); // get the animator
    }

    // Update is called once per frame
    void Update()
    {
        //if the player presses e then the animation will play
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(showing == 0) anim.Play("menu_showing"); showing = 1;
            if(showing == 1) anim.Play("menu_hidden"); showing = 0;
        }
        
    }
}
