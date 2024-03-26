using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour
{
    public MapPosition Position { get; set; }

    protected SpriteRenderer _spriteRenderer;

    public void Initialize(int x, int y)
    {
        Position = new MapPosition(x, y);
    }

    public void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }
}

public class MapPosition
{
    public int X { get; set; }
    public int Y { get; set; }

    public MapPosition(int x, int y)
    {
        X = x;
        Y = y;
    }
}