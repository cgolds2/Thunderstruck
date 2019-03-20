using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public Transform canvas;
    public GameObject button;
    //push comment


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (canvas.gameObject.activeInHierarchy == false)
            {
                canvas.gameObject.SetActive(true);
                button.gameObject.SetActive(true);
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
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
