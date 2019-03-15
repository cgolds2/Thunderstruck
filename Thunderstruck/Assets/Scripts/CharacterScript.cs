using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : BaseSprite {
    public float panSpeed;
    public float health;
    public int iFrames;
    public GameObject spherePrefab;
    public Rigidbody2D bodyMC;

    // I don't know how to get the camera object to grab the resolution from it
    //Camera maincam = (Camera)GameObject.Find("MainCamera").GetComponent("Camera");
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

            bodyMC.velocity = new Vector3(0, 0, 0);
            Vector3 pos = transform.position;

            // Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

            //  bodyMC.velocity = new Vector2(movement.x *panSpeed, movement.y * panSpeed);
            if (Input.GetMouseButtonDown(0))
            {
                fire(pos,3);
            }
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

        //else
        //{
        //    bodyMC.velocity = new Vector2(0, 0);
        // }

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            health--;
        }else if(col.gameObject.tag == "Door")
        {
           var direction = col.gameObject.GetComponent<DoorScript>().Direction;
            MainScript.SetRoom(MainScript.currentRoom.GetRoomInt(direction));
        }
    }

    public void fire(Vector2 origin, float speed)
    {



        GameObject shot = Instantiate(spherePrefab, transform.position, Quaternion.identity);
        Physics2D.IgnoreCollision(shot.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        Vector3 shootDirection;
        shootDirection = Input.mousePosition;
        shootDirection.z = 0.0f;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection - transform.position;
        //...instantiating the rocket
        Rigidbody2D rigidBody =  shot.GetComponent<Rigidbody2D>();
        // Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
        float angle =90+ Mathf.Atan2(origin.x - shootDirection.x, origin.y - shootDirection.y) * Mathf.Rad2Deg;
        angle = angle*Mathf.PI / -180;
        Debug.Log(angle);
        

        float xUnit = Mathf.Cos(angle);
        float yUnit = Mathf.Sin(angle);

        rigidBody.velocity = new Vector2(xUnit * speed, yUnit * speed);
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
