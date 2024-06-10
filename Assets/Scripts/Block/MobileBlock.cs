using System;
using UnityEngine;

public class MobileBlock : ABlock
{
    public MobileBlock(int id, GameObject blockObject, GridElement gridElement) : base(id, blockObject, gridElement)
    {
        isMovable = true;
        gridElement.SetFull();
    }

    public override void ChangePoint(GridElement newElement)
    {
        gridElement = newElement;
    }

    public override void TransitTransform()
    {
        isInTransit = true;
    }

    public override void FinalTransform()
    {
        isInTransit = false;
    }
}