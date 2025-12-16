using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchSystem : MonoBehaviour
{
    [Header("Settings")]
    public float mismatchDelay = 0.75f;

    private List<Card> openCards = new List<Card>();
    private ScoreManager scoreManager;

    private int totalCards = 0;
    private int matchedCards = 0;
    private bool gameWon = false;

    private void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    // 🔹 CALLED BY CardGridManager
    public void RegisterTotalCards(int count)
    {
        totalCards = count;
        matchedCards = 0;
        gameWon = false;

        Debug.Log($"MatchSystem registered {totalCards} cards");
    }

    public void RegisterFlippedCard(Card card)
    {
        if (card.IsMatched || openCards.Contains(card))
            return;

        openCards.Add(card);

        if (openCards.Count == 2)
            StartCoroutine(CheckMatch());
    }

    private IEnumerator CheckMatch()
    {
        Card a = openCards[0];
        Card b = openCards[1];

        if (a.cardId == b.cardId)
        {
            a.SetMatched();
            b.SetMatched();

            matchedCards += 2;

            scoreManager?.AddMatchScore();
            AudioManager.Instance?.PlayMatch();

            openCards.Clear();
            CheckForWin();
        }
        else
        {
            AudioManager.Instance?.PlayMismatch();

            yield return new WaitForSeconds(mismatchDelay);

            StartCoroutine(a.FlipDown());
            StartCoroutine(b.FlipDown());

            openCards.Clear();
            scoreManager?.ResetCombo();
        }
    }

    private void CheckForWin()
    {
        if (gameWon)
            return;

        if (matchedCards == totalCards)
        {
            gameWon = true;
            AudioManager.Instance?.PlayWin();
            Debug.Log("Game Complete!");
        }
    }

    public void ResetSystem()
    {
        openCards.Clear();
        matchedCards = 0;
        gameWon = false;
    }

}
