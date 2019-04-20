using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour
{
    public static GameObject HUD;
    public static GameObject Score;
    public static GameObject Time;
    public static GameObject Level;
    public static GameObject MapAnchor;
    public static GameObject Key;
    private static Text ScoreText;
    private static Text TimeText;
    private static Text LevelText;
    private static bool hasKey;
    private static int _level;
    private static int _score;
    private static TimeSpan _time;
    private static GameObject[] healthbars;
    public static Vector3 mapStartingPos;
    public static Material normal;
    public static Material greyed;

    public static GameObject redUmb;
    public static GameObject blueUmb;
    public static GameObject redBoots;
    public static GameObject yellowHat;
    public static GameObject redCoat;
    public static GameObject blueCoat;

    private void Awake()
    {
        HUD = gameObject;
        MapAnchor = GameObject.Find("MapAnchor");
        mapStartingPos = MapAnchor.transform.localPosition;
        Score = GameObject.Find("Score");
        ScoreText = Score.GetComponent<Text>();
        Time = GameObject.Find("Time");
        TimeText = Time.GetComponent<Text>();
        Level = GameObject.Find("Level");
        LevelText = Level.GetComponent<Text>();
        LevelText.text = "Level: " + _level.ToString();
        Key = GameObject.Find("HUDKey");

        normal = Resources.Load<Material>("Sprites/DefaultMat");
        greyed = Resources.Load<Material>("Sprites/greyedMat");
       
        redUmb = GameObject.Find("REDumbrella");
        blueUmb = GameObject.Find("BLUEumbrella");
        redBoots = GameObject.Find("boots");
        yellowHat = GameObject.Find("yellow hat pickup");
        redCoat = GameObject.Find("Red_Coat_Pickup");
        blueCoat = GameObject.Find("Blue_Coat_Pickup");


    }
    void RefreshHud(){
        
        if(CharacterScript.blueCoat){
            blueCoat.GetComponent<SpriteRenderer>().material = HUDScript.normal;
        }
        if (CharacterScript.redCoat)
        {
            redCoat.GetComponent<SpriteRenderer>().material = HUDScript.normal;
        }
        if (CharacterScript.redUmbrella)
        {
            redUmb.GetComponent<SpriteRenderer>().material = HUDScript.normal;
        }
        if (CharacterScript.blueUmbrella)
        {
            blueUmb.GetComponent<SpriteRenderer>().material = HUDScript.normal;
        }
        if (CharacterScript.boots)
        {
            redBoots.GetComponent<SpriteRenderer>().material = HUDScript.normal;
        }
        if (CharacterScript.hat)
        {
            yellowHat.GetComponent<SpriteRenderer>().material = HUDScript.normal;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        RefreshHud();
        //AddSecondToTimer();
        StartTimer();
        healthbars = new GameObject[8];
        var barAsset = Resources.Load<GameObject>("Sprites/HealthSquare");
        var offset = barAsset.GetComponent<Renderer>().bounds.size.x;
        for (int i = 0; i < healthbars.Length; i++)
        {
            healthbars[i] = MainScript.Instantiate(barAsset);
            healthbars[i].transform.position = new Vector3((float)-5.058 + offset * i, (float)4.594, (float)-0.6);
            healthbars[i].transform.parent = HUD.transform;
        }

    }
    public void PauseTimer()
    {
        CancelInvoke();
    }
    public void StartTimer()
    {
        InvokeRepeating("CallRepeat", 1, 1);
    }
    public void ResetTimer()
    {
        _time = new TimeSpan();
    }

    public void CallRepeat()
    {
        AddSecondToTimer();
    }
    // Update is called once per frame
    void Update()
    {

    }

    public static void SetScore(int score)
    {
        _score = score;
        var ui = ScoreText;
        if (ScoreText != null)
        {
            ui.text = "SCORE: " + score.ToString();

        }
    }
    public static int GetScore()
    {
        return _score;
    }

    public static void SetLevel(int level)
    {
        _level = level;

    }
    public static int GetLevel()
    {
        return _level;
    }

    public static void SetTime(TimeSpan time)
    {
        _time = time;
        var ui = TimeText;
        ui.text = "TIME: " + time.ToString("hh\\:mm\\:ss");
    }

    internal static void SetHealth(float health)
    {
        for (int i = 0; i < health; i++)
        {
            healthbars[i].GetComponent<Renderer>().enabled = true;
        }
        for (int i = (int)health; i < 8; i++)
        {
            healthbars[i].GetComponent<Renderer>().enabled = false;
        }
        //if(health > 0)
        //{
        //    healthbars[0].GetComponent<Renderer>().enabled = true;
        //}
    }

    public static TimeSpan GetTime()
    {
        return _time;
    }

    public static void AddSecondToTimer()
    {
        _time += TimeSpan.FromSeconds(1);
        SetTime(_time);
    }

    public static void MoveObjects(Vector3 location)
    {
        if (HUD == null) { return; }
        location.x -= (float)1.22;
        HUD.transform.position = location;

        //SetTime(new TimeSpan(1,2,3));
        //Score.transform.position = location;
        //Time.transform.position = location;
    }


    public static void SetKey(bool keyStatus)
    {
        hasKey = keyStatus;
        Key.SetActive(keyStatus);
    }

    public static bool GetKeyStatus()
    {
        return hasKey;
    }

    public static void AddToScore(int points)
    {
        if (CharacterScript.blueUmbrella)
        {
            points = (int)(points * 1.25);
        }
        SetScore(_score + points);
    }
}
