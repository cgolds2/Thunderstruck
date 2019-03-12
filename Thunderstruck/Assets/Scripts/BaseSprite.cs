using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSprite : MonoBehaviour {
    static float xBound;
    static float yBound;
  
	// Use this for initialization
	public void BaseStart () {
      

    }
	public static void SetBounds(float xBound, float yBound)
    {
        BaseSprite.xBound = xBound;
        BaseSprite.yBound = yBound;
    }

	// Update is called once per frame
	public void BaseUpdate() {

        
        if (transform.position.x <= MainScript.currentRoomX + -1 * xBound)
        {
            transform.position = new Vector2(MainScript.currentRoomX + (-1 * xBound), transform.position.y);
        }
        else if (transform.position.x >= MainScript.currentRoomX + (xBound))
        {
            transform.position = new Vector2(MainScript.currentRoomX + (xBound), transform.position.y);
        }

        // Y axis
        if (transform.position.y <= MainScript.currentRoomY + (-1 * yBound))
        {
            transform.position = new Vector2(transform.position.x, MainScript.currentRoomY + (-1 * yBound));
        }
        else if (transform.position.y >= MainScript.currentRoomY + ((float)yBound))
        {
            transform.position = new Vector2(transform.position.x, MainScript.currentRoomY + ((float)yBound));
        }
    }
}
