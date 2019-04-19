using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadScript : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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
    }
}
