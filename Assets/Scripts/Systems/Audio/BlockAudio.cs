using UnityEngine;

public class BlockAudio : MonoBehaviour
{
    [SerializeField] private AudioClip blockHitClip;
    [SerializeField] private AudioClip blockDestroyedClip;
    private AudioSource audioSource;
    private Block block;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        block = GetComponent<Block>();
        block.BlockDestroyed += OnBlockDestroyed;
        block.BlockHit += OnBlockHit;
    }

    private void OnDisable()
    {
        block.BlockDestroyed -= OnBlockDestroyed;
        block.BlockHit -= OnBlockHit;
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
