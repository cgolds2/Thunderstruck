using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBossScript : EnemyScript
{
    public GameObject otherBullet;
    // Start is called before the first frame update
    void Start()
    {
        base.EnemyStart();
        Health = 10;
        if(otherBullet != null){
            base.spherePrefab = otherBullet;

        }
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
            }
        }
    }
}
