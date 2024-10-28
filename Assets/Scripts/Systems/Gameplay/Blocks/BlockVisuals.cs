using System;
using UnityEngine;

public class BlockVisuals : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem explosiveParticles;
    private Block block;

    private void Awake()
    {
        block = GetComponent<Block>();
        block.BlockHit += OnBlockHit;
        block.BlockDestroyed += OnBlockDestroyed;
    }

    private void OnDisable()
    {
        block.BlockHit -= OnBlockHit;
        block.BlockDestroyed -= OnBlockDestroyed;
    }

    private void OnBlockHit(Block block)
    {
        //animator.SetTrigger("hit");
        explosiveParticles.Play(true);
    }

    private void OnBlockDestroyed(Block block)
    {
        animator.SetTrigger("die");
        explosiveParticles.Play(true);
    }
}
