
using System;
using UnityEngine;

public class StaticBlock : ABlock
{
    public StaticBlock(int id, GameObject blockObject, GridElement gridElement) : base(id, blockObject, gridElement)
    {
        this.isMovable = false;
        gridElement.SetInaccessible();
    }

    public override void ChangePoint(GridElement newElement)
    {

    }

    public override void TransitTransform()
    {

    }

    public override void FinalTransform()
    {

    }
}