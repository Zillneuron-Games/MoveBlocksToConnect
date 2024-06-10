using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public static class DataReader
{
    private const string gameDataPath = @"Data/";

    public static void SetGameData(GameData gameData)
    {
        /* 
         JsonWriter writer = new JsonWriter();
         writer.PrettyPrint = true;

         JsonMapper.ToJson(_game_data, writer);

         string _game_data_json = @writer.ToString();
         */

        string gameDataJson = JsonUtility.ToJson(gameData);
        string dataPathFull = string.Format("Assets/Resources/{0}{1}.txt", gameDataPath, gameData.Id.ToString());

        StreamWriter streemWriter = new StreamWriter(dataPathFull);
        streemWriter.Write(gameDataJson);
        streemWriter.Close();
    }

    public static void SetGameDataDynamic(GameDataDynamic gameDataDynamic)
    {
        if (gameDataDynamic.Id > GameStartData.GamesCount || gameDataDynamic.Id < 1)
        {
            return;
        }
        /*
        JsonWriter writer = new JsonWriter();
        writer.PrettyPrint = true;

        JsonMapper.ToJson(_game_data_dynamic, writer);

        string _game_data_dynamic_json = @writer.ToString();
        */

        string gameDataDynamicJson = JsonUtility.ToJson(gameDataDynamic);

        string dataPathFull = string.Format("{0}{1}", GameStartData.GameDataPrefsKey, gameDataDynamic.Id.ToString());

        PlayerPrefs.SetString(dataPathFull, gameDataDynamicJson);
    }

    public static GameData GetGameData(int gameId)
    {
        if (gameId > GameStartData.GamesCount || gameId < 1)
        {
            return null;
        }

        string dataPathFull = string.Format("{0}{1}", gameDataPath, gameId.ToString());

        TextAsset gameInfoJson = Resources.Load(dataPathFull, typeof(TextAsset)) as TextAsset;

        GameDataOld gameDataOld = JsonUtility.FromJson<GameDataOld>(gameInfoJson.text);
        //GameData gameData = JsonUtility.FromJson<GameData>(gameInfoJson.text);
        GameData gameData = new GameData(gameDataOld.ID, gameDataOld.MinimumStepsCount, gameDataOld.InscriptionStonePositionRed, gameDataOld.InscriptionStonePositionBlue, gameDataOld.InscriptionStonePositionYellow, gameDataOld.InscriptionStoneTargetPositionRed, gameDataOld.InscriptionStoneTargetPositionBlue, gameDataOld.InscriptionStoneTargetPositionYellow, gameDataOld.MobilStonesPositions, gameDataOld.StaticStonesPositions);

        //GameData _game_data = JsonMapper.ToObject<GameData>(_game_info_json.text);

        return gameData;
    }

    public static GameDataDynamic GetGameDataDynamic(int gameId)
    {
        if (gameId > GameStartData.GamesCount || gameId < 1)
        {
            return null;
        }

        string dataPathFull = string.Format("{0}{1}", GameStartData.GameDataPrefsKey, gameId.ToString());

        string gameInfoJson = PlayerPrefs.GetString(dataPathFull);

        if(string.IsNullOrEmpty(gameInfoJson))
        {
            SetUpGameDataDynamic();
            return GetGameDataDynamic(gameId);
        }
        GameDataDynamic gameDataDynamic = JsonUtility.FromJson<GameDataDynamic>(gameInfoJson);
        //GameDataDynamic _game_data_dynamic = JsonMapper.ToObject<GameDataDynamic>(_game_info_json);

        return gameDataDynamic;
    }

    private static void SetUpGameDataDynamic()
    {
        for (int i = 0; i < GameStartData.GamesCount; i++)
        {
            int gamePlayedCount = 0;

            if (i == 0)
            {
                gamePlayedCount = 1;
            }


            GameDataDynamic gameDataDynamic = new GameDataDynamic(i + 1, GameStartData.MaximumStepsCount, 0, gamePlayedCount);
            SetGameDataDynamic(gameDataDynamic);
        }
    }
}
