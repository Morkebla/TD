using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color defaultColor = Color.white; // color of buildable tiles in the game
    [SerializeField] Color blockedColor = Color.gray;  // color of non buildable tiles in the game.
    [SerializeField] Color pathColor = new Color(1f, 0.5f, 0f);

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        label.enabled = false; // on awake we turn off our label that holds the text of our TextMesh.
        DisplayCoordinates();
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            UpdateObjectName();
            DisplayCoordinates();
            label.enabled = false;
        }
        ToggleLabels();
        SetLabelColor();
    }

    void ToggleLabels()
    {
        if (Input.GetKey(KeyCode.C))
        {
            label.enabled = !label.IsActive(); // this is a toggle for our label so we can turn on and off our coordinate system
        }
    }

    void SetLabelColor()
    {
        if(gridManager == null) { return; }

        Node node = gridManager.GetNode(coordinates);

        if(node == null) { return; }

        if (!node.isWalkable)
        {
            label.color = blockedColor;
        }

        else if (!node.isPath)
        {
            label.color = pathColor;
        }

        else if (node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultColor;
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
