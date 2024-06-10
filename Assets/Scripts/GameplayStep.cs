using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameplayStep
{
    #region Variables

    private int chainIndex;
    private EDirection direction;
    private SortedDictionary<int, Vector2> blocksPositions;

    #endregion Variables

    #region Properties

    public int Index => chainIndex;
   
    #endregion Properties

    #region Constructors

    public GameplayStep(int chainIndex, EDirection direction, SortedDictionary<int, Vector2> blocksPositions)
    {
        this.chainIndex = chainIndex;
        this.direction = direction;
        this.blocksPositions = blocksPositions;
    }

    #endregion Constructors

    #region Methods

    public Vector2 GetPositionById(int id)
    {
        return blocksPositions[id];
    }

    #region Override

    public static bool operator ==(GameplayStep stepFirst, GameplayStep stepSecond)
    {
        if ((object)stepFirst == null && (object)stepSecond == null)
        {
            return true;
        }

        if ((object)stepFirst == null)
        {
            return false;
        }

        if ((object)stepSecond == null)
        {
            return false;
        }

        if (stepFirst.blocksPositions.Count != stepSecond.blocksPositions.Count)
        {
            return false;
        }

        for (int index = 0; index < stepFirst.blocksPositions.Count; index++)
        {
            if (stepFirst.blocksPositions.ElementAt(index).Key == stepSecond.blocksPositions.ElementAt(index).Key)
            {
                if (stepFirst.blocksPositions.ElementAt(index).Value != stepSecond.blocksPositions.ElementAt(index).Value)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        return true;
    }

    public static bool operator !=(GameplayStep stepFirst, GameplayStep stepSecond)
    {
        if ((object)stepFirst == null && (object)stepSecond == null)
        {
            return false;
        }

        if ((object)stepFirst == null)
        {
            return true;
        }

        if ((object)stepSecond == null)
        {
            return true;
        }

        if (stepFirst.blocksPositions.Count != stepSecond.blocksPositions.Count)
        {
            return true;
        }

        for (int index = 0; index < stepFirst.blocksPositions.Count; index++)
        {
            if (stepFirst.blocksPositions.ElementAt(index).Key == stepSecond.blocksPositions.ElementAt(index).Key)
            {
                if (stepFirst.blocksPositions.ElementAt(index).Value != stepSecond.blocksPositions.ElementAt(index).Value)
                {
                    return true;
                }
            }
            else
            {
                return true;
            }

        }

        return false;
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }

        GameplayStep step = obj as GameplayStep;

        if ((object)step != null)
        {
            if (blocksPositions.Count != step.blocksPositions.Count)
            {
                return false;
            }

            for (int index = 0; index < step.blocksPositions.Count; index++)
            {
                if (step.blocksPositions.ElementAt(index).Key == blocksPositions.ElementAt(index).Key)
                {
                    if (step.blocksPositions.ElementAt(index).Value != blocksPositions.ElementAt(index).Value)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }

            return true;
        }
        else
            return false;

    }

    public bool Equals(GameplayStep step)
    {
        if ((object)step == null)
        {
            return false;
        }

        if (blocksPositions.Count != step.blocksPositions.Count)
        {
            return false;
        }

        for (int index = 0; index < step.blocksPositions.Count; index++)
        {
            if (step.blocksPositions.ElementAt(index).Key == blocksPositions.ElementAt(index).Key)
            {
                if (step.blocksPositions.ElementAt(index).Value != blocksPositions.ElementAt(index).Value)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        return true;
    }

    public override int GetHashCode()
    {
        return chainIndex ^ (int)direction;
    }

    #endregion

    #endregion Methods
}
