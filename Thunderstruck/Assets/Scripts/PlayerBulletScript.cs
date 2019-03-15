using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : BaseSprite
{
    // Start is called before the first frame update
    float curPos = 0;
    float lastPos = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //kill the projectile if its no longer moving
        //curPos = transform.position.x;
        //if (curPos == lastPos)
        //{
        //    if (gameObject != null)
        //    Destroy(gameObject);
        //}
        //lastPos = curPos;
        base.BaseUpdate();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

    }
}
