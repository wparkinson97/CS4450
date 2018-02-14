using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceMonster : MonoBehaviour {

    // Use this for initialization

    public GameObject monsterPrefab;
    private GameObject monster;
    private GameManagerBehavior gameManager;

    private bool CanPlaceMonster()
    {
        //return monster == null;
        int cost = monsterPrefab.GetComponent<MonsterData>().levels[0].cost;
        return monster == null && gameManager.Gold >= cost;
    }

    //Unity automatically calls OnMouseUp when a player taps a GameObject’s physics collider.
    void OnMouseUp()
    {
        //When called, this method places a new monster if CanPlaceMonster() returns true.
        if (CanPlaceMonster())
        {
            /*You create the monster with Instantiate, a method that creates an instance of a
            given prefab with the specified position and rotation. In this case, you copy
            monsterPrefab, give it the current GameObject’s position and no rotation, cast the
            result to a GameObject and store it in monster.*/
            monster = (GameObject) Instantiate(monsterPrefab, transform.position, Quaternion.identity);
            //Finally, you call PlayOneShot to play the sound effect attached to the object’s AudioSource component
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);
            gameManager.Gold -= monster.GetComponent<MonsterData>().CurrentLevel.cost;
        }
        else if (CanUpgradeMonster())
        {
            monster.GetComponent<MonsterData>().IncreaseLevel();
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);
            gameManager.Gold -= monster.GetComponent<MonsterData>().CurrentLevel.cost;
        }
    }

    private bool CanUpgradeMonster()
    {
        if (monster != null)
        {
            MonsterData monsterData = monster.GetComponent<MonsterData>();
            MonsterData.MonsterLevel nextLevel = monsterData.GetNextLevel();
            if (nextLevel != null)
            {
                //return true;
                
                return gameManager.Gold >= nextLevel.cost;
            }
        }
        return false;
    }

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }

}
