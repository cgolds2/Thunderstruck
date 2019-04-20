using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadScript : MonoBehaviour
{
    Animator animator;
    public GameObject hat;
    public Sprite hatForward;
    public Sprite hatLeft;
    public Sprite hatRight;
    public Sprite hatBack;
    public Sprite[] hats;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        hats = new Sprite[] { hatRight,hatBack,hatLeft,hatForward };
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Walk(int direction)
    {
        if (direction == 5)
        {
            direction = -1;
        }
        //Debug.Log(direction);
        animator.SetInteger("WalkState", direction);
        int hatDir = direction;
        if(hatDir < 0 || hatDir > 3){
            hatDir = 3;
        }
        if(CharacterScript.hat){
            hat.SetActive(true);

            hat.GetComponent<SpriteRenderer>().sprite = hats[hatDir];

        }else{
            hat.SetActive(false);
        }
    }
}
