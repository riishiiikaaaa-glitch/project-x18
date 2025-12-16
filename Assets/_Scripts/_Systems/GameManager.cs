using UnityEngine;

public class GameManager : MonoBehaviour
{
    private CardGridManager gridManager;
    private MatchSystem matchSystem;
    private ScoreManager scoreManager;

    private void Awake()
    {
        gridManager = FindObjectOfType<CardGridManager>();
        matchSystem = FindObjectOfType<MatchSystem>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    public void RestartGame()
    {
        // Reset systems
        scoreManager?.ResetAll();
        matchSystem?.ResetSystem();

        // Rebuild board
        gridManager?.RestartGrid();
    }
}
