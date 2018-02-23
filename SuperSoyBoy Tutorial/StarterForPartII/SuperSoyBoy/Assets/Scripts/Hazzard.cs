﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazzard : MonoBehaviour {

    public GameObject playerDeathPrefab;
    public AudioClip deathClip;
    public Sprite hitSprite;
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start () {
		
	}
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        // 1
        if (coll.transform.tag == "Player")
        {
            // 2
            var audioSource = GetComponent<AudioSource>();
            if (audioSource != null && deathClip != null)
            {
                audioSource.PlayOneShot(deathClip);
            }
            // 3
            Instantiate(playerDeathPrefab, coll.contacts[0].point,
            Quaternion.identity);
            spriteRenderer.sprite = hitSprite;
            // 4
            Destroy(coll.gameObject);
        }
    }

}
