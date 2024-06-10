using System;

public enum EGameplayState : byte
{
    Start = 0,
    Gameplay= 1,
    Transit = 2,
    Pause = 3,
    Win = 4,
    End = 5,
    Error = 6
}
