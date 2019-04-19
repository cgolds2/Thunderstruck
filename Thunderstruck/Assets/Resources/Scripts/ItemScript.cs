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
                    break;
                case Items.redCoat:
                    SetBodyRed();
                    break;
                case Items.redUmbrella:
                    CharacterScript.redUmbrella = true;
                    CharacterScript.blueUmbrella = false;
                    break;
                case Items.hat:
                    CharacterScript.hat = true;
                    break;
                case Items.boots:
                    CharacterScript.boots = true;
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

    }

    public static void SetBodyRed()
    {
        CharacterScript.blueCoat = false;
        CharacterScript.redCoat = true;
        PlayerBodyScript.playerSkin = PlayerBodyScript.Skin.red;

    }

}
