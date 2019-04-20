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
    public Sprite Yellow;
    public Sprite Red;
    public Sprite Blue;
    public SpriteRenderer rend;
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        vel = new Vector2(0, 0);
        spherePrefab = Resources.Load<GameObject>("Sprites/sphere");
    }

    // Update is called once per frame
    void Update()
    {
        base.BaseUpdate();
        
    }
    public enum Umberella{
        yellow,
        red,
        blue
    }
    public void SetUmberella(Umberella powerUp){
        switch (powerUp)
        {
            case Umberella.yellow:
                rend.sprite = Yellow;
                break;
            case Umberella.red:
                rend.sprite = Red;
                break;
            case Umberella.blue:
                rend.sprite = Blue;
                break;
        }
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
