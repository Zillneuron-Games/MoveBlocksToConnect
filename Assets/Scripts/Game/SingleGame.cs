using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SingleGame : AGame
{
    protected List<BuildingBlock> redBuildingBlocks;

    public SingleGame(GameBoardGrid gameBoardGrid, int id, int stepsBest, int coinsBest, int stepsMinimum, int playedNumber, List<BuildingBlock> redBuildingBlocks, List<MobileBlock> mobileBlock, List<StaticBlock> staticBlocks, Stack<GameplayStep> allMoves)
                         : base(gameBoardGrid, id, stepsBest, coinsBest, stepsMinimum, playedNumber, mobileBlock, staticBlocks, allMoves)
    {
        this.redBuildingBlocks = redBuildingBlocks;
    }

    protected override void MoveUP()
    {
        bool isNewStepDone = false;

        List<int> unmovableBlockGroups = CheckGroupedBlocksMovement(redBuildingBlocks, EGridElementNeighborSide.Top);

        List<ABlock> allMovableBlocks = new List<ABlock>();

        allMovableBlocks.AddRange(redBuildingBlocks);

        if (mobileBlocks != null && mobileBlocks.Count > 0)
        {
            foreach (MobileBlock block in mobileBlocks)
            {
                allMovableBlocks.Add(block);
            }
        }

        for (int i = 0; i < allMovableBlocks.Count; i++)
        {
            foreach (ABlock block in allMovableBlocks)
            {
                if (IsBlockMovable(unmovableBlockGroups, block) && !block.IsInTransit)
                {
                    if (block.CurrentElement.GetReferencePoint(EGridElementNeighborSide.Top) != null && block.CurrentElement.GetReferencePoint(EGridElementNeighborSide.Top).State == EGridElementState.Empty)
                    {
                        block.CurrentElement.GetReferencePoint(EGridElementNeighborSide.Top).SetFull();
                        block.CurrentElement.SetEmpty();
                        block.ChangePoint(block.CurrentElement.GetReferencePoint(EGridElementNeighborSide.Top));
                        block.TransitTransform();

                        if (!isNewStepDone)
                        {
                            isNewStepDone = true;
                        }
                    }
                }
            }
        }

        if (isNewStepDone)
        {
            isNewStepDone = false;

            stepsCounter++;

            if (backStepsCounter < backStepsMaximum)
            {
                backStepsCounter++;
            }

            SortedDictionary<int, Vector2> allBlocksPositions = new SortedDictionary<int, Vector2>();

            foreach (ABlock block in allMovableBlocks)
            {
                block.FinalTransform();

                allBlocksPositions.Add(block.Id, block.CurrentElement.Position);
            }

            GameplayStep nextStep = new GameplayStep(allMoves.Count, EDirection.Up, allBlocksPositions);

            allMoves.Push(nextStep);

            SoundManager.Instance.PlayStoneMove();
            ThrowFinalTransformEvent();
            CalculateGroups();
        }
    }

    protected override void MoveDOWN()
    {
        bool isNewStepDone = false;

        List<int> unmovableBlockGroups = CheckGroupedBlocksMovement(redBuildingBlocks, EGridElementNeighborSide.Bottom);

        List<ABlock> allMovableBlocks = new List<ABlock>();

        allMovableBlocks.AddRange(redBuildingBlocks);

        if (mobileBlocks != null && mobileBlocks.Count > 0)
        {
            foreach (MobileBlock block in mobileBlocks)
            {
                allMovableBlocks.Add(block);
            }
        }

        for (int i = 0; i < allMovableBlocks.Count; i++)
        {
            foreach (ABlock block in allMovableBlocks)
            {
                if (IsBlockMovable(unmovableBlockGroups, block) && !block.IsInTransit)
                {
                    if (block.CurrentElement.GetReferencePoint(EGridElementNeighborSide.Bottom) != null && block.CurrentElement.GetReferencePoint(EGridElementNeighborSide.Bottom).State == EGridElementState.Empty)
                    {
                        block.CurrentElement.GetReferencePoint(EGridElementNeighborSide.Bottom).SetFull();
                        block.CurrentElement.SetEmpty();
                        block.ChangePoint(block.CurrentElement.GetReferencePoint(EGridElementNeighborSide.Bottom));
                        block.TransitTransform();

                        if (!isNewStepDone)
                        {
                            isNewStepDone = true;
                        }
                    }
                }
            }
        }

        if (isNewStepDone)
        {
            isNewStepDone = false;

            stepsCounter++;

            if (backStepsCounter < backStepsMaximum)
            {
                backStepsCounter++;
            }

            SortedDictionary<int, Vector2> allBlocksPositions = new SortedDictionary<int, Vector2>();

            foreach (ABlock block in allMovableBlocks)
            {
                block.FinalTransform();

                allBlocksPositions.Add(block.Id, block.CurrentElement.Position);
            }

            GameplayStep nextStep = new GameplayStep(allMoves.Count, EDirection.Down, allBlocksPositions);

            allMoves.Push(nextStep);

            SoundManager.Instance.PlayStoneMove();
            ThrowFinalTransformEvent();
            CalculateGroups();
        }
    }

    protected override void MoveLEFT()
    {
        bool isNewStepDone = false;

        List<int> unmovableBlockGroups = CheckGroupedBlocksMovement(redBuildingBlocks, EGridElementNeighborSide.Left);

        List<ABlock> allMovableBlocks = new List<ABlock>();

        allMovableBlocks.AddRange(redBuildingBlocks);

        if (mobileBlocks != null && mobileBlocks.Count > 0)
        {
            foreach (MobileBlock block in mobileBlocks)
            {
                allMovableBlocks.Add(block);
            }
        }

        for (int i = 0; i < allMovableBlocks.Count; i++)
        {
            foreach (ABlock block in allMovableBlocks)
            {
                if (IsBlockMovable(unmovableBlockGroups, block) && !block.IsInTransit)
                {
                    if (block.CurrentElement.GetReferencePoint(EGridElementNeighborSide.Left) != null && block.CurrentElement.GetReferencePoint(EGridElementNeighborSide.Left).State == EGridElementState.Empty)
                    {
                        block.CurrentElement.GetReferencePoint(EGridElementNeighborSide.Left).SetFull();
                        block.CurrentElement.SetEmpty();
                        block.ChangePoint(block.CurrentElement.GetReferencePoint(EGridElementNeighborSide.Left));
                        block.TransitTransform();

                        if (!isNewStepDone)
                        {
                            isNewStepDone = true;
                        }
                    }
                }
            }
        }


        if (isNewStepDone)
        {
            isNewStepDone = false;

            stepsCounter++;

            if (backStepsCounter < backStepsMaximum)
            {
                backStepsCounter++;
            }

            SortedDictionary<int, Vector2> allBlocksPositions = new SortedDictionary<int, Vector2>();

            foreach (ABlock block in allMovableBlocks)
            {
                block.FinalTransform();

                allBlocksPositions.Add(block.Id, block.CurrentElement.Position);
            }

            GameplayStep nextStep = new GameplayStep(allMoves.Count, EDirection.Left, allBlocksPositions);

            allMoves.Push(nextStep);

            SoundManager.Instance.PlayStoneMove();
            ThrowFinalTransformEvent();
            CalculateGroups();
        }
    }

    protected override void MoveRIGHT()
    {
        bool isNewStepDone = false;

        List<int> unmovableBlockGroups = CheckGroupedBlocksMovement(redBuildingBlocks, EGridElementNeighborSide.Right);

        List<ABlock> allMovableBlocks = new List<ABlock>();

        allMovableBlocks.AddRange(redBuildingBlocks);

        if (mobileBlocks != null && mobileBlocks.Count > 0)
        {
            foreach (MobileBlock block in mobileBlocks)
            {
                allMovableBlocks.Add(block);
            }
        }

        for (int i = 0; i < allMovableBlocks.Count; i++)
        {
            foreach (ABlock block in allMovableBlocks)
            {
                if (IsBlockMovable(unmovableBlockGroups, block) && !block.IsInTransit)
                {
                    if (block.CurrentElement.GetReferencePoint(EGridElementNeighborSide.Right) != null && block.CurrentElement.GetReferencePoint(EGridElementNeighborSide.Right).State == EGridElementState.Empty)
                    {
                        block.CurrentElement.GetReferencePoint(EGridElementNeighborSide.Right).SetFull();
                        block.CurrentElement.SetEmpty();
                        block.ChangePoint(block.CurrentElement.GetReferencePoint(EGridElementNeighborSide.Right));
                        block.TransitTransform();

                        if (!isNewStepDone)
                        {
                            isNewStepDone = true;
                        }
                    }
                }
            }
        }

        if (isNewStepDone)
        {
            isNewStepDone = false;

            stepsCounter++;

            if (backStepsCounter < backStepsMaximum)
            {
                backStepsCounter++;
            }

            SortedDictionary<int, Vector2> allBlocksPositions = new SortedDictionary<int, Vector2>();

            foreach (ABlock block in allMovableBlocks)
            {
                block.FinalTransform();

                allBlocksPositions.Add(block.Id, block.CurrentElement.Position);
            }

            GameplayStep nextStep = new GameplayStep(allMoves.Count, EDirection.Right, allBlocksPositions);

            allMoves.Push(nextStep);

            SoundManager.Instance.PlayStoneMove();
            ThrowFinalTransformEvent();
            CalculateGroups();
        }
    }

    protected override void MoveBACK()
    {
        if (backStepsCounter == 0)
        {
            return;
        }

        backStepsCounter--;

        if (allMoves.Count > 1)
        {
            allMoves.Pop();

            GameplayStep prevStep = allMoves.Peek();

            stepsCounter--;

            List<ABlock> allMovableBlocks = new List<ABlock>();

            allMovableBlocks.AddRange(redBuildingBlocks);

            if (mobileBlocks != null && mobileBlocks.Count > 0)
            {
                foreach (MobileBlock block in mobileBlocks)
                {
                    allMovableBlocks.Add(block);
                }
            }

            foreach (ABlock block in allMovableBlocks)
            {
                block.CurrentElement.SetEmpty();

                Vector2 blockPosition = prevStep.GetPositionById(block.Id);

                block.ChangePoint(gameBoardGrid[(int)blockPosition.x, (int)blockPosition.y]);

                block.CurrentElement.SetFull();
            }
        }

        SoundManager.Instance.PlayStoneMove();
        ThrowFinalTransformEvent();
        CalculateGroups();
    }

    protected override void StartStoneMatchEffects()
    {
        redBuildingBlocks.ForEach(m => m.StartStoneMatchEffects());
    }

    public override void PutBlockObjects()
    {
        List<ABlock> allBlocks = new List<ABlock>();

        allBlocks.AddRange(redBuildingBlocks);

        if (mobileBlocks != null && mobileBlocks.Count > 0)
        {
            foreach (MobileBlock block in mobileBlocks)
            {
                allBlocks.Add(block);
            }
        }

        if (staticBlocks != null && staticBlocks.Count > 0)
        {
            foreach (StaticBlock block in staticBlocks)
            {
                allBlocks.Add(block);
            }
        }

        foreach (ABlock block in allBlocks)
        {
            block.BlockPosition = new Vector3(block.CurrentElement.X, block.CurrentElement.Y, 0.0f);
        }
    }

    public override void RemoveBlockObjects()
    {
        List<ABlock> allBlocks = new List<ABlock>();

        allBlocks.AddRange(redBuildingBlocks);

        if (mobileBlocks != null && mobileBlocks.Count > 0)
        {
            foreach (MobileBlock block in mobileBlocks)
            {
                allBlocks.Add(block);
            }
        }

        if (staticBlocks != null && staticBlocks.Count > 0)
        {
            foreach (StaticBlock block in staticBlocks)
            {
                allBlocks.Add(block);
            }
        }

        foreach (ABlock block in allBlocks)
        {
            block.BlockPosition = new Vector3(200, 200, 200);
        }
    }

    public override void MoveBlockObjects(float lerpAlpha, float minDistance)
    {
        List<ABlock> allBlocks = new List<ABlock>();

        allBlocks.AddRange(redBuildingBlocks);

        if (mobileBlocks != null && mobileBlocks.Count > 0)
        {
            foreach (MobileBlock block in mobileBlocks)
            {
                allBlocks.Add(block);
            }
        }

        foreach (ABlock block in allBlocks)
        {
            if (Vector3.Distance(block.BlockPosition, new Vector3(block.CurrentElement.X, block.CurrentElement.Y, 0.0f)) >= minDistance)
            {
                block.BlockPosition = Vector3.Lerp(block.BlockPosition, new Vector3(block.CurrentElement.X, block.CurrentElement.Y, 0.0f), lerpAlpha);
            }
            else
            {
                block.BlockPosition = new Vector3(block.CurrentElement.X, block.CurrentElement.Y, 0.0f);
            }
        }

        foreach (ABlock block in allBlocks)
        {
            if (block.BlockPosition != new Vector3(block.CurrentElement.X, block.CurrentElement.Y, 0.0f))
            {
                return;
            }
        }

        ThrowTransitOverEvent();

        if (IsMatchEnded())
        {
            SoundManager.Instance.PlayStoneStop();

            StartStoneMatchEffects();
            ThrowStonesMatchEvent();

            return;
        }

        if (stepsCounter > GameStartData.MaximumStepsCount)
        {
            ThrowErrorEvent(EErrorType.StepsCount);
        }
    }



    private bool IsBlockMovable(List<int> unmovableBlockGroups, ABlock block)
    {
        BuildingBlock buildingBlock = block as BuildingBlock;
        if (buildingBlock != null && unmovableBlockGroups.Contains(buildingBlock.GroupId))
        {
            return false;
        }
        return true;
    }

    private List<int> CheckGroupedBlocksMovement(List<BuildingBlock> redBuildingBlocks, EGridElementNeighborSide elementNeighborSide)
    {
        List<int> unmovableBlockGroups = new List<int>();

        foreach (var block in redBuildingBlocks)
        {
            if(block.GroupId > 0 && !unmovableBlockGroups.Contains(block.GroupId))
            {
                if(!BlockIsMovable(block.CurrentElement, elementNeighborSide))
                {
                    unmovableBlockGroups.Add(block.GroupId);
                }
            }
        }
        return unmovableBlockGroups;
    }

    private bool BlockIsMovable(GridElement blockGridElement, EGridElementNeighborSide elementNeighborSide)
    {
        GridElement currentElementReferencePoint = blockGridElement.GetReferencePoint(elementNeighborSide);
        
        if (currentElementReferencePoint == null)
        {
            return false;
        }
        else if (currentElementReferencePoint.State == EGridElementState.Inaccessible)
        {
            return false;
        }
        else if(currentElementReferencePoint.State == EGridElementState.Full)
        {
            return BlockIsMovable(currentElementReferencePoint, elementNeighborSide);
        }

        return true;
    }


    private void CalculateGroups()
    {
        Dictionary<GridElement, BuildingBlock> blocksGridElements = new Dictionary<GridElement, BuildingBlock>();

        foreach (var block in redBuildingBlocks)
        {
            block.ResetGroupId();
            blocksGridElements[block.CurrentElement] = block;
        }

        for (int i = 0; i < redBuildingBlocks.Count; i++)
        {
            CalculateBlockGroup(i + 1, redBuildingBlocks[i].CurrentElement, blocksGridElements);
        }

        Dictionary<int, List<BuildingBlock>> redBuildingBlocksGroups = redBuildingBlocks.GroupBy(m => m.GroupId).ToDictionary(m => m.Key, m => m.ToList());

        foreach (var groups in redBuildingBlocksGroups)
        {
            if(groups.Value.Count == 1)
            {
                foreach (var block in groups.Value)
                {
                    block.ResetGroupId();
                }
            }
        }
    }

    private void CalculateBlockGroup(int index, GridElement gridElement, Dictionary<GridElement, BuildingBlock> blocksGridElements)
    {
        if (blocksGridElements.TryGetValue(gridElement, out BuildingBlock currentBlock))
        {
            if (currentBlock.GroupId == 0)
            {
                currentBlock.SetGroupId(index);

                CalculateReferencePointBlockGroup(index, gridElement, blocksGridElements, EGridElementNeighborSide.Top);
                CalculateReferencePointBlockGroup(index, gridElement, blocksGridElements, EGridElementNeighborSide.Bottom);
                CalculateReferencePointBlockGroup(index, gridElement, blocksGridElements, EGridElementNeighborSide.Left);
                CalculateReferencePointBlockGroup(index, gridElement, blocksGridElements, EGridElementNeighborSide.Right);
            }
        }
    }

    private void CalculateReferencePointBlockGroup(int index, GridElement gridElement, Dictionary<GridElement, BuildingBlock> blocksGridElements, EGridElementNeighborSide referencePointSide)
    {
        GridElement blockGridElement = gridElement.GetReferencePoint(referencePointSide);

        if (blockGridElement != null && blocksGridElements.TryGetValue(blockGridElement, out BuildingBlock block))
        {
            CalculateBlockGroup(index, block.CurrentElement, blocksGridElements);
        }
    }

    private bool IsMatchEnded()
    {
        int firstGroupId = redBuildingBlocks[0].GroupId;
        return redBuildingBlocks.All(m => m.GroupId > 0 && m.GroupId == firstGroupId);
    }
}