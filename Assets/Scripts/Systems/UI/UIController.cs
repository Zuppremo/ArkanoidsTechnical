using System;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject losePanel = default;
    [SerializeField] private GameObject winPanel = default;

    private GameController gameController;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        gameController.OnGameLost += HandleGameLost;
        gameController.OnGameWon += HandleGameWon;
    }

    private void HandleGameLost()
    {
        losePanel.SetActive(true);
        gameController.OnGameLost -= HandleGameLost;
    }

    private void HandleGameWon()
    {
        winPanel.SetActive(true);
        gameController.OnGameWon -= HandleGameWon;
    }

}
