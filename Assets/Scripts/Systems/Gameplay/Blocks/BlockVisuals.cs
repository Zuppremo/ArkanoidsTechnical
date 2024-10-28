using UnityEngine;

public class BlockVisuals : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem explosiveParticles;
    private Block block;

    private void Awake()
    {
        block = GetComponent<Block>();
        block.BlockDestroyed += OnBlockDestroyed;
    }

    private void OnDisable()
    {
        block.BlockDestroyed -= OnBlockDestroyed;
    }

    private void OnBlockDestroyed(Block block)
    {
        animator.SetTrigger("die");
        explosiveParticles.Play(true);
    }
}
