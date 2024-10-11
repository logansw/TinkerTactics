using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPreview : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;

    public void Initialize(Sprite previewSprite)
    {
        SpriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        SpriteRenderer.sprite = previewSprite;
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, 0);
    }
}