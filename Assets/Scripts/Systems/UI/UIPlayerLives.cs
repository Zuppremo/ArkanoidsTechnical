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
        livesCount++;
        if (livesCount >= playerLives.Count)
            livesCount = playerLives.Count - 1;
        playerLives[livesCount].SetActive(true);
    }
}
 