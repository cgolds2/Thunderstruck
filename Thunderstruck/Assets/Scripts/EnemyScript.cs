﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyScript : BaseSprite {
    GameObject player;
    float speed;
    int health = 5;
    public GameObject spherePrefab;

    // Use this for initialization

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        spherePrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Sprites/tempSphere.prefab");

        speed = 2;
        base.BaseStart();
        StartTimer();
    }

    public void StartTimer()
    {
        InvokeRepeating("CallRepeat", 1, 1);
    }
    public void FireAtPlayer()
    {
        Fire(transform.position, 7);
    }

    public void Fire(Vector2 origin, float speed)
    {



        GameObject shot = Instantiate(spherePrefab, transform.position, Quaternion.identity);
        shot.tag = "EnemyBullet";
        Physics2D.IgnoreCollision(shot.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        Vector3 shootDirection;
        shootDirection = player.transform.position;
        shootDirection.z = 0.0f;
        float angle = 90 + Mathf.Atan2(origin.x - shootDirection.x, origin.y - shootDirection.y) * Mathf.Rad2Deg;
        Debug.Log(angle);

        angle = angle * Mathf.PI / -180;
     //   Debug.Log(angle);


        float xUnit = Mathf.Cos(angle);
        float yUnit = Mathf.Sin(angle);

        Rigidbody2D rigidBody = shot.GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(xUnit * speed, yUnit * speed);
        Destroy(shot, 5f);
    }

    public void CallRepeat()
    {
        FireAtPlayer();
    }
    // Update is called once per frame



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
                MainScript.DecreaseEnemyCount();
            }
        }
    }
}
