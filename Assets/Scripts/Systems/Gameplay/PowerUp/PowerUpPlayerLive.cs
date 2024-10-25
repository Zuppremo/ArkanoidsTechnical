using System.Net;
using UnityEngine;

public class PowerUpPlayerLive : PowerUpBase
{
    [SerializeField] private int livesToAdd = 1;

    private PlayerData playerData;
    private void Start()
    {
        playerData = FindObjectOfType<GameController>().PlayerData;
    }

    public void OnCollisionEnter(Collision collision)
    {
        Activate();
        gameObject.SetActive(false);
    }

    public override void Activate()
    {
        playerData.AddLives(livesToAdd);
    }
}
