using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public event Action OnGameWon;
    public event Action OnGameLost;
    
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
        killZone = FindObjectOfType<KillZone>();

        playerData = new PlayerData(3, 0);
        Debug.Log(playerData.Lives);
        var gameObjectsInScene = FindObjectsOfType<MonoBehaviour>().OfType<IDamageable>();
        foreach (IDamageable destructable in gameObjectsInScene)
        {
            blocks.Add(destructable);
            destructable.OnBlockDestroyed += HandleBlockDestroyed;
        }

        killZone.OnBallLost += HandleBallLost;
        gameState = GameState.WaitingLaunch;
    }

    private void Start()
    {
        ball = FindObjectOfType<Ball>();
        paddle = FindObjectOfType<Paddle>();
    }

    private void Update()
    {
        Debug.Log(gameState);
    }

    private void HandleBlockDestroyed(Block block)
    {
        blocks.Remove(block);
        playerData.AddScore(100);
        block.OnBlockDestroyed -= HandleBlockDestroyed;
        if (blocks.Count == 0)
        {
            Debug.Log("Game Won");
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
        }

        ball.transform.SetParent(paddle.transform);
        ball.ChangeKinematicState();
        ball.transform.position = new Vector3(paddle.transform.position.x, paddle.transform.position.y + 0.5F, 0);

        gameState = GameState.WaitingLaunch;
    }

    private void OnDisable()
    {
        killZone.OnBallLost -= HandleBallLost;
    }
}
