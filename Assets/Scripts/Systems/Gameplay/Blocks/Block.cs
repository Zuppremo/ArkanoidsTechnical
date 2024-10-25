using UnityEngine;

public class Block : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth;
    private int currentHealth;
    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    
    public void ReceiveDamage(int damage)
    {
        if (currentHealth > 0)
            currentHealth -= damage;
        else
            gameObject.SetActive(false);
    }
}
