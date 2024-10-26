using UnityEngine;

public interface IPaddle 
{
    void AddPowerUp(float speed, float timeLeft);
    void ChangeSize(float sizeX, float duration);
}
