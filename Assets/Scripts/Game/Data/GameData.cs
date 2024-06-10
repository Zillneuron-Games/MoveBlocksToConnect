using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public int Id;
    public int MinimumStepsCount;

    public Vector2 InscriptionBlockPositionRed;
    public Vector2 InscriptionBlockPositionBlue;
    public Vector2 InscriptionBlockPositionYellow;

    public Vector2 InscriptionBlockTargetPositionRed;
    public Vector2 InscriptionBlockTargetPositionBlue;
    public Vector2 InscriptionBlockTargetPositionYellow;

    public List<Vector2> MobileBlocksPositions;
    public List<Vector2> StaticBlocksPositions;

    public GameData()
    {
        Id = 0;
        MinimumStepsCount = 0;

        InscriptionBlockPositionRed = new Vector2();
        InscriptionBlockPositionBlue = new Vector2();
        InscriptionBlockPositionYellow = new Vector2();

        InscriptionBlockTargetPositionRed = new Vector2();
        InscriptionBlockTargetPositionBlue = new Vector2();
        InscriptionBlockTargetPositionYellow = new Vector2();

        MobileBlocksPositions = null;
        StaticBlocksPositions = null;
    }

    public GameData(int id, int stepsMinimum,
                    Vector2 inscriptionBlockRed, Vector2 inscriptionBlockBlue, Vector2 inscriptionBlockYellow,
                    Vector2 targetBlockRed, Vector2 targetBlockBlue, Vector2 targetBlockYellow,
                    List<Vector2> mobileBlocksPositions = null, List<Vector2> staticBlocksositions = null)
    {
        Id = id;
        MinimumStepsCount = stepsMinimum;

        InscriptionBlockPositionRed = inscriptionBlockRed;
        InscriptionBlockPositionBlue = inscriptionBlockBlue;
        InscriptionBlockPositionYellow = inscriptionBlockYellow;

        InscriptionBlockTargetPositionRed = targetBlockRed;
        InscriptionBlockTargetPositionBlue = targetBlockBlue;
        InscriptionBlockTargetPositionYellow = targetBlockYellow;

        MobileBlocksPositions = mobileBlocksPositions;
        StaticBlocksPositions = staticBlocksositions;
    }

    public bool IsDoubleGame
    {
        get { return (InscriptionBlockPositionYellow == InscriptionBlockTargetPositionYellow); }
    }
}

