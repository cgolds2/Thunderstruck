using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDScript : MonoBehaviour
{
    public static GameObject hud;
    public static GameObject score;
    // Start is called before the first frame update
    void Start()
    {
        hud = GameObject.Find("HUD");
        score = GameObject.Find("Score");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

