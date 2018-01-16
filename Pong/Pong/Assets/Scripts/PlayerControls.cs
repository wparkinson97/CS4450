using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    public KeyCode moveup = KeyCode.W;
    public KeyCode movedown = KeyCode.S;
    public float speed = 10.0f;

    public float boundY = 3.0f;

    private Rigidbody2D rb2d;


	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        var vel = rb2d.velocity;
        if (Input.GetKey(moveup))
        {
            vel.y = speed;
        }
        else if (Input.GetKey(movedown))
        {
            vel.y = -speed;
        }
        else if (!Input.anyKey)
        {
            vel.y = 0;
        }

        rb2d.velocity = vel;

        var pos = rb2d.transform.position;

        if(pos.y > boundY)
        {
            pos.y = boundY;
        }
        if (pos.y < -boundY)
        {
            pos.y = -boundY;
        }

        transform.position = pos;
    }
}
