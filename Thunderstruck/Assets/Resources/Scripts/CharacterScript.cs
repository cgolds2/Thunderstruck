using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterScript : BaseSprite
{
    public float panSpeed;
    private float health;
    private float maxHealth;
    public int iFrames;
    public float fireRate;
    public float lastShot;
    public float damageGracePeriod;
    public float lastHitTaken;
    public GameObject spherePrefab;
    public Rigidbody2D bodyMC;
    public GameObject shieldUmberella;
    public GameObject umbrellaPivotPoint;
    public GameObject head;
    public GameObject body;
    public GameObject feet;
    public GameObject idleUmberella;
    public GameObject playerDeath;
    PlayerFeetScript feetScript;
    PlayerHeadScript headScript;
    public Quaternion startRotation;
    public Vector3 umbrellaOffset;
    PlayerBodyScript bodyScript;
    public Sprite forwaredBody;
    public Sprite holdingHand;
    bool played;
    float umbrellaXOffset = -.02f;
    float umbrellaYOffset = -0.419f;

    //item flags
    public static bool blueCoat, redCoat, redUmbrella, blueUmbrella, hat, boots = false;


    // I don't know how to get the camera object to grab the resolution from it
    //Camera maincam = (Camera)GameObject.Find("MainCamera").GetComponent("Camera");
    // Use this for initialization
    void Start()
    {
        bodyScript = body.GetComponent<PlayerBodyScript>();
        feetScript = feet.GetComponent<PlayerFeetScript>();
        headScript = head.GetComponent<PlayerHeadScript>();
        played = false;
        panSpeed = 10;
        health = 8;
        maxHealth = health;
        iFrames = 0;
        fireRate = .5f;
        lastShot = 0f;
        damageGracePeriod = .6f;
        lastHitTaken = 0f;
        spherePrefab = Resources.Load<GameObject>("Sprites/sphere");

        Physics2D.IgnoreCollision(shieldUmberella.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), shieldUmberella.GetComponent<Collider2D>());
        shieldUmberella.SetActive(false);
        startRotation = new Quaternion(0,0,0,0);
        shieldUmberella.transform.rotation = startRotation;
     
        umbrellaOffset = new Vector3(umbrellaXOffset,umbrellaYOffset, -1);
        base.BaseStart();
        playerDeath.GetComponent<Renderer>().enabled = false;
    }
    public float GetHeath()
    {
        return health;
    }
    public void KillPlayer()
    {
        playerDeath.transform.position = transform.position;
        MainScript.gameOver = true;
        head.SetActive(false);
        body.SetActive(false);
        feet.SetActive(false);
        idleUmberella.SetActive(false);
        playerDeath.SetActive(true);
        playerDeath.GetComponent<Animator>().SetTrigger("KillPlayer");
        playerDeath.GetComponent<Renderer>().enabled = true;

    }
    public void SetHealth(float health)
    {
        this.health = health;
        if (health <= 0)
        {
            health = 0;
            if (CharacterScript.hat == true) //dont die if wearing the hat
            {
                CharacterScript.hat = false;
                this.health = 1;
                FireInACircle(transform.position, 7, 9);
                HUDScript.SetHealth(this.health);
                return;
            }
            KillPlayer();
            SoundManagerScript.PlaySound("death");

            //SceneManager.LoadScene("Game Over");

            //here
        }
        HUDScript.SetHealth(health);
    }
   
    public bool IsAlive()
    {
        return health > 0;

    }
    // Update is called once per frame
    
    void Update()
    {
        if (!MainScript.gameOver)
        {
            



            bodyMC.velocity = new Vector3(0, 0, 0);
            Vector3 pos = transform.position;

            // Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

            //  bodyMC.velocity = new Vector2(movement.x *panSpeed, movement.y * panSpeed);
            bool isIdle = true;
            bool isUmbrellaActive = false;
            if (Input.GetMouseButton(0) && (Time.time > fireRate + lastShot))
            {
                if (!Input.GetKey(KeyCode.Space))
                {
                    Fire(pos, 5);
                    SoundManagerScript.PlaySound("fire");
                }
            }
            if (Input.GetKey("w"))
            {
                WalkDirection(1);
                isIdle = false;
                SoundManagerScript.PlaySound("walking");
                pos.y += panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("s"))
            {
                WalkDirection(3);
                isIdle = false;
                SoundManagerScript.PlaySound("walking");
                pos.y -= panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("d"))
            {
                isIdle = false;
                WalkDirection(0);

                SoundManagerScript.PlaySound("walking");
                pos.x += panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("a"))
            {
                isIdle = false;
                WalkDirection(2);

                SoundManagerScript.PlaySound("walking");
                pos.x -= panSpeed * Time.deltaTime;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                shieldUmberella.SetActive(true);
                idleUmberella.SetActive(false);
                isUmbrellaActive = true;
                //body.GetComponent<SpriteRenderer>().sprite = holdingHand;

                //Vector3 shootDirection;
                //shootDirection = Input.mousePosition;
                //shootDirection.z = 0.0f;
                //shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
                //float angle = -1 * Mathf.Atan2(shieldUmberella.transform.position.x - shootDirection.x, shieldUmberella.transform.position.y - shootDirection.y) * Mathf.Rad2Deg;
                ////angle = angle * Mathf.PI / -180;
                ////float xUnit = Mathf.Cos(angle);
                ////float yUnit = Mathf.Sin(angle);
                //shieldUmberella.transform.rotation = Quaternion.Euler(shieldUmberella.transform.rotation.x , shieldUmberella.transform.rotation.x, angle);
            }
            if (Input.GetKey(KeyCode.Space))
            {
                shieldUmberella.transform.Rotate(Vector3.forward * 230 * Time.deltaTime);
                isUmbrellaActive = true;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                shieldUmberella.SetActive(false);
                shieldUmberella.transform.rotation = startRotation;
                idleUmberella.SetActive(true);
                isUmbrellaActive = false ;

            }
            if (isIdle)
            {
                umbrellaOffset = new Vector3(umbrellaXOffset, umbrellaYOffset, -1);

                if (isUmbrellaActive)
                {
                    WalkDirection(5);

                }
                else
                {
                    WalkDirection(-1);

                }
            }
            else
            {
                umbrellaOffset = new Vector3(umbrellaXOffset, umbrellaYOffset, 0);

            }
            transform.position = pos;
        }
        else
        {
            if(playerDeath.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("PlayerDeath")){
                //
                Debug.Log("Playing");
                played = true;
            }
            else
            {
                if (played)
                {
                    SceneManager.LoadScene("Game Over");
                }
                Debug.Log("NotPlaying");

            }
        }
        
        shieldUmberella.transform.position = transform.position + umbrellaOffset;

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
        if ((col.gameObject.tag == "Enemy" || col.gameObject.tag == "EnemyBullet") && GetHeath() > 0)
        {
            if (Time.time > damageGracePeriod + lastHitTaken)
            {
                float damage = 1;
                if (CharacterScript.blueUmbrella || CharacterScript.redUmbrella)
                    damage = damage * 1.25f;
                if (CharacterScript.blueCoat)
                    damage = damage * .75f;

                SetHealth(health - damage);
                SoundManagerScript.PlaySound("hit");
                lastHitTaken = Time.time;

                if (GetHeath() > 0)
                {
                    var croutine = base.BlinkGameObject(gameObject, 4, damageGracePeriod / 6);
                    StartCoroutine(croutine);
                }
            }
        }
        else if (col.gameObject.tag == "Door" && MainScript.currentRoom.numEnemies<=0)
        {
            var direction = col.gameObject.GetComponent<DoorScript>().Direction;
            var targetRoom = MainScript.currentRoom.GetRoomInt(direction);
            if (targetRoom.isRoomLocked)
            {
                if (targetRoom.roomType == Assets.Scripts.RoomType.Item && HUDScript.GetKeyStatus() && targetRoom.numEnemies==0)
                {
                    HUDScript.SetKey(false);
                    targetRoom.isRoomLocked = false;
                    MainScript.currentRoom.SetDoors(true);
                    //targetRoom.SetDoors(true);

                }
                else
                {
                    return;
                }
            }
           
            MainScript.SetRoom(targetRoom);
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
        else if(col.gameObject.tag == "Heart")
        {
            HeartScript HS = col.gameObject.GetComponent<HeartScript>();
            SoundManagerScript.PlaySound("pickup");
            SetHealth(Mathf.Min(health + HS.restoreValue, maxHealth)); //never go over max hp
            Destroy(col.gameObject);
        }
        else if (col.gameObject.tag == "Key")
        {
            HUDScript.SetKey(true);

            Destroy(col.gameObject);
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
        Destroy(shot, 3f);

        lastShot = Time.time;
    }

    public void FireInACircle(Vector2 origin, float speed, int numBullets)
    {
        int[] shots = new int[numBullets];
        int step = 360 / numBullets;
        for (int i = 0; i <= 360; i += step)
        {
            GameObject shot = Instantiate(spherePrefab, transform.position, Quaternion.identity);
            shot.tag = "PlayerBullet";
            Physics2D.IgnoreCollision(shot.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            Physics2D.IgnoreCollision(shot.GetComponent<Collider2D>(), shieldUmberella.GetComponent<Collider2D>());

            float angle = i;
            angle = angle * Mathf.PI / -180;
            float xUnit = Mathf.Cos(angle);
            float yUnit = Mathf.Sin(angle);

            Rigidbody2D rigidBody = shot.GetComponent<Rigidbody2D>();
            rigidBody.velocity = new Vector2(xUnit * speed, yUnit * speed);
            Destroy(shot, 3f);
            lastShot = Time.time;

            numBullets--;
        }
    }
}
