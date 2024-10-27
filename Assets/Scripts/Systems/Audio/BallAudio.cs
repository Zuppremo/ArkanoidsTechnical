using System;
using System.Collections.Generic;
using UnityEngine;

public class BallAudio : MonoBehaviour
{
    [SerializeField] private AudioClip ballLaunchedClip;
    [SerializeField] private AudioClip ballCollisionClip;
    [SerializeField] private AudioClip ballLostClip;
    private AudioSource audioSource;
    private Ball ball;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        ball = FindObjectOfType<Ball>();
        ball.BallLaunched += OnBallLaunched;
        ball.BallCollision += OnBallCollision;
        ball.BallLost += OnBallLost;
    }

    private void OnDestroy()
    {
        ball.BallLaunched -= OnBallLaunched;
        ball.BallCollision -= OnBallCollision;
        ball.BallLost -= OnBallLost;
    }

    private void OnBallLaunched()
    {
        audioSource.PlayOneShot(ballLaunchedClip);
    }

    private void OnBallCollision()
    {
        audioSource.PlayOneShot(ballCollisionClip);
    }

    private void OnBallLost()
    {
        audioSource.PlayOneShot(ballLostClip);
    }
}
