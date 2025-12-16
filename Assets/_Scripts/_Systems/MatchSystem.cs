using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchSystem : MonoBehaviour
{
    public float mismatchDelay = 0.75f;

    public Dictionary<int, List<Card>> openCardsById = new Dictionary<int, List<Card>>();
    public List<Card> unmatchedOpenCards = new List<Card>();

    public void RegisterFlippedCard(Card card)
    {
        if (card.IsMatched)
            return;

        unmatchedOpenCards.Add(card);

        if (!openCardsById.ContainsKey(card.cardId))
            openCardsById[card.cardId] = new List<Card>();

        openCardsById[card.cardId].Add(card);

        // If we have a matching pair
        if (openCardsById[card.cardId].Count == 2)
        {
            Card a = openCardsById[card.cardId][0];
            Card b = openCardsById[card.cardId][1];

            a.SetMatched();
            b.SetMatched();

            unmatchedOpenCards.Remove(a);
            unmatchedOpenCards.Remove(b);
        }
        else
        {
            // Check mismatch case
            if (unmatchedOpenCards.Count >= 2)
            {
                Card first = unmatchedOpenCards[0];
                Card second = unmatchedOpenCards[1];

                if (first.cardId != second.cardId)
                {
                    StartCoroutine(FlipBackAfterDelay(first, second));
                    unmatchedOpenCards.Clear();
                }
            }
        }
    }

    IEnumerator FlipBackAfterDelay(Card a, Card b)
    {
        yield return new WaitForSeconds(mismatchDelay);

        if (!a.IsMatched)
            StartCoroutine(a.FlipDown());

        if (!b.IsMatched)
            StartCoroutine(b.FlipDown());
    }
}