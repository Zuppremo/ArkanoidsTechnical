using UnityEngine;

public abstract class PowerUpBase : MonoBehaviour
{
    public abstract void Activate();

    public virtual void OnCollisionEnter(Collision collision)
    {
        Activate();
        gameObject.SetActive(false);
    }
}
