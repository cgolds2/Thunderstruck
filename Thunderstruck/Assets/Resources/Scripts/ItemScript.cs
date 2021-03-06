﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public Items Item { get; set; }


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SoundManagerScript.PlaySound("powerup");

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            CharacterScript script = player.GetComponent<CharacterScript>();
            switch (Item)
            {
                case Items.blueCoat:
                    SetBodyBlue();
                    SoundManagerScript.PlaySound("powerup");
                    break;
                case Items.blueUmbrella:
                    SetUmberellaBlue(script);
                    break;
                case Items.redCoat:
                    SetBodyRed();
                    SoundManagerScript.PlaySound("powerup");
                    break;
                case Items.redUmbrella:
                    SetUmberellaRed(script);
                    break;
                case Items.hat:
                    SetYellowHat();
                    break;
                case Items.boots:
                    SetRedBoots();
                    break;
            }

            Destroy(gameObject);
        }
    }

    public static void SetYellowHat(){
        CharacterScript.hat = true;
        HUDScript.yellowHat.GetComponent<SpriteRenderer>().material = HUDScript.normal;
    }

    public static void SetRedBoots(){
        CharacterScript.boots = true;
       HUDScript.redBoots.GetComponent<SpriteRenderer>().material = HUDScript.normal;
    }
    public static void SetBodyBlue()
    {
        CharacterScript.blueCoat = true;
        CharacterScript.redCoat = false;
        PlayerBodyScript.playerSkin = PlayerBodyScript.Skin.blue;
        HUDScript.blueCoat.GetComponent<SpriteRenderer>().material = HUDScript.normal;
        HUDScript.redCoat.GetComponent<SpriteRenderer>().material = HUDScript.greyed;


    }

    public static void SetBodyRed()
    {


        CharacterScript.blueCoat = false;
        CharacterScript.redCoat = true;
        PlayerBodyScript.playerSkin = PlayerBodyScript.Skin.red;
        HUDScript.blueCoat.GetComponent<SpriteRenderer>().material = HUDScript.greyed;
        HUDScript.redCoat.GetComponent<SpriteRenderer>().material = HUDScript.normal;

    }

    internal static void SetBodyYellow()
    {
        PlayerBodyScript.playerSkin = PlayerBodyScript.Skin.yellow;
        HUDScript.blueCoat.GetComponent<SpriteRenderer>().material = HUDScript.greyed;
        HUDScript.redCoat.GetComponent<SpriteRenderer>().material = HUDScript.greyed;
    }

    public static void SetUmberellaBlue(CharacterScript c)
    {

        CharacterScript.redUmbrella = false;
        CharacterScript.blueUmbrella = true;
        HUDScript.redUmb.GetComponent<SpriteRenderer>().material = HUDScript.greyed;
        HUDScript.blueUmb.GetComponent<SpriteRenderer>().material = HUDScript.normal;
        c.shieldUmberella.GetComponent<UmbrellaScript>().SetUmberella(UmbrellaScript.Umberella.blue);
        c.idleUmberella.GetComponent<SpriteRenderer>().sprite = c.IdleUmbBlue;

    }

    public static void SetUmberellaRed(CharacterScript c)
    {
        CharacterScript.redUmbrella = true;
        CharacterScript.blueUmbrella = false;
        HUDScript.redUmb.GetComponent<SpriteRenderer>().material = HUDScript.normal;
        HUDScript.blueUmb.GetComponent<SpriteRenderer>().material = HUDScript.greyed;
        c.shieldUmberella.GetComponent<UmbrellaScript>().SetUmberella(UmbrellaScript.Umberella.red);
        c.idleUmberella.GetComponent<SpriteRenderer>().sprite = c.IdleUmbRed;

    }
    public static void SetUmberellaYellow(CharacterScript c)
    {
        CharacterScript.redUmbrella = true;
        CharacterScript.blueUmbrella = false;
        HUDScript.redUmb.GetComponent<SpriteRenderer>().material = HUDScript.greyed;
        HUDScript.blueUmb.GetComponent<SpriteRenderer>().material = HUDScript.greyed;
        c.shieldUmberella.GetComponent<UmbrellaScript>().SetUmberella(UmbrellaScript.Umberella.yellow);
        c.idleUmberella.GetComponent<SpriteRenderer>().sprite = c.IdleUmbYellow;

    }
}
