using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterData : MonoBehaviour {


[System.Serializable]
public class MonsterLevel
    {
        public int cost;
        public GameObject visulization;
        public GameObject bullet;
        public float fireRate;
    }

    public List<MonsterLevel> levels;
    private MonsterLevel currentLevel;


    /*With a property defined, you can call just like any other variable: either as CurrentLevel
 (from inside the class) or as monster.CurrentLevel (from outside it). You can define custom
 behavior in a property's getter or setter method, and by supplying only a getter, a setter or
both, you can control whether a property is read-only, write-only or read/write.*/
    public MonsterLevel CurrentLevel
    {
        //In the getter, you return the value of currentLevel.
        get
        {
            return currentLevel;
        }
        /*In the setter, you assign the new value to currentLevel. Next you get the index of the
       current level. Finally you iterate over all the levels and set the visualization to active or inactive,
       depending on the currentLevelIndex. This is great because it means that whenever someone sets
       currentLevel, the sprite updates automatically. */
        set
        {
            currentLevel = value;
            int currentLevelIndex = levels.IndexOf(currentLevel);
            GameObject levelVisualization = levels[currentLevelIndex].visulization;
            for (int i = 0; i < levels.Count; i++)
            {
                if (levelVisualization != null)
                {
                    if (i == currentLevelIndex)
                    {
                        levels[i].visulization.SetActive(true);
                    }
                    else
                    {
                        levels[i].visulization.SetActive(false);
                    }
                }
            }
        }
    }

    void OnEnable()
    {
        CurrentLevel = levels[0];
    }

    public MonsterLevel GetNextLevel()
    {
        int currentLevelIndex = levels.IndexOf(currentLevel);
        int maxLevelIndex = levels.Count - 1;
        if (currentLevelIndex < maxLevelIndex)
        {
            return levels[currentLevelIndex + 1];
        }
        else
        {
            return null;
        }
    }

    public void IncreaseLevel()
    {
        int currentLevelIndex = levels.IndexOf(currentLevel);
        if (currentLevelIndex < levels.Count - 1)
        {
            CurrentLevel = levels[currentLevelIndex + 1];
        }
    }
}
