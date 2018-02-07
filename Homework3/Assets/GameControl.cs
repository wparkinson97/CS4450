using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {


    [SerializeField]
    public List<GameObject> friends;

    private float waitTime;
    private float time;
    private int indexOn = 0;

    private static int score = 0;

    public static Text scoreText;

    public Button startButton;


    public bool isStarting = false;
    public static bool isStarted = false;

    public static int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            scoreText.text = "" + score;
        }
    }

	// Use this for initialization
	void Start () {
        startButton.onClick.AddListener(StartGame);
	}
    void StartGame()
    {
        isStarting = true;
        //startButton.transform.Translate(100f, 100f);
        startButton.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (isStarting)
        {
            waitTime = Time.deltaTime;
            isStarted = true;
            isStarting = false;
        }
        if (isStarted)
        {
            time += Time.deltaTime;
		    if(time >= waitTime && indexOn < 5)
            {
                float xRand = Random.Range(-6, 6);
                var coin = Instantiate(friends[indexOn], new Vector3(xRand,6.0f, 0.0f), Quaternion.identity);
                indexOn++;
                waitTime = time + 1f;
            }
        }

    }
}
