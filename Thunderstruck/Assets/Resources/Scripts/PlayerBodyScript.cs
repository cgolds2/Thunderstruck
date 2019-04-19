using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerBodyScript : MonoBehaviour
{

    Animator animator;
    private new SpriteRenderer renderer;
    public List<SpriteCollection> skinsLeft = new List<SpriteCollection>();
    public SpriteCollection blueBodyLeft;
    public SpriteCollection yellowBodyLeft;
    public SpriteCollection redBodyLeft;

    public Sprite blueBodyForward;
    public Sprite yellowBodyForward;
    public Sprite redBodyForward;
    public List<Sprite> skinsForward = new List<Sprite>();

    public Sprite blueBodyTwirl;
    public Sprite yellowBodyTwirl;
    public Sprite redBodyTwirl;
    public List<Sprite> skinsTwirl = new List<Sprite>();


    public static Skin playerSkin;
    //0 is yellow
    //1 is blue
    //2 is red
    public enum Skin{
        yellow,
        blue,
        red
    }
 
    void Start()
    {
        animator = GetComponent<Animator>();

        renderer = GetComponent<SpriteRenderer>();

        //First thing we must do is load all the sprites for the character
        skinsLeft.Add(yellowBodyLeft);
        skinsLeft.Add(blueBodyLeft);
        skinsLeft.Add(redBodyLeft);

        foreach (SpriteCollection coll in skinsLeft){
            var thesprites = Resources.LoadAll<Sprite>("Artwork/" + coll.sheet.name);
            coll.sprites = thesprites;
        }

        skinsForward.Add(yellowBodyForward);
        skinsForward.Add(blueBodyForward);
        skinsForward.Add(redBodyForward);

        skinsTwirl.Add(yellowBodyTwirl);
        skinsTwirl.Add(blueBodyTwirl);
        skinsTwirl.Add(redBodyTwirl);
        playerSkin = Skin.yellow;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Walk(int direction)
    {
      
        //Debug.Log(direction);
        animator.SetInteger("WalkState", direction);
    }
    void LateUpdate()
    {
        //Select the correct sprite
        var state = animator.GetInteger("WalkState");
        string spriteName = renderer.sprite.name.Split('_').Last();

        switch (state){
            case 0:
                //left
            case 2:
                //right
                SpriteCollection coll = skinsLeft[(int)playerSkin];

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
            case 1:
                //up:
            case 3:
                //down
                renderer.sprite = skinsForward[(int)playerSkin];

                break;
            case 5:
                //twirl
                renderer.sprite = skinsTwirl[(int)playerSkin];
                break;

        }


    }
}

 [System.Serializable]
public class SpriteCollection
{
    public string name;
    public Texture sheet;

    [System.NonSerialized]
    public Sprite[] sprites;
}
