using System;
using System.Collections.Generic;

public class GameBoardGrid
{
    #region Variables

    private int gridLength;
    private int gridHight;
    private IGridElementObjectProvider gridElementObjectProvider;
    private GridElement rootElement;

    #endregion Variables

    #region Properties

    public GridElement this[int i, int j]
    {
        get
        {
            if (i >= 0 && i < gridLength && j >= 0 && j < gridHight)
            {
                return ForEachPoint(i, j, GetPointByIndex);
            }
            else
            {
                return null;
            }
        }
    }

    #endregion Properties

    #region Constructors

    public GameBoardGrid(int gridLength, int gridHight, IGridElementObjectProvider gridElementObjectProvider)
    {
        this.gridLength = gridLength;
        this.gridHight = gridHight;
        this.gridElementObjectProvider = gridElementObjectProvider;
        this.rootElement = new GridElement(0, 0, gridElementObjectProvider);

        List<GridElement> frontElements = new List<GridElement>();

        frontElements.Add(rootElement);

        CreateReferencePoints(frontElements);
    }

    #endregion Constructors

    #region Methods

    private void CreateReferencePoints(List<GridElement> frontElements)
    {
        List<GridElement> currentElements = frontElements;

        while (currentElements.Count > 0)
        {
            List<GridElement> nextElement = new List<GridElement>();

            foreach (GridElement rootElement in currentElements)
            {
                #region Bottom, Left, Top, Right

                if (rootElement.GetReferencePoint(EGridElementNeighborSide.Bottom) == null)
                {
                    if (rootElement.Y - 1 >= 0)
                    {
                        GridElement temp_point = new GridElement(rootElement.X, rootElement.Y - 1, gridElementObjectProvider);

                        rootElement.AddReferencePoint(EGridElementNeighborSide.Bottom, temp_point);
                        rootElement.GetReferencePoint(EGridElementNeighborSide.Bottom).AddReferencePoint(EGridElementNeighborSide.Top, rootElement);

                        nextElement.Add(temp_point);
                    }
                }

                if (rootElement.GetReferencePoint(EGridElementNeighborSide.Top) == null)
                {
                    if (rootElement.Y + 1 < gridHight)
                    {

                        GridElement temp_point = new GridElement(rootElement.X, rootElement.Y + 1, gridElementObjectProvider);

                        rootElement.AddReferencePoint(EGridElementNeighborSide.Top, temp_point);
                        rootElement.GetReferencePoint(EGridElementNeighborSide.Top).AddReferencePoint(EGridElementNeighborSide.Bottom, rootElement);

                        nextElement.Add(temp_point);
                    }
                }

                if (rootElement.GetReferencePoint(EGridElementNeighborSide.Left) == null)
                {
                    if (rootElement.X - 1 >= 0)
                    {
                        GridElement temp_point = new GridElement(rootElement.X - 1, rootElement.Y, gridElementObjectProvider);

                        rootElement.AddReferencePoint(EGridElementNeighborSide.Left, temp_point);
                        rootElement.GetReferencePoint(EGridElementNeighborSide.Left).AddReferencePoint(EGridElementNeighborSide.Right, rootElement);

                        nextElement.Add(temp_point);
                    }
                }

                if (rootElement.GetReferencePoint(EGridElementNeighborSide.Right) == null)
                {
                    if (rootElement.X + 1 < gridLength)
                    {
                        GridElement temp_point = new GridElement(rootElement.X + 1, rootElement.Y, gridElementObjectProvider);

                        rootElement.AddReferencePoint(EGridElementNeighborSide.Right, temp_point);
                        rootElement.GetReferencePoint(EGridElementNeighborSide.Right).AddReferencePoint(EGridElementNeighborSide.Left, rootElement);

                        nextElement.Add(temp_point);
                    }
                }

                #endregion

                #region Bottom -> <- Left

                if (rootElement.GetReferencePoint(EGridElementNeighborSide.Bottom) != null && rootElement.GetReferencePoint(EGridElementNeighborSide.Left) != null)
                {
                    if (rootElement.GetReferencePoint(EGridElementNeighborSide.Bottom).GetReferencePoint(EGridElementNeighborSide.Left) == null && rootElement.GetReferencePoint(EGridElementNeighborSide.Left).GetReferencePoint(EGridElementNeighborSide.Bottom) == null)
                    {
                        GridElement tempElement = new GridElement(rootElement.X - 1, rootElement.Y - 1, gridElementObjectProvider);

                        rootElement.GetReferencePoint(EGridElementNeighborSide.Bottom).AddReferencePoint(EGridElementNeighborSide.Left, tempElement);
                        rootElement.GetReferencePoint(EGridElementNeighborSide.Left).AddReferencePoint(EGridElementNeighborSide.Bottom, tempElement);

                        tempElement.AddReferencePoint(EGridElementNeighborSide.Right, rootElement.GetReferencePoint(EGridElementNeighborSide.Bottom));
                        tempElement.AddReferencePoint(EGridElementNeighborSide.Top, rootElement.GetReferencePoint(EGridElementNeighborSide.Left));

                        nextElement.Add(tempElement);
                    }
                }

                #endregion

                #region Left -> <- Top

                if (rootElement.GetReferencePoint(EGridElementNeighborSide.Left) != null && rootElement.GetReferencePoint(EGridElementNeighborSide.Top) != null)
                {
                    if (rootElement.GetReferencePoint(EGridElementNeighborSide.Left).GetReferencePoint(EGridElementNeighborSide.Top) == null && rootElement.GetReferencePoint(EGridElementNeighborSide.Top).GetReferencePoint(EGridElementNeighborSide.Left) == null)
                    {
                        GridElement tempElement = new GridElement(rootElement.X - 1, rootElement.Y + 1, gridElementObjectProvider);

                        rootElement.GetReferencePoint(EGridElementNeighborSide.Left).AddReferencePoint(EGridElementNeighborSide.Top, tempElement);
                        rootElement.GetReferencePoint(EGridElementNeighborSide.Top).AddReferencePoint(EGridElementNeighborSide.Left, tempElement);

                        tempElement.AddReferencePoint(EGridElementNeighborSide.Bottom, rootElement.GetReferencePoint(EGridElementNeighborSide.Left));
                        tempElement.AddReferencePoint(EGridElementNeighborSide.Right, rootElement.GetReferencePoint(EGridElementNeighborSide.Top));

                        nextElement.Add(tempElement);
                    }
                }


                #endregion

                #region Top -> <- Right


                if (rootElement.GetReferencePoint(EGridElementNeighborSide.Top) != null && rootElement.GetReferencePoint(EGridElementNeighborSide.Right) != null)
                {
                    if (rootElement.GetReferencePoint(EGridElementNeighborSide.Top).GetReferencePoint(EGridElementNeighborSide.Right) == null && rootElement.GetReferencePoint(EGridElementNeighborSide.Right).GetReferencePoint(EGridElementNeighborSide.Top) == null)
                    {
                        GridElement tempElement = new GridElement(rootElement.X + 1, rootElement.Y + 1, gridElementObjectProvider);

                        rootElement.GetReferencePoint(EGridElementNeighborSide.Top).AddReferencePoint(EGridElementNeighborSide.Right, tempElement);
                        rootElement.GetReferencePoint(EGridElementNeighborSide.Right).AddReferencePoint(EGridElementNeighborSide.Top, tempElement);

                        tempElement.AddReferencePoint(EGridElementNeighborSide.Left, rootElement.GetReferencePoint(EGridElementNeighborSide.Top));
                        tempElement.AddReferencePoint(EGridElementNeighborSide.Bottom, rootElement.GetReferencePoint(EGridElementNeighborSide.Right));

                        nextElement.Add(tempElement);
                    }
                }


                #endregion

                #region Right -> <- Bottom

                if (rootElement.GetReferencePoint(EGridElementNeighborSide.Right) != null && rootElement.GetReferencePoint(EGridElementNeighborSide.Bottom) != null)
                {
                    if (rootElement.GetReferencePoint(EGridElementNeighborSide.Right).GetReferencePoint(EGridElementNeighborSide.Bottom) == null && rootElement.GetReferencePoint(EGridElementNeighborSide.Bottom).GetReferencePoint(EGridElementNeighborSide.Right) == null)
                    {
                        GridElement tempElement = new GridElement(rootElement.X + 1, rootElement.Y - 1, gridElementObjectProvider);

                        rootElement.GetReferencePoint(EGridElementNeighborSide.Right).AddReferencePoint(EGridElementNeighborSide.Bottom, tempElement);
                        rootElement.GetReferencePoint(EGridElementNeighborSide.Bottom).AddReferencePoint(EGridElementNeighborSide.Right, tempElement);

                        tempElement.AddReferencePoint(EGridElementNeighborSide.Top, rootElement.GetReferencePoint(EGridElementNeighborSide.Right));
                        tempElement.AddReferencePoint(EGridElementNeighborSide.Left, rootElement.GetReferencePoint(EGridElementNeighborSide.Bottom));

                        nextElement.Add(tempElement);
                    }
                }

                #endregion

            }

            currentElements = nextElement;
        }

    }

    public bool Clear()
    {
        return ForEachPoint(SetEmptyAndDropTarget);
    }

    #region Apply For Each Eelemnt

    private GridElement ForEachPoint(int x, int y, Func<GridElement, int, int, GridElement> handler)
    {
        return ApplyForEachPoint(rootElement, x, y, handler);
    }

    private bool ForEachPoint(Func<GridElement, bool> handler)
    {
        return ApplyForEachPoint(rootElement, handler);
    }

    private GridElement ApplyForEachPoint(GridElement currentElement, int x, int y, Func<GridElement, int, int, GridElement> handler)
    {
        if (currentElement == null)
        {
            return null;
        }

        GridElement topElement = currentElement.GetReferencePoint(EGridElementNeighborSide.Top);
        GridElement rightElement = currentElement.GetReferencePoint(EGridElementNeighborSide.Right);
        GridElement nextElement = (currentElement.GetReferencePoint(EGridElementNeighborSide.Top) == null) ? null : currentElement.GetReferencePoint(EGridElementNeighborSide.Top).GetReferencePoint(EGridElementNeighborSide.Right);

        if (handler(currentElement, x, y) != null)
        {
            return handler(currentElement, x, y);
        }

        while (topElement != null)
        {
            if (handler(topElement, x, y) != null)
            {
                return handler(topElement, x, y);
            }

            topElement = topElement.GetReferencePoint(EGridElementNeighborSide.Top);
        }

        while (rightElement != null)
        {
            if (handler(rightElement, x, y) != null)
            {
                return handler(rightElement, x, y);
            }

            rightElement = rightElement.GetReferencePoint(EGridElementNeighborSide.Right);
        }

        return ApplyForEachPoint(nextElement, x, y, handler);
    }

    private bool ApplyForEachPoint(GridElement currentElement, Func<GridElement, bool> handler)
    {
        if (currentElement == null)
        {
            return true;
        }

        GridElement topElement = currentElement.GetReferencePoint(EGridElementNeighborSide.Top);
        GridElement rightElement = currentElement.GetReferencePoint(EGridElementNeighborSide.Right);
        GridElement nextElement = (currentElement.GetReferencePoint(EGridElementNeighborSide.Top) == null) ? null : currentElement.GetReferencePoint(EGridElementNeighborSide.Top).GetReferencePoint(EGridElementNeighborSide.Right);

        handler(currentElement);

        while (topElement != null)
        {
            handler(topElement);

            topElement = topElement.GetReferencePoint(EGridElementNeighborSide.Top);
        }

        while (rightElement != null)
        {
            handler(rightElement);

            rightElement = rightElement.GetReferencePoint(EGridElementNeighborSide.Right);
        }

        return ApplyForEachPoint(nextElement, handler);
    }

    private GridElement GetPointByIndex(GridElement element, int x, int y)
    {
        if (element.X == x && element.Y == y)
        {
            return element;
        }

        return null;
    }

    private bool SetEmptyAndDropTarget(GridElement element)
    {
        element.SetEmpty();

        return false;
    }

    #endregion Apply For Each Eelemnt

    #endregion Methods
}