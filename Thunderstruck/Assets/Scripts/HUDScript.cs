﻿using System;
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
    public static GameObject MapAnchor;
    private static Text ScoreText;
    private static Text TimeText;
    private static int _score;
    private static TimeSpan _time;
    private static GameObject[] healthbars;
    public static Vector3 mapStartingPos;

    private void Awake()
    {
        HUD = gameObject;
        MapAnchor = GameObject.Find("MapAnchor");
        mapStartingPos = MapAnchor.transform.localPosition;

    }
    // Start is called before the first frame update
    void Start()
    {
        Score  = GameObject.Find("Score");
        ScoreText = Score.GetComponent<Text>();
        Time = GameObject.Find("Time");
        TimeText = Time.GetComponent<Text>();
        //AddSecondToTimer();
        StartTimer();
        healthbars = new GameObject[8];
        var barAsset = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Sprites/HealthSquare.prefab");
        var offset = barAsset.GetComponent<Renderer>().bounds.size.x;
        for (int i = 0; i < healthbars.Length; i++)
        {
            healthbars[i] = MainScript.Instantiate(barAsset);
            healthbars[i].transform.position = new Vector3((float)-5.058 + offset*i, (float) 4.594, (float) -0.6);
            healthbars[i].transform.parent = HUD.transform;
        }
    }
    public void PauseTimer(){
        CancelInvoke();
    }
    public void StartTimer(){
        InvokeRepeating("CallRepeat", 1, 1);
    }
    public void ResetTimer(){
        _time = new TimeSpan();
    }

    public void CallRepeat(){
        AddSecondToTimer();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public static void SetScore(int score)
    {
        _score = score;
        var ui = Score.GetComponent<Text>();
        ui.text = "SCORE: " +score.ToString();
    }
    public static int GetScore()
    {
        return _score;
    }

    public static void SetTime(TimeSpan time)
    {
        _time = time;
        var ui = Time.GetComponent<Text>();
        ui.text = "TIME: " + time.ToString("hh\\:mm\\:ss");
    }

    internal static void SetHealth(int health)
    {
        for (int i = 0; i < health; i++)
        {
            healthbars[i].GetComponent<Renderer>().enabled = true;
        }
        for (int i = health; i < 8; i++)
        {
            healthbars[i].GetComponent<Renderer>().enabled = false;

        }

    }

    public static TimeSpan GetTime()
    {
        return _time;
    }

    public static void AddSecondToTimer(){
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
}
