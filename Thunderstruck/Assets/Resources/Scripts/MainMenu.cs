using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlayGame()
    {
        MainScript.CalcSeed(null);
        HUDScript.SetScore(0);
        SceneManager.LoadScene("MainGame");
    }
    public void NextLevel()
    {
        HUDScript.SetLevel(HUDScript.GetLevel() + 1);
        
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");

    }

    public static void QuitGame()
    {
        // save any game data here
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    public void Leaderboard()
    {
        // save any game data here
#if UNITY_EDITOR
        //idk    
#else
              Application.OpenURL("https://umbrellastudios.azurewebsites.net/Home/Leaderboards");

#endif
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene("How To Play");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
