using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour, IGameControllerForState
{
    public event Action OnGameWon;
    public event Action OnGameLost;
    public event Action<int> OnScore;

    [SerializeField] private GameplayInput input;

    private PlayerData playerData;
    private List<IDamageable> blocks = new List<IDamageable>();
    private Ball ball;
    private Paddle paddle;
    private GameState gameState;
    private KillZone killZone;

    public PlayerData PlayerData => playerData;
    public GameState GameState => gameState;

    private void Awake()
    {
        ball = FindObjectOfType<Ball>();
        paddle = FindObjectOfType<Paddle>();
        killZone = FindObjectOfType<KillZone>();
        playerData = new PlayerData(3, 0);
        var gameObjectsInScene = FindObjectsOfType<MonoBehaviour>().OfType<IDamageable>();
        foreach (IDamageable destructable in gameObjectsInScene)
        {
            blocks.Add(destructable);
            destructable.OnBlockDestroyed += HandleBlockDestroyed;
        }

        killZone.OnBallLost += HandleBallLost;
        gameState = GameState.WaitingLaunch;
        input.Initialize(paddle, ball);
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

    private void Update()
    {
        Debug.Log(gameState);
    }

    private void HandleBlockDestroyed(Block block)
    {
        blocks.Remove(block);
        playerData.AddScore(100);
        OnScore?.Invoke(playerData.Score);
        block.OnBlockDestroyed -= HandleBlockDestroyed;
        if (blocks.Count == 0)
        {
            OnGameWon?.Invoke();
            gameState = GameState.GameWin;
        }
    }

    private void HandleBallLost()
    {
        playerData.RemoveLives(1);
        if (playerData.Lives <= 0)
        {
            OnGameLost?.Invoke();
            gameState = GameState.GameLost;
            return;
        }

        ball.OnBallLost();
        ball.transform.SetParent(paddle.transform);
        ball.transform.position = new Vector3(paddle.transform.position.x, paddle.transform.position.y + 0.5F, 0);
        gameState = GameState.WaitingLaunch;
    }

    
}
