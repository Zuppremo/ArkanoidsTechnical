using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out IDamageable damageable))
            damageable.ReceiveDamage(damage);
    }
}
