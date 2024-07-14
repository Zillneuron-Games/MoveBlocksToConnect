using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameDataOld
{
    public int Id;
    public int StepsMinimumCount;
    public List<Vector2> RedTileBlocksPositions;
    public List<Vector2> BlueTileBlocksPositions;
    public List<Vector2> GreenTileBlocksPositions;
    public List<Vector2> MobileBlocksPositions;
    public List<Vector2> StaticStonesPositions;

    public GameDataOld()
    {
        Id = 0;
        StepsMinimumCount = 0;

        RedTileBlocksPositions = null;
        BlueTileBlocksPositions = null;
        GreenTileBlocksPositions = null;
        MobileBlocksPositions = null;
        StaticStonesPositions = null;
    }
}

