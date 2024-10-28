using UnityEngine;
using TMPro;

public class PlayerScoreText : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    private GameController gameController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        gameController.OnScore += HandleScoreOnUI;
    }

    private void HandleScoreOnUI(int score)
    {
        scoreText.text = $"Score: {score}";
    }

    public void OnDisable()
    {
        gameController.OnScore -= HandleScoreOnUI;
    }
}
