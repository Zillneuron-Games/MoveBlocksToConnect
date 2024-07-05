using System;
using UnityEngine;

public class BuildingBlock : ABlock
{
    private int groupId;

    public int GroupId => groupId;

    public BuildingBlock(int id, int groupId, GameObject blockObject, GridElement gridElement) : base(id, blockObject, gridElement)
    {
        this.groupId = groupId;
        this.isMovable = true;

        gridElement.SetFull();

        SetActive(true);
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

    public void StartStoneMatchEffects()
    {
        SetActive(false);
    }

    private void SetActive(bool value)
    {
        Debug.LogError($"Inscription Block -> SetActive : {value}");
    }

    public void SetGroupId(int id)
    {
        groupId = id;
    }

    public void ResetGroupId()
    {
        groupId = 0;
    }
}
