using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ball : MonoBehaviour, IBall
{
    [SerializeField] private float baseSpeed = 5F;
    [SerializeField] private float yOffset;

    private Vector3 lastVelocity;
    private Rigidbody rb;
    private readonly List<SpeedPowerUpApplied> speedPowerUps = new();

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        transform.position = transform.parent.position + new Vector3(0, yOffset, 0);
    }

    private void Update()
    {
        lastVelocity = rb.velocity;

        foreach (SpeedPowerUpApplied powerUp in speedPowerUps)
            powerUp.timeLeft -= Time.deltaTime;

        if (speedPowerUps.Count > 0)
            speedPowerUps.RemoveAll(p => p.timeLeft <= 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        float minSpeed = lastVelocity.magnitude;
        float maxSpeed = baseSpeed + speedPowerUps.Sum(p => p.speedBonus);

        Vector3 direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
        Vector3 extraVelocity = collision.contacts[0].point.y > transform.position.y ? Vector3.down : Vector3.up;

        if (collision.rigidbody != null)
            extraVelocity = collision.rigidbody.velocity;

        rb.velocity = (direction * Mathf.Max(minSpeed, maxSpeed)) + extraVelocity;

        if(rb.velocity.sqrMagnitude > (maxSpeed * maxSpeed))
            rb.velocity = rb.velocity.normalized * maxSpeed;
    }

    public void AddPowerUp(float speedBonus, float duration)
    {
        speedPowerUps.Add(new SpeedPowerUpApplied(speedBonus, duration));
        float maxSpeed = baseSpeed + speedPowerUps.Sum(p => p.speedBonus);
        rb.velocity = rb.velocity.normalized * maxSpeed;
    }

    public void SetInitialSetup()
    {
        ChangeKinematicState();
        rb.velocity = Vector3.up * Time.fixedDeltaTime * baseSpeed;
    }

    public bool ChangeKinematicState()
    {
        return (rb.isKinematic) ? rb.isKinematic = false : rb.isKinematic = true;
    }
}
