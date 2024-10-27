using UnityEngine;

public class PowerUpIncreasePaddleSize : PowerUpBase
{
    [SerializeField] private float wantedSizeX;
    [SerializeField] private float duration;
    private IPaddle paddle;

    public override void Awake()
    {
        base.Awake();
        paddle = FindObjectOfType<Paddle>();
    }

    public override void Activate()
    {
        paddle.AddSizePowerUp(wantedSizeX, duration);
    }
}
