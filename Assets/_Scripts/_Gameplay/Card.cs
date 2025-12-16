using System.Collections;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    [Header("Card Data")]
    public int cardId;

    [Header("Flip Settings")]
    public float flipDuration = 0.25f;

    [Header("Systems")]
    private MatchSystem matchSystem;

    [Header("UI")]
    public TMP_Text idText;

    public bool IsFaceUp { get; private set; }
    public bool IsMatched { get; private set; }

    private bool isFlipping = false;

    private void Start()
    {
        matchSystem = FindObjectOfType<MatchSystem>();

        // Debug visualization: show ID only when card is face up
        if (idText != null)
        {
            idText.text = cardId.ToString();
            idText.gameObject.SetActive(false);
        }
    }

    public void HandleClick()
    {
        if (IsMatched || IsFaceUp || isFlipping)
            return;

        StartCoroutine(FlipUp());
    }

    private IEnumerator FlipUp()
    {
        isFlipping = true;

        float time = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0f, 180f, 0f);

        while (time < flipDuration)
        {
            time += Time.deltaTime;
            transform.rotation =
                Quaternion.Slerp(startRotation, endRotation, time / flipDuration);
            yield return null;
        }

        transform.rotation = endRotation;
        IsFaceUp = true;
        isFlipping = false;

        if (idText != null)
            idText.gameObject.SetActive(true);

        if (matchSystem != null)
        {
            matchSystem.RegisterFlippedCard(this);
        }
    }

    public IEnumerator FlipDown()
    {
        isFlipping = true;

        float time = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.identity;

        while (time < flipDuration)
        {
            time += Time.deltaTime;
            transform.rotation =
                Quaternion.Slerp(startRotation, endRotation, time / flipDuration);
            yield return null;
        }

        transform.rotation = endRotation;
        IsFaceUp = false;
        isFlipping = false;

        if (idText != null)
            idText.gameObject.SetActive(false);
    }

    public void SetMatched()
    {
        IsMatched = true;

        // Optional: keep ID visible when matched
        if (idText != null)
            idText.gameObject.SetActive(true);
    }
}
