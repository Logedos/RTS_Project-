using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridData
{
    private Dictionary<Vector3Int, PlacementData> placedObjects = new Dictionary<Vector3Int, PlacementData>();
    
    public void AddOblectAt(Vector3Int gridPosition, Vector2Int objectSize, int ID, int placedObjectIndex)
    {
        List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize);
        PlacementData data = new PlacementData(positionToOccupy, ID, placedObjectIndex);
        foreach (var position in positionToOccupy)
        {
            if (placedObjects.ContainsKey(position))
                throw new Exception("Dictionary already contains this cell position" + position);
           
            placedObjects[position] = data;
        }
    }

    private List<Vector3Int> CalculatePositions(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> returnVal = new List<Vector3Int>();
        for (int x = 0; x < objectSize.x; x++)
        {
            for (int y = 0; y < objectSize.y; y++)
            {
                returnVal.Add(gridPosition + new Vector3Int(x,0,y));
            }
        }

        return returnVal;
    }
    public bool CanPlaceObjectAt(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize);
        foreach (var position in positionToOccupy)
        {
            if (placedObjects.ContainsKey(position))
            {
                return false;
            }
            
        }
        return true;
    }
}

public class PlacementData
{
    public PlacementData(List<Vector3Int> occupiedPositions, int id, int placedObjectIndex)
    {
        this.occupiedPositions = occupiedPositions;
        ID = id;
        PlacedObjectIndex = placedObjectIndex;
    }
    public List<Vector3Int> occupiedPositions;
    public int ID { get; private set; }
    public int PlacedObjectIndex { get; private set; }
}
