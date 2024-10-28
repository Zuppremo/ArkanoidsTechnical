using System;

public interface IDamageable
{
    event Action<Block> BlockDestroyed;
    event Action<Block> BlockHit;
    int MaxHealth { get; }
    int CurrentHealth { get; }
    void ReceiveDamage(int damage);
}
