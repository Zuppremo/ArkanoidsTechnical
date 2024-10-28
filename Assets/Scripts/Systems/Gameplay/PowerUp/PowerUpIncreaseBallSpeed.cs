using UnityEngine;

public class PowerUpIncreaseBallSpeed : PowerUpBase
{
    [SerializeField] private float desiredSpeed = 14F;
    [SerializeField] private float powerUpDuration = 2F;

    private IBall ball;

    public override void Awake()
    {
        base.Awake();
        ball = FindObjectOfType<Ball>();
    }

    public override void Activate()
    {
        ball.AddSpeedPowerUp(desiredSpeed, powerUpDuration);
    }
}
