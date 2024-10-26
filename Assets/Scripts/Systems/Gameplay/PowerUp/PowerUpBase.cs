using UnityEngine;

public abstract class PowerUpBase : MonoBehaviour
{
    private Collider paddleCollider;

    public virtual void Awake()
    {
        paddleCollider = FindObjectOfType<Paddle>().GetComponent<Collider>();
    }

    public abstract void Activate();

    public virtual void OnCollisionEnter(Collision collision)
    {
        if(collision.collider == paddleCollider)
        {
            Activate();
            gameObject.SetActive(false);
        }
    }
}
