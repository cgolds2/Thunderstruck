using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletedScript : MonoBehaviour
{
    public Text LevelCompleted;
    public Text Score;
    public GameObject NextLevelButton;

    // Start is called before the first frame update
    void Start()
    {
        Score.text = "Score = " + HUDScript.GetScore().ToString();
        if (HUDScript.GetLevel() >= 3)
        {
            NextLevelButton.SetActive(false);
            LevelCompleted.text = "Demo Completed";
            CharacterScript.SendScore();
        }
        else
        {
            LevelCompleted.text = string.Format("Level {0} Completed", HUDScript.GetLevel());

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
