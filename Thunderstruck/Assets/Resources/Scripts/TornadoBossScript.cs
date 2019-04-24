using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TornadoBossScript : EnemyScript
{
    // Start is called before the first frame update
    private int numHits = 0;
    public int iFrames;
    public float damageGracePeriod;
    public float lastHitTaken;
    void Start()
    {
        base.EnemyStart();
        Health = 30;
        RateOfFire = .5f;
        BulletSpeed = 10f;
        iFrames = 0;
        damageGracePeriod = .6f;
        lastHitTaken = 0f;

        InvokeRepeating("CallSpawnTornados", 5, 15);
    }

    // Update is called once per frame
    void Update()
    {
        base.tornadoUpdate();
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "PlayerBullet")
        {
            if (Time.time > damageGracePeriod + lastHitTaken)
            {
                TakeDamage();
                numHits++;
                
                if (Health <= 0)
                {
                    HUDScript.AddToScore(1000);
                    if (!MainScript.gameOver)
                        SceneManager.LoadScene("Level Complete");

                }
                else
                {
                    var croutine = base.BlinkGameObject(gameObject, 2, .1f);
                    StartCoroutine(croutine);
                    if (Health % 5 == 0 || numHits == 5)
                    {
                        base.FireInACircle(transform.position, 4, 15);
                        numHits = 0;
                    }
                }
                lastHitTaken = Time.time;
            }
            Destroy(col.gameObject);
        }
    }

    public void CallSpawnTornados()
    {
        if (MainScript.gameOver)
        {
            return;
        }
        SpawnTornados();
    }

    public void SpawnTornados() {
        int direc = Random.Range(0, 4); //0 top, 1 left, 2 down, 3 right
        Vector3 direction =  new Vector3(0, 0, 0);

        float xLoc = MainScript.currentRoomX;
        float yLoc = MainScript.currentRoomY;
        //get top left corner
        xLoc -= MainScript.mapWidth * .5f;
        yLoc += MainScript.mapHeight * .5f;

        var startX = xLoc;
        var startY = yLoc;
        switch (direc)
        {
            case 0:
                direction = new Vector3(0, 1, 0);
                yLoc -= MainScript.mapHeight;
                for (; xLoc < (startX + MainScript.mapWidth); xLoc += .8f)
                {
                    var x = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Sprites/MiniTornado"));
                    x.transform.position = new Vector3(xLoc, yLoc, -1);
                    x.GetComponent<TornadoScript>().direction = direction * 10;
                }
                break;
            case 1:
                direction = new Vector3(-1, 0, 0);
                xLoc += MainScript.mapWidth;
                for (; yLoc > (startY - MainScript.mapHeight); yLoc -= .8f)
                {
                    var x = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Sprites/MiniTornado"));
                    x.transform.position = new Vector3(xLoc, yLoc, -1);
                    x.GetComponent<TornadoScript>().direction = direction * 10;
                }
                break;
            case 2:
                direction = new Vector3(0, -1, 0);
                for (; xLoc < (startX + MainScript.mapWidth); xLoc += .8f)
                {
                    var x = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Sprites/MiniTornado"));
                    x.transform.position = new Vector3(xLoc, yLoc, -1);
                    x.GetComponent<TornadoScript>().direction = direction * 10;
                }
                break;
            case 3:
                direction = new Vector3(1, 0, 0);
                for (; yLoc > (startY - MainScript.mapHeight); yLoc -= .8f)
                {
                    var x = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Sprites/MiniTornado"));
                    x.transform.position = new Vector3(xLoc, yLoc, -1);
                    x.GetComponent<TornadoScript>().direction = direction * 10;
                }
                break;
        }
    }
}
