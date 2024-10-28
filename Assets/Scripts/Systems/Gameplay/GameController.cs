using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;

public class GameController : MonoBehaviour, IGameControllerForState
{
    public event Action GamePaused;
    public event Action GameUnpaused;
    public event Action GameWon;
    public event Action GameLost;
    public event Action<int> OnScore;

    [SerializeField] private GameplayInput input;
    [SerializeField] private int maxPlayerLives = 5;

    private PlayerData playerData;
    private List<IDamageable> blocks = new List<IDamageable>();
    private Ball ball;
    private Paddle paddle;
    private GameState gameState;
    private KillZone killZone;

    public PlayerData PlayerData => playerData;
    public GameState GameState => gameState;
    public List<IDamageable> Blocks => blocks;

    private void Awake()
    {
        ball = FindObjectOfType<Ball>();
        paddle = FindObjectOfType<Paddle>();
        killZone = FindObjectOfType<KillZone>();
        playerData = new PlayerData(maxPlayerLives, maxPlayerLives, 0);
        var gameObjectsInScene = FindObjectsOfType<MonoBehaviour>().OfType<IDamageable>();
        foreach (IDamageable destructable in gameObjectsInScene)
        {
            blocks.Add(destructable);
            destructable.BlockDestroyed += HandleBlockDestroyed;
        }
        killZone.OnBallLost += HandleBallLost;
        gameState = GameState.WaitingLaunch;
        input.Initialize(paddle, ball, this);
        ball.BallLaunched += OnBallLaunched;
    }


    private void OnDestroy()
    {
        killZone.OnBallLost -= HandleBallLost;
        ball.BallLaunched -= OnBallLaunched;
    }

    private void OnBallLaunched()
    {
        if (gameState is GameState.WaitingLaunch)
            gameState = GameState.Gameplay;
    }

    private void HandleBlockDestroyed(Block block)
    {
        blocks.Remove(block);
        playerData.AddScore(100);
        OnScore?.Invoke(playerData.Score);
        if (blocks.Count == 0)
        {
            GameWon?.Invoke();
            FreezeObjects();
            gameState = GameState.GameWin;
        }
        block.BlockDestroyed -= HandleBlockDestroyed;
    }

    private void HandleBallLost()
    {
        playerData.RemoveLives(1);
        if (playerData.Lives <= 0)
        {
            GameLost?.Invoke();
            FreezeObjects();
            gameState = GameState.GameLost;
            return;
        }

        ball.OnBallLost();
        ball.transform.SetParent(paddle.transform);
        ball.transform.position = new Vector3(paddle.transform.position.x, paddle.transform.position.y + 0.5F, 0);
        gameState = GameState.WaitingLaunch;
    }

    public void SetPause()
    {
        if (gameState == GameState.GamePaused)
        {
            GameUnpaused?.Invoke();
            gameState = GameState.Gameplay;
            ball.UnfreezeBall();
        }
        else
        {
            if (gameState == GameState.WaitingLaunch)
                return;
            GamePaused?.Invoke();
            gameState = GameState.GamePaused;
            FreezeObjects();
        }
    }

    private void FreezeObjects()
    {
        ball.FreezeBall();
        paddle.FreezePaddle();
    }
}
