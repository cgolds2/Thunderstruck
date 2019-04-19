using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerBodyScript : MonoBehaviour
{

    Animator animator;
    private new SpriteRenderer renderer;
    public List<SpriteCollection> skins = new List<SpriteCollection>();
    public SpriteCollection blueBodyLeft;
    public SpriteCollection yellowBodyLeft;
    public int skin = 0;

 
    void Start()
    {
        animator = GetComponent<Animator>();

        renderer = GetComponent<SpriteRenderer>();

        //First thing we must do is load all the sprites for the character
        skins.Add(blueBodyLeft);
        skins.Add(yellowBodyLeft);

        foreach (SpriteCollection coll in skins){
            var thesprites = Resources.LoadAll<Sprite>("Artwork/" + coll.sheet.name);
            coll.sprites = thesprites;
        }

         
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
        SpriteCollection coll = skins[this.skin];

        //Get the name
        string spriteName = renderer.sprite.name.Split('_').Last();
        int res = -1;
        if (!int.TryParse(spriteName, out res)){
            return;
        }
      

        //Search for the correct name
        if (coll == null || coll.sprites == null) return;

        Sprite newSprite = coll.sprites.Where(item => item.name.Split('_').Last() == spriteName).ToArray()[0];

        //Set the sprite
        if (newSprite)
            renderer.sprite = newSprite;
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
