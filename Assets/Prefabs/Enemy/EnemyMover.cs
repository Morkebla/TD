using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    List<WayPoint> path = new List<WayPoint>();  // here we create a new list for our path that the enemy will be walking.
    [SerializeField] [Range(0f,5f)] float moveSpeed = 1f; // set a movespeed for the enemy between 1 and 5 so it can not go negative.
    
    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    void FindPath()
    {
        path.Clear();  

        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Path"); // here we find the waypoints that we have tagget as path.

        foreach(GameObject waypoint in waypoints)
        {
            path.Add(waypoint.GetComponent<WayPoint>()); // here we loop through all of the path tiles and add them to our enemy's movement path.
        }
    }

    void ReturnToStart()
    {
        transform.position = path[0].transform.position; // here we tell our enemy where the first tile is so it can head that way.
    }

    IEnumerator FollowPath()
    {
        foreach(WayPoint wayPoint in path)
        {
            Vector3 startPosition = transform.position; // here we set the current position in a variable.
            Vector3 endPointPosition = wayPoint.transform.position; // and the position we want to reach next into a variable.

            float travelPercent = 0; 

            transform.LookAt(endPointPosition); // and we tell our enemy to always be looking at the position it is traveling to.

            while (travelPercent < 1)
            {
                travelPercent +=Time.deltaTime * moveSpeed; // simple equation to set our enemy movement.
                transform.position = Vector3.Lerp(startPosition, endPointPosition, travelPercent); // and here we lerp the movement to make it nice and smooth.

                yield return new WaitForEndOfFrame(); // here we wait for the end of the frame before we start FollowPath Process again.
            }
               
        }

        gameObject.SetActive(false); // once we reach the end of the path we put our enemy in the ObjectPool.
    }

}