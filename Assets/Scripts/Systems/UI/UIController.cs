using System;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject losePanel = default;
    [SerializeField] private GameObject winPanel = default;
    [SerializeField] private GameObject pausePanel = default;

    private GameController gameController;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        gameController.GameLost += OnGameLost;
        gameController.GameWon += OnGameWon;
        gameController.GamePaused += OnGamePaused;
        gameController.GameUnpaused += OnGameUnpaused;
    }

    private void OnDestroy()
    {
        gameController.GamePaused -= OnGamePaused;
        gameController.GameUnpaused -= OnGameUnpaused;
        gameController.GameLost -= OnGameLost;
        gameController.GameWon -= OnGameWon;
    }

    private void OnGamePaused()
    {
        pausePanel.SetActive(true);
    }

    private void OnGameUnpaused()
    {
        pausePanel.SetActive(false);

    }

    private void OnGameLost()
    {
        losePanel.SetActive(true);
    }

    private void OnGameWon()
    {
        winPanel.SetActive(true);
    }
}
