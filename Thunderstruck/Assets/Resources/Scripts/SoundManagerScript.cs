using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

    public static AudioClip playerShootSound, playerHitSound, playerWalkingSound, playerDeathSound,playerReflectSound,
        playerPickupSound, playerPowerupSound, levelTwoSound;
    public static bool isPlaying;
    static AudioSource audioSrc;
    public HUDScript hudscript;
    public static bool themePlaying = false;
    public AudioClip Level2;
    public int level = 1;
    public GameObject[] levelaudio;

    //public CharacterScript playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerDeathSound = Resources.Load<AudioClip>("death");
        playerWalkingSound = Resources.Load<AudioClip>("footstep");
        playerShootSound = Resources.Load<AudioClip>("shoot");
        playerHitSound =  Resources.Load<AudioClip>("splah");
        playerReflectSound = Resources.Load<AudioClip>("Reflecting");
        playerPickupSound = Resources.Load<AudioClip>("pickup");
        playerPowerupSound = Resources.Load<AudioClip>("powerup");
        
        Level2 = Resources.Load<AudioClip>("LevelTwo");





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
                Debug.Log("Reflect");
                audioSrc.PlayOneShot(playerReflectSound);
                break;
            case "pickup":
                audioSrc.PlayOneShot(playerPickupSound);
                break;
            case "powerup":
                audioSrc.PlayOneShot(playerPowerupSound);
                break;

        }

    }
    public static void playTheme(int level)
    {
        switch (level)
        {
            case 1:
                themePlaying = true;
                GameObject level1audio = GameObject.FindGameObjectWithTag("Level1");
                level1audio.SetActive(true);
                break;
            case 2:
                themePlaying = true;
                //audioSrc.clip = Level2;
                audioSrc.Play();
                break;

        }
    }

}
