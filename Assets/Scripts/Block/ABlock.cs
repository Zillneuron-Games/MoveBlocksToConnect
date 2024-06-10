using System;
using UnityEngine;

public abstract class ABlock
{
    #region Variables

    protected int id;
    protected bool isMovable;
    protected bool isInTransit;
    protected GameObject blockObject;
    protected GridElement gridElement;

    #endregion Variables

    #region Properties

    public int Id
    {
        get { return id; }
    }

    public bool IsInTransit
    {
        get { return isInTransit; }
    }

    public GridElement CurrentElement
    {
        get { return gridElement; }
    }

    public Vector3 BlockPosition
    {
        get { return blockObject.transform.position; }
        set { blockObject.transform.position = value; }
    }

    #endregion Properties

    public ABlock(int id, GameObject blockObject, GridElement gridElement)
    {
        this.id = id;
        this.blockObject = blockObject;
        this.gridElement = gridElement;
        this.isInTransit = false;
    }


    #region Methods

    public abstract void ChangePoint(GridElement newElement);

    public abstract void TransitTransform();

    public abstract void FinalTransform();

    #endregion Methods
}