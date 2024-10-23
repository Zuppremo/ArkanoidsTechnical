using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public event Action OnBallLost;
    [SerializeField] private float elevationForce = 1.5F;
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Paddle>() != null)
            GiveRandomForceToBall();
    }
    private void OnTriggerEnter(Collider other)
    {
        OnBallLost?.Invoke();
    }

    public void GiveRandomForceToBall()
    {
        rb.AddForce(UnityEngine.Random.Range(-elevationForce, elevationForce), elevationForce, 0, ForceMode.Impulse);
    }

    public void GiveUpForceToBall()
    {
        rb.AddForce(0, elevationForce, 0, ForceMode.Impulse);
    }

    public void FreezeBall()
    {
        rb.Sleep();
    }
}
