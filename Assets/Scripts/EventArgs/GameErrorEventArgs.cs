using System;
using UnityEngine;

public class GameErrorEventArgs : EventArgs
{
    private EErrorType errorType;

    public EErrorType ErrorType
    {
        get { return errorType; }
    }

    public GameErrorEventArgs(EErrorType errorType)
    {
        this.errorType = errorType;
    }
}
