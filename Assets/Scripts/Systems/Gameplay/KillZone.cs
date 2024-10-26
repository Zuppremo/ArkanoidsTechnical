using System;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    public event Action OnBallLost;

    private Collider ballCollider;

    private void Awake()
    {
        ballCollider = FindObjectOfType<Ball>().GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider == ballCollider)
        {
            OnBallLost?.Invoke();
        }
    }
}
