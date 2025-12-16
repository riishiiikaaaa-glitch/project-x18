using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text scoreText;

    [Header("Scoring")]
    public int baseMatchScore = 100;

    private int score = 0;
    private int combo = 0;

    private void Start()
    {
        UpdateUI();
    }

    public void AddMatchScore()
    {
        combo++;
        score += baseMatchScore * combo;
        UpdateUI();
    }

    public void ResetCombo()
    {
        combo = 0;
    }

    private void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";
        }
    }

    public void ResetAll()
    {
        score = 0;
        combo = 0;
        UpdateUI();
    }

}
