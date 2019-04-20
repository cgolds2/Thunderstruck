using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HailBossScript : EnemyScript
{
    // Start is called before the first frame update
    private int numHits = 0;
    void Start()
    {
        base.EnemyStart();
        Health = 20;
        RateOfFire = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        base.EnemyUpdate();
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "PlayerBullet")
        {
            numHits++;
            TakeDamage();
            Destroy(col.gameObject);
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
                if ((int)Health % 2 == 0 || numHits == 2)
                {
                    base.FireInACircle(transform.position, 7, 9);
                    numHits = 0;
                }
            }
        }
    }
}
