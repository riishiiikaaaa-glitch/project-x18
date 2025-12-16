using System.Collections;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int cardId; // Used to identify matching pairs

    public bool IsFaceUp { get; private set; }
    public bool IsMatched { get; private set; }

    public bool isFlipping = false;

    public float flipDuration = 0.25f;

    public void OnCardClicked()
    {
        if (IsMatched || IsFaceUp || isFlipping)
            return;

        StartCoroutine(FlipUp());
    }

    public IEnumerator FlipUp()
    {
        isFlipping = true;

        float time = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0f, 180f, 0f);

        while (time < flipDuration)
        {
            time += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time / flipDuration);
            yield return null;
        }

        transform.rotation = endRotation;
        IsFaceUp = true;
        isFlipping = false;
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
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time / flipDuration);
            yield return null;
        }

        transform.rotation = endRotation;
        IsFaceUp = false;
        isFlipping = false;
    }

    public void SetMatched()
    {
        IsMatched = true;
    }

    public void HandleClick()
    {
        OnCardClicked();
    }
}