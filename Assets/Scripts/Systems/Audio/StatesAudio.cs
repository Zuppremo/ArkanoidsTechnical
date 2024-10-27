using System;
using UnityEngine;

public class StatesAudio : MonoBehaviour
{
    [SerializeField] private AudioClip gameWonClip;
    [SerializeField] private AudioClip gameLostClip;
    [SerializeField] private AudioClip gamePausedClip;
    private AudioSource audioSource;
    private GameController gameController;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        gameController = GetComponent<GameController>();
        gameController.GameWon += OnGameWon;
        gameController.GameLost += OnGameLost;
        gameController.GamePaused += OnGamePaused;
        gameController.GameUnpaused += OnGameUnpaused;
    }

    private void OnDestroy()
    {
        gameController.GameWon += OnGameWon;
        gameController.GameLost += OnGameLost;
        gameController.GamePaused += OnGamePaused;
        gameController.GameUnpaused += OnGameUnpaused;
    }

    private void OnGameUnpaused()
    {
        audioSource.PlayOneShot(gamePausedClip);
    }

    private void OnGamePaused()
    {
        audioSource.PlayOneShot(gamePausedClip);
    }

    private void OnGameLost()
    {
        audioSource.PlayOneShot(gameLostClip);
    }

    private void OnGameWon()
    {
        audioSource.PlayOneShot(gameWonClip);
    }
}
