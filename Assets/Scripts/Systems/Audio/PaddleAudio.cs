using UnityEngine;

public class PaddleAudio : MonoBehaviour
{
    [SerializeField] private AudioClip powerUpStartClip;
    [SerializeField] private AudioClip powerUpEndClip;
    private AudioSource audioSource;
    private Paddle paddle;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        paddle = GetComponent<Paddle>();
        paddle.PowerUpAdd += OnPowerUpAdded;
    }

    private void OnDisable()
    {
        paddle.PowerUpAdd -= OnPowerUpAdded;
    }

    private void OnPowerUpAdded()
    {
        audioSource.PlayOneShot(powerUpStartClip);
    }
}
