using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] private float delayToMove;
    [SerializeField] private float speed;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void MoveLeft()
    {
        rb.AddForce(Vector3.left * speed * Time.deltaTime, ForceMode.Impulse);
    }

    public void MoveRight()
    {
        rb.AddForce(Vector3.right * speed * Time.deltaTime, ForceMode.Impulse);
    }

    public void FreezePaddle()
    {
        rb.Sleep();
    }

    private void OnCollisionEnter(Collision collision)
    {
    }
}
