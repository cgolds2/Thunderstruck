using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrellaScript : BaseSprite
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    Vector2 vel;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        vel = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        base.BaseUpdate();
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!(collision.gameObject.tag == "PlayerBullet" || collision.gameObject.tag == "EnemyBullet"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            return;
        }
        base.ReflectOther(collision);
        rb.velocity = vel;
    }
}
