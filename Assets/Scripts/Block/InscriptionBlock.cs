using System;
using UnityEngine;

public class InscriptionBlock : ABlock
{
    public InscriptionBlock(int id, GameObject blockObject, GridElement gridElement) : base(id, blockObject, gridElement)
    {
        isMovable = true;
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
}
