using System.Collections;
using System.Collections.Generic; // ✅ REQUIRED
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
    }

    void SpawnCards()
    {
        int totalCards = rows * columns;

        // Create paired IDs
        List<int> cardIds = new List<int>();
        for (int i = 0; i < totalCards / 2; i++)
        {
            cardIds.Add(i);
            cardIds.Add(i);
        }

        // Shuffle
        for (int i = 0; i < cardIds.Count; i++)
        {
            int randomIndex = Random.Range(i, cardIds.Count);
            int temp = cardIds[i];
            cardIds[i] = cardIds[randomIndex];
            cardIds[randomIndex] = temp;
        }

        // Spawn cards
        for (int i = 0; i < totalCards; i++)
        {
            Card card = Instantiate(cardPrefab, gridLayout.transform);
            card.cardId = cardIds[i];
        }

        // Register total cards
        matchSystem?.RegisterTotalCards(totalCards);
    }

    public void RestartGrid()
    {
        // Destroy existing cards
        for (int i = gridLayout.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(gridLayout.transform.GetChild(i).gameObject);
        }

        // Rebuild grid
        StartCoroutine(ConfigureGridDelayed());
    }

}
