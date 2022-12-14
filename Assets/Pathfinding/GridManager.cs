using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Tooltip("UnityWorldGrid should match unity editor snap settings.")]
    [SerializeField] int _unityWorldGrid = 10;
    [SerializeField] Vector2Int gridSize;
    public int unityWorldGrid { get { return _unityWorldGrid; } }

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }

    private void Awake()
    {
        CreateGrid();
    }

    public Node GetNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            return grid[coordinates];
        }

        return null;
    }
    
    public void BlockNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            grid[coordinates].isWalkable = false;
        }
    }

    public void ResetNodes()
    {
        foreach(KeyValuePair<Vector2Int, Node> Entry in grid)
        {
            Entry.Value.connectedTo = null;
            Entry.Value.isExplored = false;
            Entry.Value.isPath = false;
        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x / unityWorldGrid);
        coordinates.y = Mathf.RoundToInt(position.z / unityWorldGrid);

        return coordinates;
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 position = new Vector3();
        position.x = coordinates.x * unityWorldGrid;
        position.z = coordinates.y * unityWorldGrid;

        return position;
    }

    void CreateGrid()
    {
        for (int x = 0; x < gridSize.x; x++)  // if our grid is 5 by 5 x here will go 1 x
        {
            for (int y = 0; y < gridSize.y; y++) // and our grid here will go all the way up y before moving back to x.
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                grid.Add(coordinates, new Node(coordinates, true));
            }
        }
    }
}