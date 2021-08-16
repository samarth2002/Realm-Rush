using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color walkableColor = Color.white;
    [SerializeField] Color blockedColor = Color.grey;
    [SerializeField] Color pathColor = Color.yellow;
    [SerializeField] Color exploredColor = new Color(1f, 0.5f ,0f);
   
    TextMeshPro label;
    Vector2Int coordinate = new Vector2Int();

    GridManager gridManager;
    
    void Awake()
    {
        label = GetComponent<TextMeshPro>();
        gridManager = FindObjectOfType<GridManager>();
        label.enabled = false;
        DisplayCoordinate();
    }
    
    void Update()
    {
        if(!Application.isPlaying)
        {
            DisplayCoordinate();
            UpdateObjectName();
            label.enabled = true;
        }
        SetLabelColor();
        ToggleCoordinates();
    }

    void ToggleCoordinates()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }
    void SetLabelColor()
    {
        if(gridManager == null){return;}
        Nodes node = gridManager.GetNodes(coordinate);
        if(node == null){return;}
        if(!node.IsWalkable)
        {
            label.color = blockedColor;
        }
        else if(node.isPath)
        {
            label.color = pathColor;
        }
        else if(node.isCovered)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = walkableColor;
        }
        
    }

    void DisplayCoordinate()
    {
        if(gridManager == null){return;}
        coordinate.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        coordinate.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);
        label.text = coordinate.x + "," + coordinate.y;
    }
    void UpdateObjectName()
    {
        transform.parent.name = coordinate.ToString();
    }
}
