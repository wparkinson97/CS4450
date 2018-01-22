﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour {

    private Rigidbody2D rb2d;
    private Vector2 vel;

    void GoBall()
    {
        float rand = Random.Range(0, 2);
        if(rand < 1)
        {
            rb2d.AddForce(new Vector2(20, -15));
        }
        else
        {
            rb2d.AddForce(new Vector2(-20, -15));
        }
    }

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        Invoke("GoBall", 2);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D col1)
    {
        if (col1.collider.CompareTag("Player"))
        {
            vel.x = rb2d.velocity.x;
            vel.y = rb2d.velocity.y / 2.0f + (col1.collider.attachedRigidbody.velocity.y / 3.0f);
            rb2d.velocity = vel;
        }
    }
    void RestartGame()
    {
        ResetBall();
        Invoke("GoBall", 1);
    }
    void ResetBall()
    {
        vel = Vector2.zero;
        rb2d.velocity = vel;
        transform.position = Vector2.zero;
    }
}
