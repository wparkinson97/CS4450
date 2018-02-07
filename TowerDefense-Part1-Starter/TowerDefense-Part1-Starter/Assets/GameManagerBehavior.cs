using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerBehavior : MonoBehaviour {

    private int gold;
    public Text goldLabel;

    private int Gold
    {
        get
        {
            return this.gold;
        }
        set
        {
            this.gold = value;
            goldLabel.GetComponent<Text>().text = "Gold: " + gold;
        }
    }
    private void Start()
    {
        Gold = 1000;
    }
}
