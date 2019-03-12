using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSprite : MonoBehaviour {
    static float xBound;
    static float yBound;
    private Vector3 spriteSize;

  
	// Use this for initialization
	public void BaseStart () {
        spriteSize = GetComponent<Renderer>().bounds.size;

    }
	public static void SetBounds(float xBound, float yBound)
    {
        BaseSprite.xBound = xBound;
        BaseSprite.yBound = yBound;
    }

	// Update is called once per frame
	public void BaseUpdate() {

        
        if (transform.position.x - (spriteSize.x/2) <= MainScript.currentRoomX + -1 * xBound)
        {
            transform.position = new Vector3(
                MainScript.currentRoomX + (-1 * xBound) + (spriteSize.x / 2),
                transform.position.y,
                transform.position.z
                );
        }
        else if (transform.position.x + (spriteSize.x / 2) >= MainScript.currentRoomX + (xBound))
        {
            transform.position = new Vector3(
                MainScript.currentRoomX + (xBound) - (spriteSize.x / 2),
                transform.position.y,
                transform.position.z
                );
        }

        // Y axis
        if (transform.position.y - (spriteSize.y / 2) <= MainScript.currentRoomY + (-1 * yBound))
        {
            transform.position = new Vector3(
                transform.position.x, 
                MainScript.currentRoomY + (-1 * yBound) + (spriteSize.y / 2),
                transform.position.z
                );
        }
        else if (transform.position.y + (spriteSize.y / 2) >= MainScript.currentRoomY + ((float)yBound))
        {
            transform.position = new Vector3(
                transform.position.x, 
                MainScript.currentRoomY + ((float)yBound) - (spriteSize.y / 2),
                transform.position.z
                );
        }
    }
}
