using System;

public interface IDamageable
{
    event Action<Block> OnBlockDestroyed;
    event Action<Block> OnBlockHit;
    int MaxHealth { get; }
    int CurrentHealth { get; }
    void ReceiveDamage(int damage);
}
