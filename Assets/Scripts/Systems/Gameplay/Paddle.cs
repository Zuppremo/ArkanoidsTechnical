using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Paddle : MonoBehaviour, IPaddle, IPaddleForInput
{
    [SerializeField] private float speed;

    private IGameControllerForState gameController;
    private Rigidbody rb;
    private float moveDirection;
    private readonly List<TimeBasedPowerUp> speedPowerUps = new();
    private readonly List<TimeBasedPowerUp> sizePowerUps = new();
    private float paddleSize;
    private float maxPaddleSize = 6F;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gameController = FindObjectOfType<GameController>();
        paddleSize = transform.localScale.x;
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }

    private void Update()
    {
        foreach (TimeBasedPowerUp powerUp in speedPowerUps)
            powerUp.timeLeft -= Time.deltaTime;

        if (speedPowerUps.Count > 0)
            speedPowerUps.RemoveAll(p => p.timeLeft <= 0);

        foreach (TimeBasedPowerUp powerUp in sizePowerUps)
            powerUp.timeLeft -= Time.deltaTime;

        if (sizePowerUps.Count > 0)
        {
            int removeCount = sizePowerUps.RemoveAll(p => p.timeLeft <= 0);
            if (removeCount > 0)
                RefreshScale();
        }
    }

    private void FixedUpdate()
    {
        float maxSpeed = speed + speedPowerUps.Sum(p => p.value);

        if (gameController.GameState is not (GameState.WaitingLaunch or GameState.Gameplay))
            maxSpeed = 0;
        rb.velocity = (Vector3.right * moveDirection) * maxSpeed * Time.fixedDeltaTime;
    }

    public void AddSpeedPowerUp(float speed, float timeLeft)
    {
        if (gameController.GameState != GameState.Gameplay)
            return;
        speedPowerUps.Add(new TimeBasedPowerUp(speed, timeLeft));
    }

    public void AddSizePowerUp(float sizeX, float duration)
    {
        if (gameController.GameState != GameState.Gameplay)
            return;
        sizePowerUps.Add(new TimeBasedPowerUp(sizeX, duration));
        RefreshScale();
    }

    private void RefreshScale()
    {
        float newSize = paddleSize + sizePowerUps.Sum(p => p.value);
        Debug.Log(newSize);
        if (newSize == transform.localScale.x || newSize > maxPaddleSize)
            return;
        transform.DOKill();
        transform.DOScaleX(newSize, 0.25F).SetEase(transform.localScale.x < newSize ? Ease.InElastic : Ease.OutElastic);
    }

    public void SetInput(float value)
    {
        moveDirection = value;
    }
}
