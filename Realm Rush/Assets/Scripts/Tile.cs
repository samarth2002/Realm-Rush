using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower TowerPrefab;
    [SerializeField] bool isPlaceable;
    GridManager gridManager;

    PathFinder pathFinder;

    Vector2Int coordinate = new Vector2Int();
    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    void Start()
    {
        if(gridManager!= null)
        {
            coordinate = gridManager.GetWorldCoordinatesfromPosition(transform.position);
            if(!isPlaceable)
            {
                gridManager.BlockNode(coordinate);
            }
        }
    }
    public bool IsPlaceable { get { return isPlaceable; }}
    void OnMouseDown()
    {
        if(gridManager.GetNodes(coordinate).IsWalkable && !pathFinder.WillBlockPath(coordinate))
        {
            bool isSuccessful = TowerPrefab.CreateTower(TowerPrefab , transform.position);
            if(isSuccessful)
            {
                gridManager.BlockNode(coordinate);
                pathFinder.NotifyReceivers();
            }
        }   
    }
}
