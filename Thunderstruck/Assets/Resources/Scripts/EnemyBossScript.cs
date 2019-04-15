using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBossScript : EnemyScript
{
    // Start is called before the first frame update
    void Start()
    {
        base.EnemyStart();
        Health = 10;
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
            Health--;
            Destroy(col.gameObject);
            if (Health < 1)
            {
                HUDScript.SetScore(HUDScript.GetScore() + 1000);

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
