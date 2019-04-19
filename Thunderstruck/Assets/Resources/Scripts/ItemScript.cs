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
                    CharacterScript.redCoat = true;
                    CharacterScript.blueCoat = false;
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
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        CharacterScript script = player.GetComponent<CharacterScript>();

       script.body.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Artwork/BLUEFwdBody");
       //script.body.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Artwork/BLUEFwdBody");

        //script.forwaredBody = Resources.Load<Sprite>("Artwork/BLUEFwdBody");
        //script.holdingHand = Resources.Load<Sprite>("Artwork/BLUEShieldCoat");

    }

}
