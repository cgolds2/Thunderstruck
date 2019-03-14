using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphereScript : MonoBehaviour
{
    public Rigidbody2D proj;
    public GameObject spherePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {



    }


   public void fire(Vector2 origin)
    {
        Vector2 angle = GetFireAngle(origin);

        GameObject shot = Instantiate(spherePrefab, transform.position, Quaternion.identity);
        proj.transform.position = new Vector3(origin.x, origin.y, 0);
        proj.velocity = new Vector3(angle.x, angle.y, 0);
        
    }
    Vector2 GetFireAngle(Vector3 charpos)
    {
        Vector3 mPos = Input.mousePosition;
        // Debug.Log(mPos.x);
        // Debug.Log(mPos.y);
        // Debug.Log(i.transform.position.x);
        //Debug.Log(i.transform.position.y);

        //Debug.Log(maincam.pixelWidth);
        //Debug.Log(maincam.pixelHeight);    
        //since I don't know the res, i'll assume 1920 1080

        Vector2 charSc = new Vector2(10, 5); //i.e. the size of the character's range
        Vector2 mouseSc = new Vector2(1920, 1080); //i.e. my assumed resolution, if you know how to code this in, assign it here
        //to get the angle, I'll scale the pointer to the units of the character
        Vector2 mSC = new Vector2(
                    /*x*/((mPos.x * charSc.x * 2 / mouseSc.x) - charSc.x),
                    /*y*/(mPos.y * charSc.y * 2 / mouseSc.y) - charSc.y);

        //  Debug.Log(mSC.x);
        //  Debug.Log(mSC.y);
        return new Vector2(charpos.x - mSC.x, charpos.y - mSC.y);
    }
}
