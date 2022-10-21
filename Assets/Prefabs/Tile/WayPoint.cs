using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] GameObject towerPrefab;

    [SerializeField] bool isPlaceable;
    public bool Isplaceable{get{ return isPlaceable; } } 
    
    private void OnMouseDown()
    {
        if (isPlaceable)
        {
            Instantiate (towerPrefab,transform.position,Quaternion.identity); // if the Waypoint /(slash)/ tile is placeable we create a tower at its position.
            isPlaceable = false; // makes the position unplaceable.
        }
    }
}
