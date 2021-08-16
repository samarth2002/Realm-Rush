using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Nodes 
{
   public Vector2Int Coordinates;
   public bool IsWalkable;
   public bool isCovered;
   public bool isPath;
   public Nodes ConnectedTo;

   public Nodes(Vector2Int Coordinates , bool IsWalkable) 
   {
        this.Coordinates = Coordinates;
        this.IsWalkable = IsWalkable;
   }
}
