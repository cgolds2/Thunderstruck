using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static string username;
    public GameObject userBoxObject;

    public InputField usernameBox;
    public GameObject pointerObject;
    public void PlayGame()
    {
        if (string.IsNullOrEmpty(username))
        {
            pointerObject.SetActive(true);
        }
        else
        {
        
        MainScript.CalcSeed(null);
            HUDScript.ResetTimer();
        CharacterScript.ResetItems();
            HUDScript.SetScore(0);
        SceneManager.LoadScene("MainGame");
    }
    }
    public void NextLevel()
    {
        HUDScript.SetLevel(HUDScript.GetLevel() + 1);
        
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");

    }
    Vector3 floatY;
    float originalY;


    public void Start()
    {
        if(userBoxObject != null){
            usernameBox = userBoxObject.GetComponent<InputField>();
            username = PlayerPrefs.GetString("username");

            usernameBox.text = username;

            originalY = pointerObject.transform.position.y;
            pointerObject.SetActive(false);
        }
      

    
    }
    public void Update()
    {
        if(userBoxObject != null){
            floatY = pointerObject.transform.position;
            floatY.y = originalY + (Mathf.Sin(Time.time) * 50);
            pointerObject.transform.position = floatY;
        }

    }

    public void UsernameChanged(){
        MainMenu.username = usernameBox.text;
 
            PlayerPrefs.SetString("username",username);
    }


    public static void QuitGame()
    {
        // save any game data here

        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game

         Application.Quit();
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
