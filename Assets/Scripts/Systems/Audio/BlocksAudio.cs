using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlocksAudio : MonoBehaviour
{
    [SerializeField] private AudioClip blockHitClip;
    [SerializeField] private AudioClip blockDestroyedClip;
    private AudioSource audioSource;
    private List<Block> blocks;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        blocks = FindObjectsOfType<Block>().ToList();

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
    }

    private void OnBlockDestroyed(Block block)
    {
        audioSource.PlayOneShot(blockDestroyedClip);
    }

    private void OnBlockHit(Block block)
    {
        audioSource.PlayOneShot(blockHitClip);
    }
}
