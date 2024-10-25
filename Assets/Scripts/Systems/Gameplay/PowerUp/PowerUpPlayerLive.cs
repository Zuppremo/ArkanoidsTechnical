using UnityEngine;

public class PowerUpPlayerLive : PowerUpBase
{
    [SerializeField] private int livesToAdd = 1;

    private PlayerData playerData;

    private void Start()
    {
        playerData = FindObjectOfType<GameController>().PlayerData;
    }

    public override void Activate()
    {
        playerData.AddLives(livesToAdd);
    }
}
