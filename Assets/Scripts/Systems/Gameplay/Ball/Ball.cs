using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ball : MonoBehaviour, IBall, IBallForInput
{
    public event Action BallLaunched;
    public event Action BallCollision;
    public event Action BallLost;
    public event Action BallPowerUp;
    public event Action<float> BallHit;

    [SerializeField] private float baseSpeed = 5F;
    [SerializeField] private float extraSpeedOnCollide = 5F;
    [SerializeField] private float yOffset;

    private Vector3 prePauseVelocity;
    private IGameControllerForState gameController;
    private Vector3 lastVelocity;
    private Rigidbody rb;
    private SphereCollider sphereCollider;
    private readonly List<TimeBasedPowerUp> speedPowerUps = new();

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        sphereCollider = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        transform.position = transform.parent.position + new Vector3(0, yOffset, 0);
    }

    private void Update()
    {
        lastVelocity = rb.velocity;

        foreach (TimeBasedPowerUp powerUp in speedPowerUps)
            powerUp.timeLeft -= Time.deltaTime;

        if (speedPowerUps.Count > 0)
            speedPowerUps.RemoveAll(p => p.timeLeft <= 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.GetComponent<IDamageable>() == null)
            BallCollision?.Invoke();

        float minSpeed = lastVelocity.magnitude;
        float maxSpeed = baseSpeed + speedPowerUps.Sum(p => p.value);

        Vector3 direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
        Vector3 extraVelocity = collision.contacts[0].point.y > transform.position.y ? Vector3.down : Vector3.up;
        BallHit?.Invoke(collision.contacts[0].point.y);

        float contactDirY = (collision.contacts[0].point.y - (transform.position.y - (sphereCollider.radius / 2)));
        if (contactDirY < 0.1F)
        {
            float xDir = transform.position.x - collision.collider.transform.position.x;
            extraVelocity += Vector3.right * xDir * extraSpeedOnCollide;
        }

        rb.velocity = (direction * Mathf.Max(minSpeed, maxSpeed)) + extraVelocity;

        if(rb.velocity.sqrMagnitude > (maxSpeed * maxSpeed))
            rb.velocity = rb.velocity.normalized * maxSpeed;

    }

    public void AddSpeedPowerUp(float speedBonus, float duration)
    {
        if (gameController.GameState != GameState.Gameplay)
            return;
        speedPowerUps.Add(new TimeBasedPowerUp(speedBonus, duration));
        BallPowerUp?.Invoke();
        float maxSpeed = baseSpeed + speedPowerUps.Sum(p => p.value);
        rb.velocity = rb.velocity.normalized * maxSpeed;
    }

    public void OnBallLost()
    {
        BallLost?.Invoke();
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
    }

    public void Launch()
    {
        if (gameController.GameState != GameState.WaitingLaunch)
            return;
        transform.SetParent(null);
        rb.isKinematic = false;
        rb.velocity = Vector3.up * Time.fixedDeltaTime * baseSpeed;
        BallLaunched?.Invoke();
    }

    public void FreezeBall()
    {
        if (rb != null)
        {
            prePauseVelocity = rb.velocity;
            rb.Sleep();
        }
    }

    public void UnfreezeBall()
    {
        if (rb != null)
            rb.velocity = prePauseVelocity;
    }
}
