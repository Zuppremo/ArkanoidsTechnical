using UnityEngine;
using TMPro;

public class UITutorialText : MonoBehaviour
{
    [SerializeField] private TMP_Text tutorialText;

    private void Start()
    {
        Invoke(nameof(TurnOffTutorialText), 5F);
    }

    private void TurnOffTutorialText()
    {
        tutorialText.text = "";
    }
}
