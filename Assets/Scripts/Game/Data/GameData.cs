using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public int Id;
    public int StepsMinimumCount;
    public List<Vector2> RedTileBlocksPositions;
    public List<Vector2> BlueTileBlocksPositions;
    public List<Vector2> GreenTileBlocksPositions;
    public List<Vector2> MobileBlocksPositions;
    public List<Vector2> StaticBlocksPositions;

    public GameData()
    {
        Id = 0;
        StepsMinimumCount = 0;

        RedTileBlocksPositions = null;
        BlueTileBlocksPositions = null;
        GreenTileBlocksPositions = null;
        MobileBlocksPositions = null;
        StaticBlocksPositions = null;
    }

    public GameData(int id, int stepsMinimum, List<Vector2> redTileBlocksPositions, List<Vector2> blueTileBlocksPositions = null, List<Vector2> greenTileBlocksPositions = null, List<Vector2> mobileBlocksPositions = null, List<Vector2> staticBlocksositions = null)
    {
        Id = id;
        StepsMinimumCount = stepsMinimum;

        RedTileBlocksPositions = redTileBlocksPositions;
        BlueTileBlocksPositions = blueTileBlocksPositions;
        GreenTileBlocksPositions = greenTileBlocksPositions;
        MobileBlocksPositions = mobileBlocksPositions;
        StaticBlocksPositions = staticBlocksositions;
    }

    public bool IsSingleGame
    {
        get 
        {
            return (BlueTileBlocksPositions.Count > 0 && GreenTileBlocksPositions.Count == 0 && RedTileBlocksPositions.Count == 0)
                || (BlueTileBlocksPositions.Count == 0 && GreenTileBlocksPositions.Count > 0 && RedTileBlocksPositions.Count == 0)
                || (BlueTileBlocksPositions.Count == 0 && GreenTileBlocksPositions.Count == 0 && RedTileBlocksPositions.Count > 0);
        }
    }

    public bool IsDoubleGame
    {
        get 
        {
            return (BlueTileBlocksPositions.Count > 0 && GreenTileBlocksPositions.Count > 0 && RedTileBlocksPositions.Count == 0)
              || (BlueTileBlocksPositions.Count > 0 && GreenTileBlocksPositions.Count == 0 && RedTileBlocksPositions.Count > 0)
              || (BlueTileBlocksPositions.Count == 0 && GreenTileBlocksPositions.Count > 0 && RedTileBlocksPositions.Count > 0);
        }
    }
}

