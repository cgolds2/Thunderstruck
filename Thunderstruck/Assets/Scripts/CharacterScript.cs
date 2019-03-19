using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterScript : BaseSprite
{
    public float panSpeed;
    private float health;
    public int iFrames;
    public GameObject spherePrefab;
    public Rigidbody2D bodyMC;
    public GameObject shieldUmberella;
    public GameObject head;
    public GameObject body;
    public GameObject feet;
    PlayerBodyScript bodyScript;
    PlayerFeetScript feetScript;
    PlayerHeadScript headScript;

    // I don't know how to get the camera object to grab the resolution from it
    //Camera maincam = (Camera)GameObject.Find("MainCamera").GetComponent("Camera");
    // Use this for initialization
    void Start()
    {
        bodyScript = body.GetComponent<PlayerBodyScript>();
        feetScript = feet.GetComponent<PlayerFeetScript>();
        headScript = head.GetComponent<PlayerHeadScript>();

        panSpeed = 10;
        health = 5;
        iFrames = 0;
        spherePrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Sprites/sphere.prefab");
        Physics2D.IgnoreCollision(shieldUmberella.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), shieldUmberella.GetComponent<Collider2D>());
        shieldUmberella.SetActive(false);
        startRotation = shieldUmberella.transform.rotation;
        var size = shieldUmberella.GetComponent<PolygonCollider2D>().bounds.size;
        float umbrellaXOffset = size.x / 2;
        float umbrellaYOffset = size.y / 2;
        umbrellaOffset = new Vector3(umbrellaXOffset,umbrellaYOffset);
        base.BaseStart();
    }
    public float GetHeath()
    {
        return health;
    }
    public void SetHealth(float health)
    {
        this.health = health;
        if (health <= 0)
        {
            health = 0;
            //here
        }
    }
   
    public bool IsAlive()
    {
        return health > 0;

    }
    // Update is called once per frame
    void Update()
    {
        if (IsAlive())
        {
            shieldUmberella.transform.position = transform.position + umbrellaOffset;

            bodyMC.velocity = new Vector3(0, 0, 0);
            Vector3 pos = transform.position;

            // Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

            //  bodyMC.velocity = new Vector2(movement.x *panSpeed, movement.y * panSpeed);
            bool idle = true;
            if (Input.GetMouseButtonDown(0))
            {
                Fire(pos,5);
                SoundManagerScript.PlaySound("fire");
            }
            if (Input.GetKey("w"))
            {
                WalkDirection(1);
                idle = false;
                SoundManagerScript.PlaySound("walking");
                pos.y += panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("s"))
            {
                WalkDirection(3);
                idle = false;
                SoundManagerScript.PlaySound("walking");
                pos.y -= panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("d"))
            {
                idle = false;
                WalkDirection(0);

                SoundManagerScript.PlaySound("walking");
                pos.x += panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("a"))
            {
                idle = false;
                WalkDirection(2);

                SoundManagerScript.PlaySound("walking");
                pos.x -= panSpeed * Time.deltaTime;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                WalkDirection(-1);
            }
            transform.position = pos;
        }
        base.BaseUpdate();

    }
    public void WalkDirection(int direction)
    {
        bodyScript.Walk(direction);
        feetScript.Walk(direction);
        headScript.Walk(direction);

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            health--;
        }
        else if (col.gameObject.tag == "Door" && MainScript.currentRoom.numEnemies==0)
        {
            var direction = col.gameObject.GetComponent<DoorScript>().Direction;
            MainScript.SetRoom(MainScript.currentRoom.GetRoomInt(direction));
            switch (direction)
            {
                case 0:
                    //place at left
                    transform.position = new Vector3(
                        transform.position.x - (MainScript.mapWidth-2)/2,
                        transform.position.y,
                        transform.position.z);
                    break;
                case 1:
                    //place at bottom
                    transform.position = new Vector3(
                        transform.position.x,
                        transform.position.y - (MainScript.mapHeight - 2)/2,
                        transform.position.z);
                    break;
                case 2:
                    //place at right
                    transform.position = new Vector3(
                        transform.position.x + (MainScript.mapWidth - 2)/2,
                        transform.position.y,
                        transform.position.z);
                    break;
                case 3:
                    transform.position = new Vector3(
                        //place at top
                        transform.position.x,
                        transform.position.y + (MainScript.mapHeight - 2)/2,
                        transform.position.z);
                    break;
                default:
                    throw new System.Exception("Something went wrong with player door management");
            }
        }
    }

    public void Fire(Vector2 origin, float speed)
    {



        GameObject shot = Instantiate(spherePrefab, transform.position, Quaternion.identity);
        shot.tag = "PlayerBullet";
        Physics2D.IgnoreCollision(shot.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(shot.GetComponent<Collider2D>(), shieldUmberella. GetComponent<Collider2D>());

        Vector3 shootDirection;
        shootDirection = Input.mousePosition;
        shootDirection.z = 0.0f;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        float angle = 90 + Mathf.Atan2(origin.x - shootDirection.x, origin.y - shootDirection.y) * Mathf.Rad2Deg;
        angle = angle * Mathf.PI / -180;
       // Debug.Log(angle);


        float xUnit = Mathf.Cos(angle);
        float yUnit = Mathf.Sin(angle);

        Rigidbody2D rigidBody = shot.GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(xUnit * speed, yUnit * speed);
        Destroy(shot, 5f);
    }
}
