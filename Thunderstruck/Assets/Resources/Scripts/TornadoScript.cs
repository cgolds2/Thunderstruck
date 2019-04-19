using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoScript : EnemyScript
{
    // Start is called before the first frame update
    public Vector3 direction = new Vector3(0,0,0);
    float speed_torn = 2;
    void Start()
    {
        Health = 1;
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed_torn * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, transform.position+ direction, step);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "PlayerBullet")
        {
            TakeDamage();
            Destroy(col.gameObject);
            if (Health <= 0)
            {

                Destroy(gameObject);
                return;
            }
            else
            {
                var croutine = base.BlinkGameObject(gameObject, 2, .1f);
                StartCoroutine(croutine);
            }
        }
    }
}
