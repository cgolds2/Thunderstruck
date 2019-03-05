using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : BaseSprite {
    public float panSpeed;
    public float health;
    public int iFrames;
    // Use this for initialization
    void Start () {
        panSpeed = 10;
        health = 5;
        iFrames = 0;
        base.BaseStart();
	}
    
    public bool IsAlive()
    {
        return health > 0;

    }
    // Update is called once per frame
    void Update () {
        if (IsAlive())
        {
            Vector3 pos = transform.position;

            if (Input.GetKey("w"))
            {
                pos.y += panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("s"))
            {
                pos.y -= panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("d"))
            {
                pos.x += panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("a"))
            {
                pos.x -= panSpeed * Time.deltaTime;
            }
            transform.position = pos;
        }
        base.BaseUpdate();


    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            health--;
        }
    }
}
