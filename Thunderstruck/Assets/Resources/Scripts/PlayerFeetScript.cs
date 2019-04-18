using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeetScript : MonoBehaviour
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
  
        Debug.Log(direction);
        if (direction == 5)
        {
            direction = -1;
        }
        animator.SetInteger("WalkState", direction);
    }
}
