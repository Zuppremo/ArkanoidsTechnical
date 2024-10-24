using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public event Action OnBallLost;
    [SerializeField] private float elevationForce = 1.5F;
    [SerializeField] private float ballSpeed = 1.5F;
    [SerializeField] private float ballMaxSpeed = 30F;

    private float sqrMaxVelocity;
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        sqrMaxVelocity = ballMaxSpeed * ballMaxSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        OnBallLost?.Invoke();
    }

    private void FixedUpdate()
    {
        //Debug.Log(rb.velocity);
        var velocity = rb.velocity;

        if (velocity.sqrMagnitude > sqrMaxVelocity)
            rb.velocity = velocity.normalized * ballMaxSpeed;
    }
    public void GiveUpForceToBall()
    {
        rb.AddForce(0, elevationForce * Time.deltaTime, 0, ForceMode.Impulse);
    }

    public void GiveLeftForceToBall()
    {
        rb.AddForce(-elevationForce * Time.deltaTime, elevationForce * Time.deltaTime, 0, ForceMode.Impulse);
    }
    public void GiveRightForceToBall()
    {
        rb.AddForce(elevationForce * Time.deltaTime, elevationForce * Time.deltaTime, 0, ForceMode.Impulse);
    }

    public void FreezeBall()
    {
        rb.Sleep();
    }
}
