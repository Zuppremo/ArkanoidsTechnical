public interface IDamageable
{
    int MaxHealth { get; }
    int CurrentHealth { get; }
    void ReceiveDamage(int damage);
}
