using UnityEngine;

public class PowerUpIncreasePaddleSpeed : PowerUpBase
{
    [SerializeField] private float desiredSpeed = 14F;
    [SerializeField] private float powerUpDuration = 2F;

    private IPaddle paddle;

    public override void Awake()
    {
        base.Awake();
        paddle = FindObjectOfType<Paddle>();
    }

    public override void Activate()
    {
        paddle.AddSpeedPowerUp(desiredSpeed, powerUpDuration);
    }
}
