using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {


    public KeyCode moveUp;
    public KeyCode moveDown;
    public KeyCode moveLeft;
    public KeyCode moveRight;

    public float playerSpeed;
    public Vector2 velocity = new Vector2(0.0f, 0.0f);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(moveUp))
        {
            transform.Translate(0.0f, playerSpeed, 0.0f);
        
        }
        if (Input.GetKey(moveDown))
        {
            transform.Translate(0.0f, -playerSpeed, 0.0f);
        }
        if (Input.GetKey(moveLeft))
        {
            transform.Translate(-playerSpeed, 0.0f, 0.0f);
        }
        if (Input.GetKey(moveRight))
        {
            transform.Translate(playerSpeed, 0.0f, 0.0f);
        }
        
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "friend")
        {
            other.gameObject.SetActive(false);
            GameControl.Score += 5;
        }
    }
}
