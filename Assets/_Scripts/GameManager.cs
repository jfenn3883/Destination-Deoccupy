using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameInstance;
    private void Awake()
    {
        if(GameManager.gameInstance != null) // make sure no duplicate game manager exist in level when loading 
        {
            Destroy(gameObject);
            return;
        }
        gameInstance = this;

        //intiate event sceneLoaded and loading
        SceneManager.sceneLoaded += LoadGame;
        DontDestroyOnLoad(gameObject); // allows gameManager object to stay when loading new level
    } 

    //Resources, keep track of player, their weapons, and xp 
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprite;
    public List<Sprite> weaponPrices;
    public List<Sprite> xpTable;

    //References
    public Player player;
    
    //logic
    public int coins;
    public int xpPoints;

    //save and load the game for progression
    public void SaveGame()
    {
        string save = "";
        save +="0" + "|";
        save += coins.ToString() + "|";
        save += xpPoints.ToString() + "|";
        save +="0"; 
        PlayerPrefs.SetString("SaveState",save);
    }
    public void LoadGame(Scene s, LoadSceneMode sceneMode)
    {
        if(!PlayerPrefs.HasKey("SaveGame"))
        {
            return;
        }
       string[] gameData = PlayerPrefs.GetString("SaveGame").Split('|');
       //Change player skin
       coins = int.Parse(gameData[1]);
      xpPoints = int.Parse(gameData[2]);
      //Change the weapon level
        Debug.Log("Load");
         
    }
    
}
