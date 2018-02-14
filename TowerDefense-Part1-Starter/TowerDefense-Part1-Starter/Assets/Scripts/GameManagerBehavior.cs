using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameManagerBehavior : MonoBehaviour {

    private int gold;
    public Text goldLabel;
    public Text waveLabel;
    public GameObject[] nextWaveLabels;

    public bool gameOver = false;

    private int wave;
    public int Wave
    {
        get
        {
            return wave;
        }
        set
        {
            wave = value;
            if (!gameOver)
            {
                for (int i = 0; i < nextWaveLabels.Length; i++)
                {
                    nextWaveLabels[i].GetComponent<Animator>().SetTrigger("nextWave");
                }
            }
            waveLabel.text = "WAVE: " + (wave + 1);
        }
    }


    public int Gold
    {
        get
        {
            return gold;
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
