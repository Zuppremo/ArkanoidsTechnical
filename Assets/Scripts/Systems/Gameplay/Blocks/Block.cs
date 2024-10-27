using System;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, IDamageable
{
    public event Action<Block> BlockDestroyed;
    public event Action<Block> BlockHit;

    [SerializeField] private int maxHealth;
    [SerializeField] private List<PowerUpBase> powerUps = new List<PowerUpBase>();
    [Range(0F, 1F)][SerializeField] private float powerUpChance;

    private int currentHealth;
    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    
    public void ReceiveDamage(int damage)
    {
        BlockHit?.Invoke(this);
        if (currentHealth > 0)
        {
            currentHealth -= damage;
        }
        else
        {
            float randomPosibilityPowerUp = UnityEngine.Random.value;

            if (randomPosibilityPowerUp < powerUpChance)
            {
                int randomGameObject = UnityEngine.Random.Range(0, powerUps.Count);
                Instantiate(powerUps[randomGameObject], transform.position, Quaternion.identity);
            }

            BlockDestroyed?.Invoke(this);
            Invoke(nameof(TurnOffBlock), 1F);
        }
    }

    private void TurnOffBlock()
    {
        gameObject.SetActive(false);
    }
}
