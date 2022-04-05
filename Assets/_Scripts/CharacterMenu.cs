using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //for images and text

public class CharacterMenu : MonoBehaviour
{
  //text variables
  public Text levelText;
  public Text healthText;
  public Text coinText;
  public Text expText;
  public Text expToNextLevelText;
  //image variables
  public Image weaponImage;
  public Image characterImage;
  public void updateMenu(){
      //update the images
        characterImage.sprite = GameManager.gameInstance.playerSprite;
        weaponImage.sprite = GameManager.gameInstance.weaponSprite;
      //update the text
      healthText.text = "Health: " + GameManager.gameInstance.health;
      coinText.text = "Coins: " + GameManager.gameInstance.coins;
      expText.text = "Experience: " + GameManager.gameInstance.xpPoints;
  }
}
