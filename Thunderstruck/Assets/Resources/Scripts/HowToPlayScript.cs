using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayScript : MonoBehaviour
{
    //make one per screen
    public GameObject screen01;
    public GameObject screen02;
    public GameObject screen03;


    public GameObject btnNext;
    public GameObject btnPrevious;

    public int screenOn = 0;
    public GameObject[] screens;
    public GameObject currentScreen;
    // Start is called before the first frame update
    void Start()
    {
        screens = new GameObject[]
        {
            //add them here after
            screen01,
            screen02,
            screen03
            //etc
        };
        btnPrevious.SetActive(false);
        currentScreen = screen01;
        foreach (GameObject scrn in screens)
        {
            scrn.SetActive(false);
        }
        screens[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextScreen()
    {
        if (screenOn + 1 >= screens.Length)
        {
            return;
        }
        if (screenOn + 1== screens.Length-1)
        {
            btnNext.SetActive(false);
        }

        btnPrevious.SetActive(true);

        currentScreen.SetActive(false);
        currentScreen = screens[++screenOn];
        currentScreen.SetActive(true);



    }
    public void PreviousScreen()
    {
        if (screenOn - 1 < 0)
        {
            return;
        }
        if (screenOn - 1 == 0)
        {
            btnPrevious.SetActive(false);
        }
        btnNext.SetActive(true);
        currentScreen.SetActive(false);
        currentScreen = screens[--screenOn];
        currentScreen.SetActive(true);

    }
}
