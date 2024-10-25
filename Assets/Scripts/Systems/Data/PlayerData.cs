public class PlayerData
{
    private int lives;
    private int score;
    public PlayerData(int lives, int score)
    {
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
        lives += amount;
    }

    public void RemoveLives(int amount)
    {
        lives -= amount;
    }
}
