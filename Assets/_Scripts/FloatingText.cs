using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
    //variables to activate, and time duration of text 
    public bool active;
    public GameObject gamesObject;
    public Text txt;
    public Vector3 motion;
    public float duration;
    public float lastShown;
    
   // method to show text 
    public void Show()
    {
        active = true;
        lastShown = Time.time;
        gamesObject.SetActive(active);
    }
    //method to hide text 
    public void Hide()
    {
       active = false;
       gamesObject.SetActive(active); 
    }

    public void UpdateFloatingTxt()
    {
        if(!active) return;
        
        if(Time.time - lastShown > duration) Hide(); // if text has been shown for long enough hide

        gamesObject.transform.position += motion * Time.deltaTime;
    }
}
