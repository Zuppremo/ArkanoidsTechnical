using UnityEngine;

public class BallVisuals : MonoBehaviour
{
    [SerializeField] private ParticleSystem hitParticles;
    [SerializeField] private ParticleSystem launchParticles;
    private Ball ball;

    private void Awake()
    {
        ball = GetComponent<Ball>();
        ball.BallHit += OnBallHit;
        ball.BallLaunched += OnBallLaunched;
    }

    private void OnDisable()
    {
        ball.BallHit -= OnBallHit;
        ball.BallLaunched -= OnBallLaunched;
    }

    private void OnBallHit(float posY)
    {
        hitParticles.transform.position = new Vector3(hitParticles.transform.position.x, posY, 0);
        hitParticles.Play();
    }

    private void OnBallLaunched()
    {
        launchParticles.Play();
    }
}
