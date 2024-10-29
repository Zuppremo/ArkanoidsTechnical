using UnityEngine;
using TMPro;

public class PlayerScoreText : MonoBehaviour
{
    [SerializeField] TMP_Text scoreTextGameplay;
    [SerializeField] TMP_Text scoreTextEndGame;
    private GameController gameController;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        gameController.OnScore += HandleScoreOnUI;
    }

    private void HandleScoreOnUI(int score)
    {
        scoreTextGameplay.text = $"Score: {score}";
        scoreTextEndGame.text = $"Total Score: {score}";
    }

    public void OnDestroy()
    {
        gameController.OnScore -= HandleScoreOnUI;
    }
}
