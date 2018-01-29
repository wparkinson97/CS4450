using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameManager : MonoBehaviour {

    public static GameObject[] tileArray = new GameObject[8];

    private bool gameStarted = false;

    private int countOpen = 0;
    private int sum = 0;

    private int[] chosenTile = new int[] { -1, -1, -1 };

    private int timeLeftToClose = 100;
    //public static GameObject[] tileArray = new GameObject[8];

    // Use this for initialization
    void Start () {
        ClickTile.frameRef = gameObject;

    }

    // Update is called once per frame
    void Update () {
        if (!gameStarted)
        {
            MakeSolution();
            gameStarted = true;
        }
        else if(countOpen == 3){
            Debug.Log("three tiles opened");
            timeLeftToClose--;
            if(timeLeftToClose < 0)
            {
                CloseOpenTiles();
                chosenTile[0] = -1;
            }
        }
	}
    void MakeSolution()
    {

        int tile1 = Random.Range(0,8);
        int tile2 = Random.Range(0, 8);
        int tile3 = Random.Range(0, 8);

        while(tile2 == tile1)
        {
            tile2 = Random.Range(0, 8);
        }
        while(tile3 == tile2 || tile3 == tile1)
        {
            tile3 = Random.Range(0, 8);
        }


        int num1 = tileArray[tile1].GetComponent<ClickTile>().myNumber;

        int num2 = tileArray[tile2].GetComponent<ClickTile>().myNumber;

        // Make sure we don't end up with a number less than -9

        if (num1 + num2 > 9)
        {

            num1 -= num1 + num2 - 9;

            tileArray[tile1].GetComponent<ClickTile>().myNumber = num1;

        }

        // Make sure we don't end up with a number larger than 9

        if (num1 + num2 < -9)
        {

            num1 -= num1 + num2 + 9;

            tileArray[tile1].GetComponent<ClickTile>().myNumber = num1;

        }

        tileArray[tile3].GetComponent<ClickTile>().myNumber = -(num1 + num2);

    }
    public void PlayTile(int id, int number)
    {
        if(countOpen >= 3)
        {
            FrameManager.tileArray[id].GetComponent<ClickTile>().CloseTile();
            return;
        }
        chosenTile[countOpen] = id;
        countOpen++;
        sum += number;
        timeLeftToClose = 100;

        if(countOpen == 3)
        {
            if (sum == 0)
                GameWon();
        }

        Debug.Log("Clicked tile: " + id + " with number: " + number);

    }
    void CloseOpenTiles()
    {
        foreach(var tile in tileArray)
            tile.GetComponent<ClickTile>().CloseTile();
        sum = 0;
        countOpen = 0;


    }
    void GameWon()
    {
        foreach(var tile in tileArray)
        {
            tile.SetActive(false);
        }
        GameObject.FindGameObjectWithTag("Finish").SetActive(true);
        GameObject.Find("redframe").SetActive(false);
    }

}
