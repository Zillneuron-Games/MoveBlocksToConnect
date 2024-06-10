using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStartData
{
    private const string SoundPrefsKey = "sound";
    public const string GameDataPrefsKey = "gamedata";
    public const int GamesCount = 105;
    public const int MaximumStepsCount = 998;

    private int totalCoins;
    private int nextToLoadGameId;
    private int lastGame;
    private GameData[] gamesData;
    private GameDataDynamic[] gamesDataDynamic;
    private Dictionary<ESceneName, string> sceneNames;

    public bool SoundState
    {
        get
        {
            if (PlayerPrefs.GetInt(SoundPrefsKey) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        set
        {
            if (value)
            {
                PlayerPrefs.SetInt(SoundPrefsKey, 1);
            }
            else
            {
                PlayerPrefs.SetInt(SoundPrefsKey, 0);
            }
        }
    }
    public int NextToLoadGame
    {
        get { return nextToLoadGameId; }
        set
        {
            if (value >= 1 && value <= GamesCount)
                nextToLoadGameId = value;
        }
    }
    public int LastGame => lastGame;
    public int TotalCoins => totalCoins;
    public Dictionary<ESceneName, string> SceneNames => sceneNames;

    public static GameStartData CreateInstance()
    {
        GameData[] gameData = new GameData[GamesCount];
        GameDataDynamic[] gameDataDynamic = new GameDataDynamic[GamesCount];

        for (int i = 0; i < GamesCount; i++)
        {
            gameData[i] = DataReader.GetGameData(i + 1);
            gameDataDynamic[i] = DataReader.GetGameDataDynamic(i + 1);
        }
        return new GameStartData(gameData, gameDataDynamic);
    }

    public GameStartData(GameData[] gamesData, GameDataDynamic[] gamesDataDynamic)
    {
        this.totalCoins = 0;
        this.nextToLoadGameId = 1;
        this.lastGame = 0;
        this.gamesData = gamesData;
        this.gamesDataDynamic = gamesDataDynamic;
        this.sceneNames = new Dictionary<ESceneName, string>() { {ESceneName.Load, "LOAD"},
                                                                 {ESceneName.Menu, "MENU"},
                                                                 {ESceneName.Game, "GAME"}
                                                               };
    }

    public void UpdateDynamicData(GameDataDynamic gameDataDynamic)
    {
        if (gamesDataDynamic[gameDataDynamic.Id - 1].BestCoins < gameDataDynamic.BestCoins)
        {
            totalCoins = totalCoins + gameDataDynamic.BestCoins - gamesDataDynamic[gameDataDynamic.Id - 1].BestCoins;
        }

        gamesDataDynamic[gameDataDynamic.Id - 1] = gameDataDynamic;

        DataReader.SetGameDataDynamic(gameDataDynamic);

        if (gameDataDynamic.Id < GamesCount)
        {
            GameDataDynamic nextGameDataDynamic = gamesDataDynamic[gameDataDynamic.Id];

            if (nextGameDataDynamic.GameCount == 0)
            {
                nextGameDataDynamic.GameCount = 1;
                lastGame = nextGameDataDynamic.Id;
                DataReader.SetGameDataDynamic(nextGameDataDynamic);
            }
        }
    }

    public GameData GetGameData(int gameId)
    {
        return gamesData[gameId - 1];
    }

    public GameDataDynamic GetGameDataDynamic(int gameId)
    {
        return gamesDataDynamic[gameId - 1];
    }
}
