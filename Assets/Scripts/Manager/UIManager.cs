using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private int steps;
    private int coins;

    private int bestSteps;
    private int bestCoins;
    private int minimalSteps;

    private EGameplayState gameMode;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public bool ChangeGameplayState(EGameplayState gameMode, Action nextAction)
    {
        return true;
    }

    public void UpdateSteps(int stepsCount)
    {
        steps = stepsCount;
    }

    public void UpdateGameInfo(int stepsCount, int bestSteps, int coins, int bestCoins)
    {
        this.steps = stepsCount;
        this.bestSteps = bestSteps;
        this.bestCoins = bestCoins;
        this.coins = coins;
    }

    public void CreateGame(int minimalSteps, int bestSteps, int bestCoins)
    {
        this.minimalSteps = minimalSteps;
        this.steps = 0;
        this.coins = 0;
        this.bestSteps = bestSteps;
        this.bestCoins = bestCoins;

        if (this.bestSteps < GameStartData.MaximumStepsCount)
        {

        }
        else
        {

        }

        //if (gameStartData.NextToLoadGame == GameStartData.GamesCount)
        //{
           
        //}
        //else
        //{
           
        //}
    }

    public void UpdateErrorInformation(EErrorType errorType)
    {
        switch (errorType)
        {
            case EErrorType.AvailableSteps : ; break;
            case EErrorType.StepsCount : ; break;
        }
    }

    public void Disable()
    {

    }
}
