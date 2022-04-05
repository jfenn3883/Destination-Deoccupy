using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FloatingTextManager : MonoBehaviour
{
  //all text will in a prefab
  public GameObject textContainer;
  public GameObject textPrefab;

  private List<FloatingText> floatingTexts = new List<FloatingText>();

private void Update(){
 foreach (FloatingText txt in floatingTexts)
    txt.UpdateFloatingTxt();
  
}
public void Show(string messege, int fontSize, Color colour, Vector3 position, Vector3 motion, float duration )
{
    FloatingText floatingText = GetFloatingText();
    floatingText.txt.text = messege;
    floatingText.txt.fontSize = fontSize;
    floatingText.txt.color = colour;
    floatingText.gamesObject.transform.position = Camera.main.WorldToScreenPoint(position); // change world space to screen space 
    floatingText.motion = motion;
    floatingText.duration = duration;
    floatingText.Show();
}
  private FloatingText GetFloatingText()
  {
    FloatingText txt = floatingTexts.Find(t => !t.active); // looking for non active text 
    if(txt == null)
    {
        txt = new FloatingText();
        txt.gamesObject = Instantiate(textPrefab);
        txt.gamesObject.transform.SetParent(textContainer.transform);
        txt.txt = txt.gamesObject.GetComponent<Text>();

        floatingTexts.Add(txt); // once new object created add to list 
    }
    return txt;
  }
}
