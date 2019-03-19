using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : BaseSprite
{
    // Start is called before the first frame update
    int reflections = 500;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        if (null != gameObject && gameObject.tag == "PlayerBullet")
        {
            //base.BaseUpdate_DestroyOnBoundsCheck(gameObject);
            base.BaseUpdate_ReflectOnBoundsCheck(rb);
            base.BaseUpdate();
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        else
        {
            //if (null != gameObject && gameObject.name == "sphere(Clone)")
            if (null != gameObject && collision.otherCollider.gameObject.tag == "PlayerBullet")
            {
                Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                return;
                //if (reflections > 0)
                //{
                //    //relfect
                //    //base.Reflect(collision);
                //    //reflections--;
                //}
                //else
                //{
                //    Destroy(gameObject);
                //}
                
            }
                
        }

    }
}
