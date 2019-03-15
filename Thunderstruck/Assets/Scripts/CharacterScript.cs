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
                Fire(pos,3);
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

    public void Fire(Vector2 origin, float speed)
    {



        GameObject shot = Instantiate(spherePrefab, transform.position, Quaternion.identity);
        Physics2D.IgnoreCollision(shot.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        Vector3 shootDirection;
        shootDirection = Input.mousePosition;
        shootDirection.z = 0.0f;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        float angle =90+ Mathf.Atan2(origin.x - shootDirection.x, origin.y - shootDirection.y) * Mathf.Rad2Deg;
        angle = angle*Mathf.PI / -180;
        Debug.Log(angle);
        

        float xUnit = Mathf.Cos(angle);
        float yUnit = Mathf.Sin(angle);

        Rigidbody2D rigidBody = shot.GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(xUnit * speed, yUnit * speed);
    }
}
