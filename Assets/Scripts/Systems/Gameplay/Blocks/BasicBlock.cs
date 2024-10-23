using UnityEngine;

public class BasicBlock : MonoBehaviour, IHealth, IDestructable
{
    [SerializeField] private int blockHealth;
    public int Health { get => blockHealth; set => blockHealth = value; }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Ball")
        {
            blockHealth--;
        }
        VerifyHealthOnTouch();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void VerifyHealthOnTouch()
    {
        if (blockHealth <= 0)
            Destroy();
    }
}
