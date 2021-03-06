﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public Transform canvas;
    //push comment


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (canvas.gameObject.activeInHierarchy == false)
            {
                canvas.gameObject.SetActive(true);
                GameObject script = GameObject.FindGameObjectWithTag("Player");
                script.GetComponent<CharacterScript>().enabled = false;
                Time.timeScale = 0;
            }
            else
            {
                canvas.gameObject.SetActive(false);
                Time.timeScale = 1;
                GameObject script = GameObject.FindGameObjectWithTag("Player");
                script.GetComponent<CharacterScript>().enabled = true;


            }
        }

    }

    public void Resume()
    {
        canvas.gameObject.SetActive(false);
        Time.timeScale = 1;
        GameObject script = GameObject.FindGameObjectWithTag("Player");
        script.GetComponent<CharacterScript>().enabled = true;


    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
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
}
