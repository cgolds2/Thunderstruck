using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : BaseSprite {
    GameObject player;
    float speed;
    int health = 5;
    // Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
        speed = 2;
        base.BaseStart();

    }

    // Update is called once per frame
    void Update () {
        float step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        base.BaseUpdate();
    }
    // base.BaseUpdate_DestroyOnBoundsCheck(gameObject);
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag=="PlayerBullet"){
            health--;
            Destroy(col.gameObject);
            if(health<1){
                Destroy(gameObject);
            }
        }
    }
}
