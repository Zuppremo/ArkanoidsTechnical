using UnityEngine;
public class Ball : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 5F;
    private Vector3 lastVelocity;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        float minSpeed = lastVelocity.magnitude;
        Vector3 direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
        Vector3 extraVelocity = Vector3.zero;
        if (collision.rigidbody != null)
            extraVelocity = collision.rigidbody.velocity;
        rb.velocity = (direction * Mathf.Max(minSpeed, maxSpeed)) + extraVelocity;

        if(rb.velocity.sqrMagnitude > (maxSpeed * maxSpeed))
            rb.velocity = rb.velocity.normalized * maxSpeed;
    }

    public void FreezeBall()
    {
        rb.Sleep();
    }
}
