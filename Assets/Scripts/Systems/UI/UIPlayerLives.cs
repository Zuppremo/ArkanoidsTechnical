using System;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerLives : MonoBehaviour
{
    [SerializeField] private GameObject playerHearth = default;
    [SerializeField] private List<GameObject> playerLives = new List<GameObject>();
    private PlayerData playerData;

    private void Start()
    {
        playerData = FindObjectOfType<GameController>().PlayerData;
        playerData.LifeAdded += OnLifeAdded;
        playerData.LifeRemoved += OnLifeRemoved;
    }

    private void OnDestroy()
    {
        playerData.LifeAdded -= OnLifeAdded;
        playerData.LifeRemoved -= OnLifeRemoved;
    }

    private void OnLifeRemoved()
    {
        playerLives[playerData.Lives].SetActive(false);
    }

    private void OnLifeAdded()
    {
        if (playerData.Lives > playerData.MaxLives)
            return;
        playerLives[playerData.Lives].SetActive(true);
    }
}
 