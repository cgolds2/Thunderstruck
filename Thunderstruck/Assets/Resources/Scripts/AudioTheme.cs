using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTheme : MonoBehaviour
{
    public AudioClip theme1;
    public AudioClip theme2;
    public AudioClip theme3;
    public AudioClip MiniBoss;
    public AudioClip Boss;
    public static int level;
    public bool isPlaying = false;
    public bool isPlaying1 = false;
    AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        //Main Soundtracks
        theme1 = Resources.Load<AudioClip>("level1theme");
        theme2 = Resources.Load<AudioClip>("LevelTwo");
        theme3 = Resources.Load<AudioClip>("Level3");

        //MiniBoss and Boss
        MiniBoss = Resources.Load<AudioClip>("MiniBoss");
        Boss = Resources.Load<AudioClip>("FinalBoss");

        audioSrc = GetComponent<AudioSource>();
        //audioSrc.clip = theme1;

        if (HUDScript.GetLevel() == 1)
        {
            audioSrc.clip = theme1;
            audioSrc.Play();

        }
        else if (HUDScript.GetLevel() == 2)
        {
            audioSrc.clip = theme2;
            audioSrc.Play();

        }

        else if(HUDScript.GetLevel() == 3)
            audioSrc.clip = theme3;
            audioSrc.Play();




    }

    // Update is called once per frame
    void Update()
    {
        if (MainScript.currentRoom.roomType.ToString() == "Boss" && isPlaying == false)
        {
            isPlaying = true;
            audioSrc.clip = MiniBoss;
            audioSrc.Play();

        }

        if(MainScript.currentRoom.roomType.ToString() == "Boss" && HUDScript.GetLevel() == 3 && isPlaying1 == false)
        {
            audioSrc.clip = Boss;
            audioSrc.Play();

        }
    }

}
