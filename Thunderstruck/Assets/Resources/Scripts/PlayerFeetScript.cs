using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerFeetScript : MonoBehaviour
{
    private new SpriteRenderer renderer;
    public SpriteCollection bootsForward;
    public SpriteCollection bootsLeft;
    public Sprite[] bootsLeftSprites;
    public Sprite[] bootsForwardSprites;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
         bootsForward.sprites = Resources.LoadAll<Sprite>("Artwork/" + bootsForward.sheet.name);
        bootsLeft.sprites = Resources.LoadAll<Sprite>("Artwork/" + bootsLeft.sheet.name);

    }

    // Update is called once per frame
    void Update()
    {
    }
    void LateUpdate()
    {
        //Select the correct sprite
        var state = animator.GetInteger("WalkState");
        if (CharacterScript.boots)
        {
            string spriteName = renderer.sprite.name.Split('_').Last();


            switch (state)
            {
                case 0:
                //left
                case 2:
                    //right
                    SpriteCollection coll = bootsLeft;

                    //Get the name


                    //Search for the correct name
                    if (coll == null || coll.sprites == null) return;

                    Sprite newSprite = coll.sprites.Where(item => item.name.Split('_').Last() == spriteName).ToArray()[0];

                    //Set the sprite
                    if (newSprite)
                        renderer.sprite = newSprite;
                    break;
                case -1:
                    //idle
                    renderer.sprite = bootsForward.sprites[0];
                    break;
                case 1:
                //up:
                case 3:
                    //down
                    SpriteCollection col2 = bootsForward;

                    //Get the name


                    //Search for the correct name
                    if (col2 == null || col2.sprites == null) return;

                    Sprite newSprite2 = col2.sprites.Where(item => item.name.Split('_').Last() == spriteName).ToArray()[0];

                    //Set the sprite
                    if (newSprite2)
                        renderer.sprite = newSprite2;
                    break;
          

            }
        }
    }

    public void Walk(int direction)
    {
  
        //Debug.Log(direction);
        if (direction == 5)
        {
            direction = -1;
        }
        animator.SetInteger("WalkState", direction);
    }
}
