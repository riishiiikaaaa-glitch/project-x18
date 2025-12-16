using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIGradient : MonoBehaviour
{
    public Color topColor = Color.white;
    public Color bottomColor = Color.black;

    private Image image;

    void Awake()
    {
        image = GetComponent<Image>();
        ApplyGradient();
    }

    void OnValidate()
    {
        if (image == null)
            image = GetComponent<Image>();

        ApplyGradient();
    }

    void ApplyGradient()
    {
        Texture2D texture = new Texture2D(1, 2);
        texture.wrapMode = TextureWrapMode.Clamp;

        texture.SetPixel(0, 0, bottomColor);
        texture.SetPixel(0, 1, topColor);
        texture.Apply();

        Sprite sprite = Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f)
        );

        image.sprite = sprite;
        image.type = Image.Type.Sliced;
    }
}