using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Keyboard.current.aKey.isPressed)
            MovePaddle(Vector3.left);
        else if (Keyboard.current.dKey.isPressed)
            MovePaddle(Vector3.right);
    }
    private void MovePaddle(Vector3 direction)
    {
        rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.Impulse);
    }
}
