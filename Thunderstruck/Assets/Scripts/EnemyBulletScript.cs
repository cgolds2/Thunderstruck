﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : BaseSprite
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
   
        if (null != gameObject )
        {
            base.BaseUpdate_DestroyOnBoundsCheck(gameObject);
            //base.BaseUpdate_ReflectOnBoundsCheck(rb);
            base.BaseUpdate();
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
        }

    }
}