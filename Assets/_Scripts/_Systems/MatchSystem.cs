using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchSystem : MonoBehaviour
{
    [Header("Settings")]
    public float mismatchDelay = 0.75f;

    private List<Card> openCards = new List<Card>();
    private ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    public void RegisterFlippedCard(Card card)
    {
        if (card.IsMatched || openCards.Contains(card))
            return;

        openCards.Add(card);

        if (openCards.Count == 2)
        {
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        Card a = openCards[0];
        Card b = openCards[1];

        if (a.cardId == b.cardId)
        {
            a.SetMatched();
            b.SetMatched();

            if (scoreManager != null)
                scoreManager.AddMatchScore();

            openCards.Clear();

            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayMatch();
            }

        }
        else
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayMismatch();
            }

            yield return new WaitForSeconds(mismatchDelay);

            if (!a.IsMatched)
                StartCoroutine(a.FlipDown());

            if (!b.IsMatched)
                StartCoroutine(b.FlipDown());

            openCards.Clear();

            if (scoreManager != null)
                scoreManager.ResetCombo();
        }
    }
}
