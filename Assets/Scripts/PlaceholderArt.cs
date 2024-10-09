using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// This script is used to store placeholder art for the game. Attach this component to any GameObject that needs placeholder art.
public class PlaceholderArt : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color _color;
    [SerializeField] private string _name;
    [SerializeField] private TMP_Text _textLabel;

    void OnValidate() {
        if (_textLabel != null) {
            _textLabel.text = _name;
        }
        _spriteRenderer.color = _color;
    }

    public void SetColor(Color color)
    {
        _color = color;
        _spriteRenderer.color = color;
    }
}
