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
            switch(Item)
            {
                case Items.blueCoat:
                    CharacterScript.blueCoat = true;
                    break;
                case Items.blueUmbrella:
                    CharacterScript.blueUmbrella = true;
                    break;
                case Items.redCoat:
                    CharacterScript.redCoat = true;
                    break;
                case Items.redUmbrella:
                    CharacterScript.redUmbrella = true;
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
}
