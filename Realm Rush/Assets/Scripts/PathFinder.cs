using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{

    [SerializeField]Vector2Int startPoint;
    public Vector2Int StartPoint {get{return startPoint;}}
    [SerializeField]Vector2Int endPoint;

    public Vector2Int EndPoint{get{return endPoint;}}

    Nodes currentSearchNode;
    Nodes startNode;
    Nodes endNode;
    
    GridManager gridManager;

    Queue<Nodes> frontier = new Queue<Nodes>();
    
    Vector2Int[] Directions = {Vector2Int.right , Vector2Int.left , Vector2Int.up , Vector2Int.down};
    
    Dictionary<Vector2Int , Nodes> grid = new Dictionary<Vector2Int, Nodes>();
    Dictionary<Vector2Int , Nodes> reached = new Dictionary<Vector2Int, Nodes>();
    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();  
        if(gridManager!= null)
        {
            grid = gridManager.Grid;
            startNode = grid[startPoint];
            endNode = grid[endPoint];
            
        }
    }
    void Start()
    {
        GetNewPath();
    }

    public List<Nodes> GetNewPath()
    {
       return GetNewPath(startPoint);
    }
    public List<Nodes> GetNewPath(Vector2Int currentCoordinates)
    {
        gridManager.ResetNodes();
        BreadthFirstSearch(currentCoordinates);
        return BuildPath();
    }
    void ExploreDirections()
    {
        List<Nodes> neighbours = new List<Nodes>();
        foreach(Vector2Int direction in Directions)
        {
            Vector2Int neighbourCoord = currentSearchNode.Coordinates + direction;
            if(grid.ContainsKey(neighbourCoord))
            {
                neighbours.Add(grid[neighbourCoord]);
            }
        }

        foreach(Nodes neighbour in neighbours)
        {
            if(!reached.ContainsKey(neighbour.Coordinates)&&neighbour.IsWalkable)
            {
                neighbour.ConnectedTo = currentSearchNode;
                reached.Add(neighbour.Coordinates,neighbour);
                frontier.Enqueue(neighbour);
            }
        }
    }

    void BreadthFirstSearch(Vector2Int coordinates)
    {
        startNode.IsWalkable = true;
        endNode.IsWalkable = true;
        frontier.Clear();
        reached.Clear();

        bool isRunning = true;

        frontier.Enqueue(grid[coordinates]);
        reached.Add(coordinates,grid[coordinates]);

        while(frontier.Count>0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isCovered = true;
            ExploreDirections();
            if(currentSearchNode.Coordinates == endPoint)
            {
                isRunning = false;
            }
        }
    }
    List<Nodes> BuildPath()
    {
        List<Nodes> path = new List<Nodes>();
        Nodes currentNode = endNode;

        path.Add(currentNode);
        currentNode.isPath = true;
        while(currentNode.ConnectedTo!=null)
        {
            currentNode = currentNode.ConnectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }
        path.Reverse();

        return path;
    }
    public bool WillBlockPath(Vector2Int coorindates)
    {
        if(grid.ContainsKey(coorindates))
        {
            bool previoustate = grid[coorindates].IsWalkable;
            grid[coorindates].IsWalkable = false;
            List<Nodes> newPath = GetNewPath();
            grid[coorindates].IsWalkable = previoustate;


            if(newPath.Count <=1)
            {
                GetNewPath();
                return true;
            }

        }
        return false;
    }
    public void NotifyReceivers()
    {
        BroadcastMessage("RecalculatePath",false,SendMessageOptions.DontRequireReceiver);
    }
}
