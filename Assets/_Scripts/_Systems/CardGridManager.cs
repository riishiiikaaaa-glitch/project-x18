using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardGridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    public int rows = 4;
    public int columns = 4;

    [Header("References")]
    public GridLayoutGroup gridLayout;
    public RectTransform boardRect;

    public void Start()
    {
        StartCoroutine(ConfigureGridDelayed());
    }

    IEnumerator ConfigureGridDelayed()
    {
        // Wait for UI layout to calculate
        yield return null;

        ConfigureGrid();
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
}
