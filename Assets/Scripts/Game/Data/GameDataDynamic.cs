using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class GameDataDynamic
{
    public int Id;
    public int BestSteps;
    public int BestCoins;
    public int GameCount;

    public GameDataDynamic()
    {
        Id = 0;
        BestSteps = 0;
        GameCount = 0;
        BestCoins = 0;
    }

    public GameDataDynamic(int id, int bestSteps, int bestCoins, int gameCount)
    {
        Id = id;
        BestSteps = bestSteps;
        GameCount = gameCount;
        BestCoins = bestCoins;
    }

    public void UpdateBestStep(int bestStep)
    {
        if (bestStep < BestSteps)
        {
            BestSteps = bestStep;
        }
    }

    public void UpdateBestCoins(int bestCoins)
    {
        if (bestCoins > BestCoins)
        {
            BestCoins = bestCoins;
        }
    }
}

