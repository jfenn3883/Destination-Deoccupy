using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{
    public string sceneName;
   protected override void onCollide(Collider2D coll)
   {
       if(coll.name == "player_0")
       {
        GameManager.gameInstance.SaveGame();

        //Load new scene
        sceneName = "LevelTwo";
        SceneManager.LoadScene(sceneName);
       }
   }
}
