using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Game Complete UI")]
    public GameObject gameCompletePanel;
    public TMP_Text finalScoreText;

    private void Start()
    {
        gameCompletePanel.SetActive(false);
    }

    public void ShowGameComplete(int finalScore)
    {
        finalScoreText.text = $"Final Score: {finalScore}";
        gameCompletePanel.SetActive(true);
    }

    public void HideGameComplete()
    {
        gameCompletePanel.SetActive(false);
    }
}
