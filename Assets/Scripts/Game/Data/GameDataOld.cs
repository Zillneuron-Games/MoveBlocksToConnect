using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class GameDataOld
{
    public int ID;
    public int RoomID;
    public int PuzzleID;
    public int MinimumStepsCount;

    public string RoomName;

    public Vector2 InscriptionStonePositionRed;
    public Vector2 InscriptionStonePositionBlue;
    public Vector2 InscriptionStonePositionYellow;

    public Vector2 InscriptionStoneTargetPositionRed;
    public Vector2 InscriptionStoneTargetPositionBlue;
    public Vector2 InscriptionStoneTargetPositionYellow;

    public List<Vector2> MobilStonesPositions;
    public List<Vector2> StaticStonesPositions;

    public GameDataOld()
    {
        ID = 0;
        RoomID = 0;
        PuzzleID = 0;
        MinimumStepsCount = 0;

        RoomName = "NULL";

        InscriptionStonePositionRed = new Vector2();
        InscriptionStonePositionBlue = new Vector2();
        InscriptionStonePositionYellow = new Vector2();

        InscriptionStoneTargetPositionRed = new Vector2();
        InscriptionStoneTargetPositionBlue = new Vector2();
        InscriptionStoneTargetPositionYellow = new Vector2();

        MobilStonesPositions = null;
        StaticStonesPositions = null;
    }

    public GameDataOld(int _id, int _room_id, int _puzzle_id, int _steps_minimum, string _room_name,
                    Vector2 _inscription_point_red, Vector2 _inscription_point_blue, Vector2 _inscription_point_yellow,
                    Vector2 _target_point_red, Vector2 _target_point_blue, Vector2 _target_point_yellow,
                    List<Vector2> _mobil_stones_positions = null, List<Vector2> _static_stones_positions = null)
    {
        ID = _id;
        RoomID = _room_id;
        PuzzleID = _puzzle_id;
        MinimumStepsCount = _steps_minimum;

        RoomName = _room_name;

        InscriptionStonePositionRed = _inscription_point_red;
        InscriptionStonePositionBlue = _inscription_point_blue;
        InscriptionStonePositionYellow = _inscription_point_yellow;

        InscriptionStoneTargetPositionRed = _target_point_red;
        InscriptionStoneTargetPositionBlue = _target_point_blue;
        InscriptionStoneTargetPositionYellow = _target_point_yellow;

        MobilStonesPositions = _mobil_stones_positions;
        StaticStonesPositions = _static_stones_positions;
    }

    public bool IsDoubleGame
    {
        get { return (InscriptionStonePositionYellow == InscriptionStoneTargetPositionYellow); }
    }
}

