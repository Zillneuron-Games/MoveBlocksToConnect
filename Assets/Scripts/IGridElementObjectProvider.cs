using System;
using UnityEngine;

public interface IGridElementObjectProvider
{
    GameObject GetGridElementObject(int x, int y);
}
