using UnityEngine;

public interface IPaddle 
{
    void AddSpeedPowerUp(float speed, float timeLeft);
    void AddSizePowerUp(float sizeX, float duration);
}
