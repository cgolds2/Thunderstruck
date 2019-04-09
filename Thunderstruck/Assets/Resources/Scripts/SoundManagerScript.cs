using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

    public static AudioClip playerShootSound, playerHitSound, playerWalkingSound, playerDeathSound,playerReflectSound;
    public static bool isPlaying;
    static AudioSource audioSrc;
    //public CharacterScript playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerDeathSound = Resources.Load<AudioClip>("death");
        playerWalkingSound = Resources.Load<AudioClip>("footstep");
        playerShootSound = Resources.Load<AudioClip>("shoot");
        playerHitSound =  Resources.Load<AudioClip>("splah");
        playerReflectSound = Resources.Load<AudioClip>("Reflecting");

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
                Debug.Log("DEATH SOUND");
                audioSrc.PlayOneShot(playerDeathSound);
                break;
            case "walking":
                if(audioSrc.isPlaying == false)
                    audioSrc.PlayOneShot(playerWalkingSound);
                break;
            case "hit":
                Debug.Log("HIT SOUND");
                //GameObject thePlayer = GameObject.Find("ThePlayer");
                //CharacterScript playerScript = thePlayer.GetComponent<CharacterScript>();
                audioSrc.PlayOneShot(playerHitSound);
                break;
            case "reflect":
                audioSrc.PlayOneShot(playerReflectSound);
                break;

        }



    }
}
