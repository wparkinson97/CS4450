using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{

    // Use this for initialization
    [HideInInspector]
    public GameObject[] waypoints;
    private int currentWaypoint = 0;
    private float lastWaypointSwitchTime;
    public float speed = 1.0f;

    void Start()
    {
        lastWaypointSwitchTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // From the waypoints array, you retrieve the start and end position for the current path segment. 
        Vector3 startPosition = waypoints[currentWaypoint].transform.position;
        Vector3 endPosition = waypoints[currentWaypoint + 1].transform.position;
        /* Calculate the time needed for the whole distance with the formula time = distance / speed, 
         then determine the current time on the path. Using Vector2.Lerp, you interpolate the current 
         position of the enemy between the segment's start and end positions. */
        float pathLength = Vector3.Distance(startPosition, endPosition);
        float totalTimeForPath = pathLength / speed;
        float currentTimeOnPath = Time.time - lastWaypointSwitchTime;
        gameObject.transform.position = Vector2.Lerp(startPosition, endPosition, currentTimeOnPath / totalTimeForPath);
        // Check whether the enemy has reached the endPosition. If yes, handle these two possible scenarios: 
        if (gameObject.transform.position.Equals(endPosition))
        {
            if (currentWaypoint < waypoints.Length - 2)
            {
                /* The enemy is not yet at the last waypoint, so increase currentWaypoint and update 
                 lastWaypointSwitchTime. Later, you'll add code to rotate the enemy so it points in the 
                 direction it's moving, too.*/
                currentWaypoint++;
                lastWaypointSwitchTime = Time.time;
                
                //TODO:  Rotate into move direction                
            }
            else
            {
                /* The enemy reached the last waypoint, so this destroys it and triggers a sound effect. 
                 Later you'll add code to decrease the player's health, too. */
                Destroy(gameObject);

                AudioSource audioSource = gameObject.GetComponent<AudioSource>();
                AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
                // TODO: deduct health
            }
        }

    }
}
