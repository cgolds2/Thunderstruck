using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyScript : BaseSprite {
    GameObject player;
    float speed;
    int health = 5;
    public GameObject spherePrefab;
    float rateOfFire = 1f;
    IEnumerator blinkRoutine;
    int heartDropRate = 50;
    int heartDropRestoreValue = 2;
    Animator animator;
    public bool isKeyEnemy = false;

    public int Health { get => health; set => health = value; }
    public float RateOfFire { get => rateOfFire; set => rateOfFire = value; }

    // Use this for initialization
    public void EnemyStart(){
        Start();
    }
    public void EnemyUpdate(){
        Update();
    }
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        spherePrefab = Resources.Load<GameObject>("Sprites/tempSphere");
        animator = GetComponent<Animator>();

        speed = 2;
        base.BaseStart();
        StartTimer();
        blinkRoutine = base.BlinkGameObject(gameObject, 2, .2f);
    }

    public void StartTimer()
    {
        InvokeRepeating("CallRepeat", rateOfFire, 1);
    }
    public void FireAtPlayer()
    {
        Fire(transform.position, 7);
    }

    public void Fire(Vector2 origin, float speed)
    {



        GameObject shot = Instantiate(spherePrefab, transform.position, Quaternion.identity);
        shot.tag = "EnemyBullet";
        Physics2D.IgnoreCollision(shot.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        Vector3 shootDirection;
        shootDirection = player.transform.position;
        shootDirection.z = 0.0f;
        float angle = 90 + Mathf.Atan2(origin.x - shootDirection.x, origin.y - shootDirection.y) * Mathf.Rad2Deg;
        Debug.Log(angle);

        angle = angle * Mathf.PI / -180;
     //   Debug.Log(angle);


        float xUnit = Mathf.Cos(angle);
        float yUnit = Mathf.Sin(angle);

        Rigidbody2D rigidBody = shot.GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(xUnit * speed, yUnit * speed);
        Destroy(shot, 5f);
    }

    public void CallRepeat()
    {
        if (MainScript.gameOver)
        {
            return;
        }
        FireAtPlayer();
    }
    // Update is called once per frame

    bool played = false;

    // Update is called once per frame
    void Update () {
        if (MainScript.gameOver)
        {
            return;
        }
        if (Health > 0)
        {
            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
            base.BaseUpdate();
        }
        else
        {
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("EnemyCloudDeath"))
            {
                //
                Debug.Log("Playing");
                played = true;
            }
            else
            {
                if (played)
                {
                    Destroy(gameObject);
                }
                Debug.Log("NotPlaying");

            }
        }
      
    }
    // base.BaseUpdate_DestroyOnBoundsCheck(gameObject);
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag=="PlayerBullet"){
            Health--;
            Destroy(col.gameObject);
            if(Health<1){
             
                KillObject();
            }
            else
            {
                var croutine = base.BlinkGameObject(gameObject, 2, .1f);
                StartCoroutine(croutine);
            }
        }
        else if (col.gameObject.tag == "EnemyBullet")
        {
            Physics2D.IgnoreCollision(col.collider, GetComponent<Collider2D>());
        }
    }

    public void KillObject()
    {
        HUDScript.SetScore(HUDScript.GetScore() + 100);
        MainScript.DecreaseEnemyCount();
        if (isKeyEnemy)
        {
            SpawnKey( gameObject.transform.position.x, gameObject.transform.position.y);
        }
        else
        {
            if (Random.Range(0, 100) < heartDropRate) // heartDropRate % chance to spawn heart
            {
                SpawnHeart(heartDropRestoreValue, gameObject.transform.position.x, gameObject.transform.position.y);
            }
        }
     
        animator.SetTrigger("KillCloud");

    }
    public void SpawnKey(float xLoc, float yLoc)
    {
        var keyAsset = Resources.Load<GameObject>("Sprites/keyPlaceHolder");
        GameObject key = UnityEngine.Object.Instantiate(keyAsset);
        Vector3 position = new Vector3(
                 xLoc,
                 yLoc,
                 0);
        key.transform.position = position;
    }
    public void SpawnHeart(int restoreValue, float xLoc, float yLoc)
    {
        var heartAsset = Resources.Load<GameObject>("Sprites/heart");
        GameObject heart = UnityEngine.Object.Instantiate(heartAsset);
        heart.tag = "Heart";
        HeartScript HS = heart.GetComponent<HeartScript>();
        HS.restoreValue = restoreValue;
        Vector3 position = new Vector3(
                 xLoc,
                 yLoc,
                 0);
        heart.transform.position = position;
    }
}
