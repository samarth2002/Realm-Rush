using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    
    [SerializeField] [Range(0f , 5f)] float speed = 1f;
    List<Nodes> path = new List<Nodes>();

    Enemy enemy;
    GridManager gridManager;
    PathFinder pathFinder;

   
    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true); 
    }
     void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();
    
        if(resetPath)
        {
            coordinates = pathFinder.StartPoint;
        }
        else
        {
            coordinates = gridManager.GetWorldCoordinatesfromPosition(transform.position);
        }
        StopAllCoroutines();
        path.Clear();
        path  = pathFinder.GetNewPath(coordinates);
        StartCoroutine(EnemyPath());
         
    }
    void ReturnToStart()
    {
        transform.position = gridManager.GetWorldPositionfromCoordinates(pathFinder.StartPoint);
    }
    
    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }
    IEnumerator EnemyPath()
    {
        for(int i = 1 ; i < path.Count ; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetWorldPositionfromCoordinates(path[i].Coordinates);
            float travelPercent = 0f;
            transform.LookAt(endPosition);
            while(travelPercent < 1f)
            {
                travelPercent += Time.deltaTime*speed;
                transform.position = Vector3.Lerp(startPosition , endPosition , travelPercent);
                yield return new WaitForEndOfFrame();
            }
            
        }
        FinishPath();
    }
}
