using System;

public class PlayerData : IPlayerDataPowerUps, IPlayerDataUI
{
    public event Action LifeAdded;
    public event Action LifeRemoved;

    private int lives;
    private int score;
    private int maxLives;

    public int Lives => lives;
    public int Score => score;
    public int MaxLives => lives;

    public PlayerData(int maxLives, int lives, int score)
    {
        this.maxLives = maxLives;
        this.lives = lives;
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
        if (lives + amount >= maxLives)
            lives = maxLives;
        else
            lives += amount;
        LifeAdded?.Invoke();
    }

    public void RemoveLives(int amount)
    {
        lives -= amount;
        LifeRemoved?.Invoke();
    }
}
