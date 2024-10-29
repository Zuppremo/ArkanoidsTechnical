using System;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerLives : MonoBehaviour
{
    [SerializeField] private List<GameObject> playerLives = new List<GameObject>();
    private PlayerData playerData;
    private int livesCount;

    private void Start()
    {
        playerData = FindObjectOfType<GameController>().PlayerData;
        playerData.LifeAdded += OnLifeAdded;
        playerData.LifeRemoved += OnLifeRemoved;
        livesCount = playerData.MaxLives;
    }

    private void OnDestroy()
    {
        playerData.LifeAdded -= OnLifeAdded;
        playerData.LifeRemoved -= OnLifeRemoved;
    }

    private void OnLifeRemoved()
    {
        livesCount--;
        playerLives[livesCount].SetActive(false);
    }

    private void OnLifeAdded()
    {
        if (playerData.Lives >= playerData.MaxLives)
            return;
        livesCount++;
        playerLives[livesCount].SetActive(true);
    }
}
 