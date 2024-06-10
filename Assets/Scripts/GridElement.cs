using System;
using System.Collections.Generic;
using UnityEngine;

public class GridElement
{
    #region Variables

    private int x;
    private int y;
    private Vector2 position;
    private GameObject elementObject;
    private EGridElementState state;
    private Dictionary<EGridElementNeighborSide, GridElement> neighborElements;

    #endregion Variables

    #region Properties

    public int X => x;
    public int Y => y;
    public EGridElementState State => state;
    public Vector2 Position => position;

    #endregion Properties

    #region Constructors

    public GridElement(int x, int y, IGridElementObjectProvider gridElementObjectProvider)
    {
        this.x = x;
        this.y = y;
        this.position = new Vector2(x, y);
        this.state = EGridElementState.Empty;
        this.neighborElements = new Dictionary<EGridElementNeighborSide, GridElement>();
        this.elementObject = gridElementObjectProvider.GetGridElementObject(x, y);
    }

    #endregion Constructors

    #region Methods

    public GridElement GetReferencePoint(EGridElementNeighborSide neighborElementSide)
    {
        if (neighborElements.ContainsKey(neighborElementSide))
        {
            return neighborElements[neighborElementSide];
        }

        return null;
    }

    public GridElement GetReferencePoint(EGridElementNeighborSide neighborElementSideFirst, EGridElementNeighborSide neighborElementSideSecond)
    {
        if (neighborElements.ContainsKey(neighborElementSideFirst))
        {
            return neighborElements[neighborElementSideFirst].GetReferencePoint(neighborElementSideSecond);
        }
        else if (neighborElements.ContainsKey(neighborElementSideSecond))
        {
            return neighborElements[neighborElementSideSecond].GetReferencePoint(neighborElementSideFirst);
        }

        return null;
    }

    public void AddReferencePoint(EGridElementNeighborSide neighborElementSide, GridElement element)
    {
        neighborElements.Add(neighborElementSide, element);
    }

    public void SetFull()
    {
        state = EGridElementState.Full;
    }

    public void SetEmpty()
    {
        state = EGridElementState.Empty;
    }

    #endregion Methods

    #region Override

    public static bool operator ==(GridElement elementFirst, GridElement elementSecond)
    {
        if ((object)elementFirst == null && (object)elementSecond == null)
        {
            return true;
        }

        if ((object)elementFirst == null)
        {
            return false;
        }

        if ((object)elementSecond == null)
        {
            return false;
        }

        if (elementFirst.X == elementSecond.X && elementFirst.Y == elementSecond.Y)
        {
            return true;
        }

        return false;
    }

    public static bool operator !=(GridElement elementFirst, GridElement elementSecond)
    {
        if ((object)elementFirst == null && (object)elementSecond == null)
        {
            return false;
        }

        if ((object)elementFirst == null)
        {
            return true;
        }

        if ((object)elementSecond == null)
        {
            return true;
        }

        if (elementFirst.X == elementSecond.X && elementFirst.Y == elementSecond.Y)
        {
            return false;
        }

        return true;
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }

        GridElement element = obj as GridElement;

        if ((object)element != null)
        {
            return (X == element.X && Y == element.Y);
        }
        else
        {
            return false;
        }

    }

    public bool Equals(GridElement element)
    {
        if ((object)element == null)
        {
            return false;
        }

        return (X == element.X) && (Y == element.Y);
    }

    public override int GetHashCode()
    {
        return x ^ y;
    }

    #endregion
}