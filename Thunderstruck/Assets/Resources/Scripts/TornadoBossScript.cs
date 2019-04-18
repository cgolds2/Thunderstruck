using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TornadoBossScript : HailBossScript
{
    // Start is called before the first frame update
    void Start()
    {
        base.EnemyStart();
        Health = 30;
        RateOfFire = .5f;
        BulletSpeed = 10f;
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
            TakeDamage();
            Destroy(col.gameObject);
            if (Health <= 0)
            {
                HUDScript.AddToScore(1000);
                SceneManager.LoadScene("Level Complete");

            }
            else
            {
                var croutine = base.BlinkGameObject(gameObject, 2, .1f);
                StartCoroutine(croutine);
                if (Health % 5 == 0)
                {
                    base.FireInACircle(transform.position, 4, 15);
                }
            }
        }
    }
}
