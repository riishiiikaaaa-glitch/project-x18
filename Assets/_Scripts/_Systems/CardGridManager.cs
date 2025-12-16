using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardGridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    public int rows = 4;
    public int columns = 4;

    [Header("Card Prefab")]
    public Card cardPrefab;

    [Header("References")]
    public GridLayoutGroup gridLayout;
    public RectTransform boardRect;

    private MatchSystem matchSystem;

    private void Start()
    {
        matchSystem = FindObjectOfType<MatchSystem>();
        StartCoroutine(ConfigureGridDelayed());
    }

    IEnumerator ConfigureGridDelayed()
    {
        // Wait one frame for UI layout
        yield return null;

        ConfigureGrid();
        SpawnCards();
    }

    void ConfigureGrid()
    {
        if (gridLayout == null || boardRect == null)
        {
            Debug.LogWarning("GridLayout or BoardRect not assigned");
            return;
        }

        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = columns;

        float boardWidth = boardRect.rect.width;
        float boardHeight = boardRect.rect.height;

        float cellWidth = boardWidth / columns;
        float cellHeight = boardHeight / rows;

        float size = Mathf.Min(cellWidth, cellHeight);
        gridLayout.cellSize = new Vector2(size, size);

        Debug.Log($"Grid configured: {rows}x{columns}, cell size {size}");
    }

    void SpawnCards()
    {
        int totalCards = rows * columns;

        for (int i = 0; i < totalCards; i++)
        {
            Card card = Instantiate(cardPrefab, gridLayout.transform);
            card.cardId = i / 2; // pairing logic
        }

        // ✅ THIS IS THE FIX
        if (matchSystem != null)
        {
            matchSystem.RegisterTotalCards(totalCards);
        }
        else
        {
            Debug.LogError("MatchSystem not found!");
        }
    }
}
