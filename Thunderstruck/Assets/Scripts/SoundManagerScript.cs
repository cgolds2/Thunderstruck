using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

    public static AudioClip playerShootSound, playerHitSound, playerWalkingSound, playerDeathSound;
    public static bool isPlaying;
    static AudioSource audioSrc; 
    // Start is called before the first frame update
    void Start()
    {
        playerDeathSound = Resources.Load<AudioClip>("death");
        playerWalkingSound = Resources.Load<AudioClip>("footstep");
        playerShootSound = Resources.Load<AudioClip>("shoot");
        playerHitSound =  Resources.Load<AudioClip>("splah");

        audioSrc = GetComponent<AudioSource>();



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "fire":
                audioSrc.PlayOneShot(playerShootSound);
                break;
            case "death":
                if(audioSrc.isPlaying == false)
                    audioSrc.PlayOneShot(playerDeathSound);
                break;
            case "walking":
                if(audioSrc.isPlaying == false)
                    audioSrc.PlayOneShot(playerWalkingSound);
                break;
            case "hit":
                if(audioSrc.isPlaying == false)
                    audioSrc.PlayOneShot(playerHitSound);
                break;
        }



    }
}
