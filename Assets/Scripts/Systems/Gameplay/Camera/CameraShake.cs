using Cinemachine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float ballHitIntensity;
    [SerializeField] private float ballHitTimer;
    [SerializeField] private float ballLostIntensity;
    [SerializeField] private float ballLostTimer;
    [SerializeField] private float blockHitIntensity;
    [SerializeField] private float blockHitTimer;
    [SerializeField] private float blockDestroyedIntensity;
    [SerializeField] private float blockDestroyedTimer;

    private float shakeTimer;
    private CinemachineVirtualCamera cinemachineCamera;
    private Ball ball;
    private List<Block> blocks;
    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;

    private void Awake()
    {

        ball = FindObjectOfType<Ball>();
        blocks = FindObjectOfType<GameController>().Blocks.Cast<Block>().ToList();
        cinemachineCamera = GetComponent<CinemachineVirtualCamera>();
        ball.BallHit += OnBallHit;
        ball.BallLost += OnBallLost;
        cinemachineBasicMultiChannelPerlin = cinemachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        foreach (Block block in blocks)
        {
            block.BlockDestroyed += OnBlockDestroyed;
            block.BlockHit += OnBlockHit;
        }
    }

    private void OnDisable()
    {
        foreach (Block block in blocks)
        {
            block.BlockDestroyed -= OnBlockDestroyed;
            block.BlockHit -= OnBlockHit;
        }

        ball.BallHit -= OnBallHit;
        ball.BallLost -= OnBallLost;
    }


    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0F)
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0F;
        }
    }

    private void OnBlockHit(Block block)
    {
        ShakeCamera(blockHitIntensity, blockHitTimer);
    }

    private void OnBlockDestroyed(Block block)
    {
        ShakeCamera(blockDestroyedIntensity, blockDestroyedTimer);
    }

    private void OnBallLost()
    {
        ShakeCamera(ballLostIntensity, ballLostTimer);
    }

    private void OnBallHit(float obj)
    {
        ShakeCamera(ballHitIntensity, ballHitTimer);
    }

    private void ShakeCamera(float shakeIntensity, float time)
    {
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = shakeIntensity;
        shakeTimer = time;
    }
}
