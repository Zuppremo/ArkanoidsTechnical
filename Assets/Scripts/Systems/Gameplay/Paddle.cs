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

    public void MoveLeft(float value)
    {
        rb.DOMoveX(transform.position.x - value, delayToMove);
    }

    public void MoveRight(float value)
    {
        rb.DOMoveX(transform.position.x + value, delayToMove);
    }

    public void FreezePaddle()
    {
        rb.Sleep();
    }
}
