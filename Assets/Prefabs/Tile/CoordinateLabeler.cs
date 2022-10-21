using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white; // color of buildable tiles in the game
    [SerializeField] Color blockedColor = Color.gray;  // color of non buildable tiles in the game.

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    WayPoint waypoint;

    private void Awake()
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = false; // on awake we turn off our label that holds the text of our TextMesh.
        waypoint = GetComponentInParent<WayPoint>();
        DisplayCoordinates();
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            UpdateObjectName();
            DisplayCoordinates();
        }
        ToggleLabels();
        ColorCoordinates();
    }

    void ToggleLabels()
    {
        if (Input.GetKey(KeyCode.C))
        {
            label.enabled = !label.IsActive(); // this is a toggle for our label so we can turn on and off our coordinate system
        }
    }

    void ColorCoordinates()
    {
        if (waypoint.Isplaceable == true)
        {
            label.color = defaultColor;
        } 
        else
        {
            label.color = blockedColor;
        }
    }

    void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x); // here we turn our coordinates to Integer from a float and divide it by the SNap settings we set in the Editor.
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);
        label.text = coordinates.x + "," + coordinates.y; // and here we display the coordinates we have set onto the textmeshpro.
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString(); // here we se the name as the coordinate it represents.
    }
}
