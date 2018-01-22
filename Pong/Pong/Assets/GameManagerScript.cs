using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    public static int PlayerScore1 = 0;
    public static int PlayerScore2 = 0;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void Score(string WallId)
    {
        if(WallId == "RightWall")
        {
            PlayerScore1++;
        }
        if(WallId == "LeftWall")
        {
            PlayerScore2++;
        }
        
    }
}
