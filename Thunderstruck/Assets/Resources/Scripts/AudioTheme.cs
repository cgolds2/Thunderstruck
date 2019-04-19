using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTheme : MonoBehaviour
{
    public AudioClip theme1;
    public AudioClip theme2;
    public AudioClip theme3;
    public static int level;
    AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        theme1 = Resources.Load<AudioClip>("level1theme");
        theme2 = Resources.Load<AudioClip>("LevelTwo");
        theme3 = Resources.Load<AudioClip>("LevelTwo");

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

        else
            audioSrc.clip = theme3;
            audioSrc.Play();



    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
