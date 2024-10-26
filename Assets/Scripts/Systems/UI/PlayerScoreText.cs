using UnityEngine;
using TMPro;
public class PlayerScoreText : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    IPlayerDataUI playerData;

    private void Start()
    {
        playerData = FindObjectOfType<GameController>().PlayerData;
    }

    void Update()
    {
        scoreText.text = playerData.Score.ToString();
    }
}
