using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour
{
    public static GameObject HUD;
    public static GameObject Score;
    public static GameObject Time;
    private static Text ScoreText;
    private static Text TimeText;
    private static int _score;
    private static TimeSpan _time;

    // Start is called before the first frame update
    void Start()
    {
        HUD = GameObject.Find("HUD");
        Score  = GameObject.Find("Score");
        ScoreText = Score.GetComponent<Text>();
        Time = GameObject.Find("Time");
        TimeText = Time.GetComponent<Text>();
        //AddSecondToTimer();
        StartTimer();
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
