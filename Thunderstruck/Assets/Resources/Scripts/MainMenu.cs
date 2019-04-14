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

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Leaderboard()
    {
        Application.OpenURL("https://umbrellastudios.azurewebsites.net/Home/Leaderboards");
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
