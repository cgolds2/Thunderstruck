using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyScript : BaseSprite {
    GameObject player;
    float speed;
    float health = 5;
    public GameObject spherePrefab;
    float rateOfFire = 1f;
    float bulletSpeed = 7;
    IEnumerator blinkRoutine;
    int heartDropRate = 20;
    int heartDropRestoreValue = 2;
    Animator animator;
    public bool isKeyEnemy = false;

    public float Health { get => health; set => health = value; }
    public float RateOfFire { get => rateOfFire; set => rateOfFire = value; }
    public float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }

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
        Fire(transform.position, bulletSpeed);
    }

    public void FireInACircle(Vector2 origin, float speed, int numBullets)
    {
        int[] shots = new int[numBullets];
        int step = 360 / numBullets;
        for(int i = 0; i <= 360; i += step)
        {
            GameObject shot = Instantiate(spherePrefab, transform.position, Quaternion.identity);
            shot.tag = "EnemyBullet";
            Physics2D.IgnoreCollision(shot.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            float angle = i;
            Debug.Log(angle);

            angle = angle * Mathf.PI / -180;
            float xUnit = Mathf.Cos(angle);
            float yUnit = Mathf.Sin(angle);

            Rigidbody2D rigidBody = shot.GetComponent<Rigidbody2D>();
            rigidBody.velocity = new Vector2(xUnit * speed, yUnit * speed);
            Destroy(shot, 5f);

            numBullets--;
        }
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

        //float angle2 = Mathf.Atan2(origin.x - shootDirection.y, origin.y - shootDirection.x) * Mathf.Rad2Deg;
        //shot.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

         var v_diff = (player.transform.position - transform.position);
        var atan2 = Mathf.Atan2(v_diff.y, v_diff.x);
       shot.transform.rotation = Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg);

        //shot.transform.rotation = targetRotation;
        //Debug.Log(angle +":"+angle2);

        angle = angle * Mathf.PI / -180;
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
            CancelInvoke(); //things that are dead usually can't shoot at you....
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("EnemyCloudDeath"))
            {
                //
                //Debug.Log("Playing");
                played = true;
            }
            else
            {
                if (played)
                {
                    MainScript.DecreaseEnemyCount();

                    Destroy(gameObject);
                }
                //Debug.Log("NotPlaying");

            }
        }
      
    }
    // base.BaseUpdate_DestroyOnBoundsCheck(gameObject);
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag=="PlayerBullet"){
            TakeDamage();
            Destroy(col.gameObject);
            if(Health<=0){
             
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
        HUDScript.AddToScore(100);
        if (isKeyEnemy)
        {
            SpawnKey( gameObject.transform.position.x, gameObject.transform.position.y);
        }
        else
        {
            int dropPercent = heartDropRate;
            if (CharacterScript.redCoat)
                dropPercent = 50;
            if (Random.Range(0, 100) < dropPercent) // heartDropRate % chance to spawn heart
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
                 -1);
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

    public void tornadoUpdate() {
        if (MainScript.gameOver)
        {
            return;
        }
        if (Health > 0)
        {
            base.BaseUpdate();
        }
        else
        {
            CancelInvoke(); //things that are dead usually can't shoot at you....
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("EnemyCloudDeath"))
            {
                //
                //Debug.Log("Playing");
                played = true;
            }
            else
            {
                if (played)
                {
                    MainScript.DecreaseEnemyCount();

                    Destroy(gameObject);
                }
                //Debug.Log("NotPlaying");

            }
        }
    }
    public void TakeDamage()
    {
        Health--;
        if (CharacterScript.redUmbrella)
            Health-= .5f;
    }
}
