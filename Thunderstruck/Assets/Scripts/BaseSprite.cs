using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSprite : MonoBehaviour {
    static float? xBound;
    static float? yBound;
	// Use this for initialization
	public void BaseStart () {
        if(xBound==null){
            xBound = (MainScript.mapWidth / 2)- MainScript.mapBorderWidth;
        }
        if (yBound == null)
        {
            yBound =  (MainScript.mapHeight / 2)- MainScript.mapBorderHeight;

        }

    }
	
	// Update is called once per frame
	public void BaseUpdate() {

        
        if (transform.position.x <= -1 * xBound)
        {
            transform.position = new Vector2((-1 * (float)xBound), transform.position.y);
        }
        else if (transform.position.x >= ((float)xBound))
        {
            transform.position = new Vector2(((float)xBound), transform.position.y);
        }

        // Y axis
        if (transform.position.y <= (-1 * (float)yBound))
        {
            transform.position = new Vector2(transform.position.x, (-1 * (float)yBound));
        }
        else if (transform.position.y >= ((float)yBound))
        {
            transform.position = new Vector2(transform.position.x, ((float)yBound));
        }
    }
}
