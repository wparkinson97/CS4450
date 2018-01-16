using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rb2d;

    public float speed;

    public Text countText;
    public Text winText;

    private int count;

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVerticle = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVerticle);
        rb2d.AddForce(movement * speed);
    }
	// Use this for initialization
	void Start () {
        count = 0;
        rb2d = GetComponent<Rigidbody2D>();
        countText.text = "Count " + count.ToString();
        winText.text = "";
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            count++;
            other.gameObject.SetActive(false);
            countText.text = "Count " + count.ToString();
            if(count >= 12)
            {
                winText.text = "you win";
            }
        }
    }
}
