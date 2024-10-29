using System;

public class PlayerData : IPlayerDataPowerUps, IPlayerDataUI
{
    public event Action LifeAdded;
    public event Action LifeRemoved;

    private int lives;
    private int score;
    private int maxLives = 5;

    public int Lives => lives;
    public int Score => score;
    public int MaxLives => lives;

    public PlayerData(int maxLives, int score)
    {
        this.lives = maxLives;
        this.score = score;
    }

    public void AddScore(int amount)
    {
        score += amount;
    }

    public void RemoveScore(int amount)
    {
        score -= amount;
    }

    public void AddLives(int amount)
    {
        lives += amount;
        if (lives >= maxLives)
            lives = maxLives;
        LifeAdded?.Invoke();
    }

    public void RemoveLives(int amount)
    {
        lives -= amount;
        LifeRemoved?.Invoke();
    }
}
