using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSprite : MonoBehaviour {

	// Use this for initialization
	public void BaseStart () {
		
	}
	
	// Update is called once per frame
	public void BaseUpdate() {
        
        if (transform.position.x <= -6.75f)
        {
            transform.position = new Vector2(-6.75f, transform.position.y);
        }
        else if (transform.position.x >= 6.75f)
        {
            transform.position = new Vector2(6.75f, transform.position.y);
        }

        // Y axis
        if (transform.position.y <= -3.05f)
        {
            transform.position = new Vector2(transform.position.x, -3.05f);
        }
        else if (transform.position.y >= 3.05f)
        {
            transform.position = new Vector2(transform.position.x, 3.05f);
        }
    }
}
