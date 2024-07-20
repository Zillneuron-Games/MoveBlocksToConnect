using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameDataOld
{
    public int Id;
    public string Level;
    public string Name;
    public bool Special;
    public List<Vector2> RedTileBlocksPositions;
    public List<Vector2> GreenTileBlocksPositions;
    public List<Vector2> BlueTileBlocksPositions;
    public List<Vector2> MobileBlocksPositions;
    public List<Vector2> StaticStonesPositions;
    public EShapeColor[][] BonusShape;

    public GameDataOld()
    {
        this.Id = 0;
        this.Level = null;
        this.Name = null;
        this.Special = false;
        this.RedTileBlocksPositions = new List<Vector2>();
        this.BlueTileBlocksPositions = new List<Vector2>();
        this.GreenTileBlocksPositions = new List<Vector2>();
        this.MobileBlocksPositions = new List<Vector2>();
        this.StaticStonesPositions = new List<Vector2>();
        this.BonusShape = null;
    }
}

