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
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            CharacterScript script = player.GetComponent<CharacterScript>();
            switch (Item)
            {
                case Items.blueCoat:
                    SetBodyBlue();
                    break;
                case Items.blueUmbrella:
                    CharacterScript.blueUmbrella = true;
                    CharacterScript.redUmbrella = false;
                    HUDScript.blueUmb.GetComponent<SpriteRenderer>().material = HUDScript.normal;
                    break;
                case Items.redCoat:
                    SetBodyRed();
                    break;
                case Items.redUmbrella:
                    CharacterScript.redUmbrella = true;
                    CharacterScript.blueUmbrella = false;
                    HUDScript.redUmb.GetComponent<SpriteRenderer>().material = HUDScript.normal;
                    break;
                case Items.hat:
                    CharacterScript.hat = true;
                    HUDScript.yellowHat.GetComponent<SpriteRenderer>().material = HUDScript.normal;
                    break;
                case Items.boots:
                    CharacterScript.boots = true;
                    HUDScript.redBoots.GetComponent<SpriteRenderer>().material = HUDScript.normal;
                    break;
            }

            Destroy(gameObject);
        }
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

}
