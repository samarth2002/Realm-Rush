using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;

    [SerializeField] int unityGridSize=10;
    Dictionary<Vector2Int , Nodes> grid = new Dictionary<Vector2Int, Nodes>();
    public Dictionary<Vector2Int , Nodes> Grid { get{return grid;}}
    public int UnityGridSize {get {return unityGridSize; }}
    void Awake()
    {
       CreateGrid();
    }

    public Nodes GetNodes(Vector2Int coordinates)
    {
        if(grid.ContainsKey(coordinates))
        {
            return grid[coordinates];
        }
        return null;
    }
    void CreateGrid()
    {
        for(int x = 0 ; x < gridSize.x ; x++)
        {
            for(int y = 0 ; y < gridSize.y ; y++)
            {
                Vector2Int coordinates = new Vector2Int(x,y);
                grid.Add(coordinates , new Nodes(coordinates , true));
            }
        }
    }

    public void ResetNodes()
    {
        foreach(KeyValuePair<Vector2Int,Nodes> entry in grid)
        {
            entry.Value.ConnectedTo = null;
            entry.Value.isCovered = false;
            entry.Value.isPath = false;
        }
    }
    
    public Vector2Int GetWorldCoordinatesfromPosition(Vector3 position)
    {
        Vector2Int coordinate = new Vector2Int();
        coordinate.x = Mathf.RoundToInt(position.x / unityGridSize);
        coordinate.y = Mathf.RoundToInt(position.z / unityGridSize);
        return coordinate;
    }

    public Vector3 GetWorldPositionfromCoordinates(Vector2Int coordinate)
    {
        Vector3 position = new Vector3();
        position.x = coordinate.x*unityGridSize;
        position.z = coordinate.y*unityGridSize;

        return position;    }
    public void BlockNode(Vector2Int coordinates)
    {
        if(grid.ContainsKey(coordinates))
        {
            grid[coordinates].IsWalkable = false;
        }
    }
}
