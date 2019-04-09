using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class UmbrellaScript : BaseSprite
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    Vector2 vel;
    public GameObject spherePrefab;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        vel = new Vector2(0, 0);
        spherePrefab = Resources.Load<GameObject>("Sprites/sphere");
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
        SoundManagerScript.PlaySound("reflect");
        if (collision.gameObject.tag == "EnemyBullet")
        {
            GameObject shot = Instantiate(spherePrefab, collision.gameObject.transform.position, Quaternion.identity);
            shot.tag = "PlayerBullet";
            Physics2D.IgnoreCollision(shot.GetComponent<Collider2D>(), GetComponent<Collider2D>());

            Rigidbody2D rigidBody = shot.GetComponent<Rigidbody2D>();
            rigidBody.velocity = collision.rigidbody.velocity;
            Destroy(shot, 3f);
            Destroy(collision.gameObject);
        }
        rb.velocity = vel;
    }
}
