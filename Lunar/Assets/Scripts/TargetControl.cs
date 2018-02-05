using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TargetControl : MonoBehaviour
{

    static Camera mainCam = null;
    public bool friend = false;
    public int scoreValue;
    public bool respawn = true;
    public AudioClip sound;
    AudioSource audioSource;
    private Rigidbody2D rb2d;



    // Use this for initialization
    void Start()
    {
        // Find the camera from the object tagged as Player.
        if (!mainCam)
            mainCam = GameObject.FindWithTag("Player").GetComponent<PlayerController>().mainCam;
        audioSource = GetComponent<AudioSource>();
        Respawn();

    }

    //Update is called once per frame
    void Update()
    {
        //Respawn();
    }

    public void Respawn()
    {
        // Randomize the initial position based on the screen size above the top of the screen
        float x = Random.Range(10, Screen.width - 9);
        float y = Screen.height + Random.Range(10, 50);

        // then covert it to world coordinates and assign it to the critter.
        Vector3 pos = mainCam.ScreenToWorldPoint(new Vector3(x, y, 0f));
        pos.z = transform.position.z;
        transform.position = pos;

        Vector2 originalScale = transform.localScale;
        Vector2 randomSize = originalScale + new Vector2(Random.Range(.75f, 1.25f), Random.Range(.75f, 1.25f));

        transform.localScale = randomSize;
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector2.zero;


    }

    // collision detection function
    void OnCollisionEnter2D(Collision2D colInfo)
    {
        if (colInfo.collider.tag == "Player")
        {
            sound = Resources.Load("heli") as AudioClip;
            audioSource.PlayOneShot(sound);
            audioSource.clip = sound;
            

        }

        if (friend == true)
        {

            PlayerController.score += scoreValue;
            Debug.Log("friend " + PlayerController.score);
           

        }

        else if (friend == false)
        {
            PlayerController.score -= scoreValue;
            Debug.Log("enemy " + PlayerController.score);
            
        }

        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}