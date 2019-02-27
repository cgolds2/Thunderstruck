using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : BaseSprite {
    public float panSpeed;
    // Use this for initialization
    void Start () {
        panSpeed = 10;
	}
    

    // Update is called once per frame
    void Update () {
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
        base.BaseUpdate();

    }

}
