using UnityEngine;

public class PowerUpPlayerLive : PowerUpBase
{
    [SerializeField] private int livesToAdd = 1;

    private PlayerData playerData;

    public override void Awake()
    {
        base.Awake();
        playerData = FindObjectOfType<GameController>().PlayerData;
    }

    public override void Activate()
    {
        if(playerData != null)
            playerData.AddLives(livesToAdd);
    }
}
