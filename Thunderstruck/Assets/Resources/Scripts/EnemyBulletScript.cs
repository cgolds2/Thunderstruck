using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : BaseSprite
{
    // Start is called before the first frame update
    int reflections = 500;
    Rigidbody2D rb;
    Vector2 zeroVec;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        zeroVec = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
   
        if (null != gameObject )
        {
            base.BaseUpdate_DestroyOnBoundsCheck(gameObject);
            //base.BaseUpdate_ReflectOnBoundsCheck(rb);
            base.BaseUpdate();
            if (rb.velocity == zeroVec) { Destroy(gameObject); }
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "EnemyBullet" || collision.gameObject.tag == "PlayerBullet")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            return;
        }
    }
}
