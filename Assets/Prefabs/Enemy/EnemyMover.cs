using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Enemy))]

public class EnemyMover : MonoBehaviour
{
    [SerializeField][Range(0f, 5f)] float moveSpeed = 1f; // set a movespeed for the enemy between 1 and 5 so it can not go negative.
    List<Node> path = new List<Node>();
    Enemy enemy;
    GridManager gridManager;
    PathFinder PathFinder;


    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
    }

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        PathFinder = FindObjectOfType<PathFinder>();
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();

        if (resetPath)
        {
            coordinates = PathFinder.StartCoordinates;
        } else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines();
        path.Clear();
        path = PathFinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }

    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(PathFinder.StartCoordinates); // here we tell our enemy where the first tile is so it can head that way.
    }

    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false); // once we reach the end of the path we put our enemy in the ObjectPool.
    }
    IEnumerator FollowPath()
    {
        for (int i = 1; i < path.Count; i++)
        {
            Vector3 startPosition = transform.position; // here we set the current position in a variable.
            Vector3 endPointPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates); // and the position we want to reach next into a variable.

            float travelPercent = 0;

            transform.LookAt(endPointPosition); // and we tell our enemy to always be looking at the position it is traveling to.

            while (travelPercent < 1)
            {
                travelPercent += Time.deltaTime * moveSpeed; // simple equation to set our enemy movement.
                transform.position = Vector3.Lerp(startPosition, endPointPosition, travelPercent); // and here we lerp the movement to make it nice and smooth.

                yield return new WaitForEndOfFrame(); // here we wait for the end of the frame before we start FollowPath Process again.
            }

        }

        FinishPath();
    }

}
