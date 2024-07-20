using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour, IGridElementObjectProvider
{
    #region Events

    public event EventHandler<EventArgs> onStartGame;
    public event EventHandler<EventArgs> onPauseGame;
    public event EventHandler<EventArgs> onWinGame;
    public event EventHandler<EventArgs> onShowMenu;

    #endregion Events

    #region Variables

    private AsyncOperation sceneLoader;

    private float gameEndPauseTime;
    private float blocksMoveSpeed;
    private float blocksMinDistance;

    #region Inspector

    public GameObject redTileBlock;
    public GameObject greenTileBlock;
    public GameObject blueTileBlock;
    public GameObject mobileBlock;
    public GameObject staticBlock;

    public GameObjectRow[] gridBlocks;

    #endregion Inspector

    public EGameplayState gameplayState;
    private GameStartData gameStartData;
    private GameBoardGrid gameBoardGrid;

    private AGame currentGame;

    #endregion Variables

    private void Awake()
    {
        gameplayState = EGameplayState.Start;
    }

    private void Start()
    {
        sceneLoader = null;

        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        gameStartData = GameStartData.CreateInstance();

        blocksMoveSpeed = 16.0f;
        blocksMinDistance = 0.05f;

        SoundManager.Instance.Switch(gameStartData.SoundState);
        InputManager.Instance.eventInput += HandleInputEvent;

        gameBoardGrid = new GameBoardGrid(16, 16, this);

        CreateGame(gameStartData.NextToLoadGame);

        yield return new WaitForEndOfFrame();

        InputManager.Instance.Enable();
    }

    private void Update()
    {
        switch (gameplayState)
        {
            case EGameplayState.Gameplay: UpdateGame(); break;
            case EGameplayState.Transit: UpdateTransit(); break;
            case EGameplayState.Pause: UpdatePause(); break;
        }
    }

    private void ChangeGameplayState(EGameplayState gameplayState)
    {
        this.gameplayState = gameplayState;

        switch (gameplayState)
        {
            case EGameplayState.Gameplay:
            case EGameplayState.Pause:
            case EGameplayState.End: UIManager.Instance.ChangeGameplayState(this.gameplayState, InputManager.Instance.Enable); break;
            case EGameplayState.Error: UIManager.Instance.ChangeGameplayState(this.gameplayState, InputManager.Instance.Enable); break;
            case EGameplayState.Win: UIManager.Instance.Disable(); UIManager.Instance.ChangeGameplayState(this.gameplayState, null); break;
            case EGameplayState.Transit: UIManager.Instance.Disable(); UIManager.Instance.ChangeGameplayState(this.gameplayState, null); break;
        }
    }

    private void CreateGame(int gameId)
    {
        if (currentGame != null)
        {
            DestroyGame();
            gameBoardGrid.Clear();
        }

        GameData gameData = gameStartData.GetGameData(gameId);

        GameDataDynamic gameDataDynamic = gameStartData.GetGameDataDynamic(gameId);

        if (gameData.IsSingleGame)
        {
            int indexCounter = 1;

            SortedDictionary<int, Vector2> allBlocksPositions = new SortedDictionary<int, Vector2>();

            List<TileBlock> redTileBlocks = new List<TileBlock>();
            foreach (var stonePosition in gameData.RedTileBlocksPositions)
            {
                redTileBlocks.Add(new TileBlock(indexCounter, 0, Instantiate(redTileBlock), gameBoardGrid[(int)stonePosition.x, (int)stonePosition.y]));
                allBlocksPositions.Add(indexCounter, stonePosition);
                indexCounter++;
            }

            List<TileBlock> blueTileBlocks = new List<TileBlock>();
            foreach (var stonePosition in gameData.BlueTileBlocksPositions)
            {
                blueTileBlocks.Add(new TileBlock(indexCounter, 0, Instantiate(blueTileBlock), gameBoardGrid[(int)stonePosition.x, (int)stonePosition.y]));
                allBlocksPositions.Add(indexCounter, stonePosition);
                indexCounter++;
            }

            List<TileBlock> greenTileBlocks = new List<TileBlock>();
            foreach (var stonePosition in gameData.GreenTileBlocksPositions)
            {
                greenTileBlocks.Add(new TileBlock(indexCounter, 0, Instantiate(greenTileBlock), gameBoardGrid[(int)stonePosition.x, (int)stonePosition.y]));
                allBlocksPositions.Add(indexCounter, stonePosition);
                indexCounter++;
            }

            List<MobileBlock> mobileBlocks = null;

            if (gameData.MobileBlocksPositions != null && gameData.MobileBlocksPositions.Count > 0)
            {
                mobileBlocks = new List<MobileBlock>();

                foreach (Vector2 pos in gameData.MobileBlocksPositions)
                {
                    MobileBlock tempBlock = new MobileBlock(indexCounter, Instantiate(mobileBlock), gameBoardGrid[(int)pos.x, (int)pos.y]);
                    mobileBlocks.Add(tempBlock);
                    allBlocksPositions.Add(indexCounter, pos);
                    indexCounter++;
                }
            }

            List<StaticBlock> staticBlocks = null;

            if (gameData.StaticBlocksPositions != null && gameData.StaticBlocksPositions.Count > 0)
            {
                staticBlocks = new List<StaticBlock>();

                foreach (Vector2 pos in gameData.StaticBlocksPositions)
                {
                    StaticBlock tempBlock = new StaticBlock(indexCounter, Instantiate(staticBlock), gameBoardGrid[(int)pos.x, (int)pos.y]);
                    staticBlocks.Add(tempBlock);
                    indexCounter++;
                }
            }

            Stack<GameplayStep> allStepsContainer = new Stack<GameplayStep>();
            allStepsContainer.Push(new GameplayStep(0, EDirection.None, allBlocksPositions));

            List<TileBlock> firstTileBlocks = redTileBlocks.Count > 0 ? redTileBlocks : blueTileBlocks.Count > 0 ? blueTileBlocks : greenTileBlocks;

            currentGame = new SingleGame(gameBoardGrid, gameData.Id, gameDataDynamic.BestSteps, gameDataDynamic.BestCoins, gameData.StepsMinimumCount, gameDataDynamic.GameCount, firstTileBlocks, mobileBlocks, staticBlocks, allStepsContainer);
        }
        else if (gameData.IsDoubleGame)
        {
            int indexCounter = 1;

            SortedDictionary<int, Vector2> allBlocksPositions = new SortedDictionary<int, Vector2>();

            List<TileBlock> redTileBlocks = new List<TileBlock>();
            foreach (var stonePosition in gameData.RedTileBlocksPositions)
            {
                redTileBlocks.Add(new TileBlock(indexCounter, 0, Instantiate(redTileBlock), gameBoardGrid[(int)stonePosition.x, (int)stonePosition.y]));
                allBlocksPositions.Add(indexCounter, stonePosition);
                indexCounter++;
            }

            List<TileBlock> blueTileBlocks = new List<TileBlock>();
            foreach (var stonePosition in gameData.BlueTileBlocksPositions)
            {
                blueTileBlocks.Add(new TileBlock(indexCounter, 0, Instantiate(blueTileBlock), gameBoardGrid[(int)stonePosition.x, (int)stonePosition.y]));
                allBlocksPositions.Add(indexCounter, stonePosition);
                indexCounter++;
            }

            List<TileBlock> greenTileBlocks = new List<TileBlock>();
            foreach (var stonePosition in gameData.GreenTileBlocksPositions)
            {
                greenTileBlocks.Add(new TileBlock(indexCounter, 0, Instantiate(greenTileBlock), gameBoardGrid[(int)stonePosition.x, (int)stonePosition.y]));
                allBlocksPositions.Add(indexCounter, stonePosition);
                indexCounter++;
            }

            List<MobileBlock> mobileBlocks = null;

            if (gameData.MobileBlocksPositions != null && gameData.MobileBlocksPositions.Count > 0)
            {
                mobileBlocks = new List<MobileBlock>();

                foreach (Vector2 pos in gameData.MobileBlocksPositions)
                {
                    MobileBlock tempBlock = new MobileBlock(indexCounter, Instantiate(mobileBlock), gameBoardGrid[(int)pos.x, (int)pos.y]);
                    mobileBlocks.Add(tempBlock);
                    allBlocksPositions.Add(indexCounter, pos);
                    indexCounter++;
                }
            }

            List<StaticBlock> staticBlocks = null;

            if (gameData.StaticBlocksPositions != null && gameData.StaticBlocksPositions.Count > 0)
            {
                staticBlocks = new List<StaticBlock>();

                foreach (Vector2 pos in gameData.StaticBlocksPositions)
                {
                    StaticBlock tempBlock = new StaticBlock(indexCounter, Instantiate(staticBlock), gameBoardGrid[(int)pos.x, (int)pos.y]);
                    staticBlocks.Add(tempBlock);
                    indexCounter++;
                }
            }

            Stack<GameplayStep> allStepsContainer = new Stack<GameplayStep>();
            allStepsContainer.Push(new GameplayStep(0, EDirection.None, allBlocksPositions));

            List<TileBlock> firstTileBlocks = redTileBlocks.Count > 0 ? redTileBlocks : blueTileBlocks.Count > 0 ? blueTileBlocks : greenTileBlocks;
            List<TileBlock> secondTileBlocks = redTileBlocks.Count > 0 && firstTileBlocks != redTileBlocks ? redTileBlocks : blueTileBlocks.Count > 0 && blueTileBlocks != redTileBlocks ? blueTileBlocks : greenTileBlocks;

            currentGame = new DoubleGame(gameBoardGrid, gameData.Id, gameDataDynamic.BestSteps, gameDataDynamic.BestCoins, gameData.StepsMinimumCount, gameDataDynamic.GameCount, firstTileBlocks, blueTileBlocks, mobileBlocks, staticBlocks, allStepsContainer);
        }
        else
        {
            int indexCounter = 1;

            SortedDictionary<int, Vector2> allBlocksPositions = new SortedDictionary<int, Vector2>();

            List<TileBlock> redTileBlocks = new List<TileBlock>();
            foreach (var stonePosition in gameData.RedTileBlocksPositions)
            {
                redTileBlocks.Add(new TileBlock(indexCounter, 0, Instantiate(redTileBlock), gameBoardGrid[(int)stonePosition.x, (int)stonePosition.y]));
                allBlocksPositions.Add(indexCounter, stonePosition);
                indexCounter++;
            }

            List<TileBlock> blueTileBlocks = new List<TileBlock>();
            foreach (var stonePosition in gameData.BlueTileBlocksPositions)
            {
                blueTileBlocks.Add(new TileBlock(indexCounter, 0, Instantiate(blueTileBlock), gameBoardGrid[(int)stonePosition.x, (int)stonePosition.y]));
                allBlocksPositions.Add(indexCounter, stonePosition);
                indexCounter++;
            }

            List<TileBlock> greenTileBlocks = new List<TileBlock>();
            foreach (var stonePosition in gameData.GreenTileBlocksPositions)
            {
                greenTileBlocks.Add(new TileBlock(indexCounter, 0, Instantiate(greenTileBlock), gameBoardGrid[(int)stonePosition.x, (int)stonePosition.y]));
                allBlocksPositions.Add(indexCounter, stonePosition);
                indexCounter++;
            }

            List<MobileBlock> mobileBlocks = null;

            if (gameData.MobileBlocksPositions != null && gameData.MobileBlocksPositions.Count > 0)
            {
                mobileBlocks = new List<MobileBlock>();

                foreach (Vector2 pos in gameData.MobileBlocksPositions)
                {
                    MobileBlock tempBlock = new MobileBlock(indexCounter, Instantiate(mobileBlock), gameBoardGrid[(int)pos.x, (int)pos.y]);
                    mobileBlocks.Add(tempBlock);
                    allBlocksPositions.Add(indexCounter, pos);
                    indexCounter++;
                }
            }

            List<StaticBlock> staticBlocks = null;

            if (gameData.StaticBlocksPositions != null && gameData.StaticBlocksPositions.Count > 0)
            {
                staticBlocks = new List<StaticBlock>();

                foreach (Vector2 pos in gameData.StaticBlocksPositions)
                {
                    StaticBlock tempBlock = new StaticBlock(indexCounter, Instantiate(staticBlock), gameBoardGrid[(int)pos.x, (int)pos.y]);
                    staticBlocks.Add(tempBlock);
                    indexCounter++;
                }
            }

            Stack<GameplayStep> allStepsContainer = new Stack<GameplayStep>();
            allStepsContainer.Push(new GameplayStep(0, EDirection.None, allBlocksPositions));

            currentGame = new TripleGame(gameBoardGrid, gameData.Id, gameDataDynamic.BestSteps, gameDataDynamic.BestCoins, gameData.StepsMinimumCount, gameDataDynamic.GameCount, redTileBlocks, blueTileBlocks, greenTileBlocks, mobileBlocks, staticBlocks, allStepsContainer);
        }

        currentGame.PutBlockObjects();

        currentGame.eventFinalTransform += HandleFinalTransform;
        currentGame.eventBlocksMatch += HandleBlocksMatch;
        currentGame.eventTransitOver += HandleTransitOver;
        currentGame.eventError += HandleError;

        UIManager.Instance.CreateGame(currentGame.MinimumStepsCount, currentGame.BestStepsCount, currentGame.BestCoinsCount);

        OnStartGame();

        ChangeGameplayState(EGameplayState.Gameplay);
    }

    private void DestroyGame()
    {
        currentGame.RemoveBlockObjects();

        currentGame.eventFinalTransform -= HandleFinalTransform;
        currentGame.eventBlocksMatch -= HandleBlocksMatch;
        currentGame.eventTransitOver -= HandleTransitOver;
        currentGame.eventError -= HandleError;

        currentGame = null;
    }

    private void EndGame()
    {
        ChangeGameplayState(EGameplayState.End);
    }

    #region Update methods

    private void UpdateGame()
    {

    }

    private void UpdateTransit()
    {
        currentGame.MoveBlockObjects(Time.deltaTime * blocksMoveSpeed, blocksMinDistance);
    }

    private void UpdatePause()
    {

    }

    #endregion Update methods

    #region Handlers

    private void HandleInputEvent(object sender, InputEventArgs args)
    {
        if (gameplayState == EGameplayState.Transit)
        {
            return;
        }

        switch (args.InputEvent)
        {
            case EInputEvent.Up:
                if (gameplayState == EGameplayState.Gameplay)
                {
                    currentGame.MoveBlocks(EDirection.Up);
                }
                break;

            case EInputEvent.Down:
                if (gameplayState == EGameplayState.Gameplay)
                {
                    currentGame.MoveBlocks(EDirection.Down);
                }
                break;

            case EInputEvent.Left:
                if (gameplayState == EGameplayState.Gameplay)
                {
                    currentGame.MoveBlocks(EDirection.Left);
                }
                break;

            case EInputEvent.Right:
                if (gameplayState == EGameplayState.Gameplay)
                {
                    currentGame.MoveBlocks(EDirection.Right);
                }
                break;

            case EInputEvent.Back:
                if (gameplayState == EGameplayState.Gameplay)
                {
                    currentGame.MoveBlocks(EDirection.None);
                }
                break;

            case EInputEvent.Escape:
                switch (gameplayState)
                {
                    case EGameplayState.Gameplay: currentGame.MoveBlocks(EDirection.None); break;
                    case EGameplayState.Pause: OnStartGame(); ChangeGameplayState(EGameplayState.Gameplay); break;
                    case EGameplayState.End: HandleMainMenuButtonClick(); break;
                }

                break;

            case EInputEvent.Menu:
                HandleMainMenuButtonClick();
                break;

            case EInputEvent.Next:
                HandleNextButtonClick();
                break;

            case EInputEvent.Pause:
                HandlePauseButtonClick();
                break;

            case EInputEvent.Play:
                HandlePlayButtonClick();
                break;

            case EInputEvent.Reload:
                HandleReloadButtonClick();
                break;
            case EInputEvent.Last:
                HandleLoadExitScene();
                break;

        }
    }

    private void HandleFinalTransform(object sender, EventArgs args)
    {
        ChangeGameplayState(EGameplayState.Transit);
    }

    private void HandleTransitOver(object sender, EventArgs args)
    {
        UIManager.Instance.UpdateSteps(currentGame.StepsCount);
        ChangeGameplayState(EGameplayState.Gameplay);
    }

    private void HandleBlocksMatch(object sender, EventArgs args)
    {
        OnWinGame();

        ChangeGameplayState(EGameplayState.Win);

        gameStartData.UpdateDynamicData(currentGame.DynamicData);

        UIManager.Instance.UpdateGameInfo(currentGame.StepsCount, currentGame.BestStepsCount, currentGame.CoinsCount, currentGame.BestCoinsCount);

        Invoke("EndGame", gameEndPauseTime);
        Invoke("OnShowMenu", gameEndPauseTime);
    }

    private void HandleError(object sender, GameErrorEventArgs args)
    {
        UIManager.Instance.UpdateErrorInformation(args.ErrorType);

        ChangeGameplayState(EGameplayState.Error);
    }

    private void HandleMainMenuButtonClick()
    {
        if (sceneLoader != null)
        {
            return;
        }

        sceneLoader = SceneManager.LoadSceneAsync(gameStartData.SceneNames[ESceneName.Menu]);
        sceneLoader.allowSceneActivation = false;

        EndGame();

        InputManager.Instance.Disable();

        EnableLoadedLevel();
    }

    private void HandleNextButtonClick()
    {
        if (sceneLoader != null)
        {
            return;
        }

        if (gameStartData.NextToLoadGame == GameStartData.GamesCount)
        {
            sceneLoader = SceneManager.LoadSceneAsync(gameStartData.SceneNames[ESceneName.Menu]);
            sceneLoader.allowSceneActivation = false;

            EndGame();

            InputManager.Instance.Disable();
        }

        gameStartData.NextToLoadGame = gameStartData.NextToLoadGame + 1;

        CreateGame(gameStartData.NextToLoadGame);
        EnableLoadedLevel();
    }

    private void HandlePauseButtonClick()
    {
        OnPauseGame();
        ChangeGameplayState(EGameplayState.Pause);
    }

    private void HandlePlayButtonClick()
    {
        OnStartGame();
        ChangeGameplayState(EGameplayState.Gameplay);
    }

    private void HandleReloadButtonClick()
    {
        //FirebaseController.Instance.AnalyticsLogEvent(EFirebaseAnalyticsEvent.ReplayGame, LEVEL, GlobalParameters.NextToLoadGame);
        CreateGame(gameStartData.NextToLoadGame);
    }

    private void HandleLoadExitScene()
    {
        if (sceneLoader != null)
        {
            return;
        }

        sceneLoader = SceneManager.LoadSceneAsync(gameStartData.SceneNames[ESceneName.Menu]);
        sceneLoader.allowSceneActivation = false;
        

        EndGame();

        InputManager.Instance.Disable();

        EnableLoadedLevel();
    }

    #endregion

    private void OnStartGame()
    {
        EventHandler<EventArgs> temp = onStartGame;

        if (temp != null)
        {
            temp(this, EventArgs.Empty);
        }
    }

    private void OnPauseGame()
    {
        EventHandler<EventArgs> temp = onPauseGame;

        if (temp != null)
        {
            temp(this, EventArgs.Empty);
        }
    }

    private void OnWinGame()
    {
        EventHandler<EventArgs> temp = onWinGame;

        if (temp != null)
        {
            temp(this, EventArgs.Empty);
        }
    }

    private void OnShowMenu()
    {
        EventHandler<EventArgs> temp = onShowMenu;

        if (temp != null)
        {
            temp(this, EventArgs.Empty);
        }
    }

    private void EnableLoadedLevel()
    {
        sceneLoader.allowSceneActivation = true;
    }

    public GameObject GetGridElementObject(int x, int y)
    {
        return gridBlocks[y].elements[x];
    }
}
