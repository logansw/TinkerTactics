using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntentUI : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _background;
    [SerializeField] private SpriteRenderer _icon;
    [SerializeField] private TMP_Text _valueText;
    [SerializeField] private TMP_Text _abbreviationText;

    void Start()
    {
        SetRenderersActive(false);
    }

    public void Render(Intent intent)
    {
        SetRenderersActive(true);
        _icon.color = intent.IconColor;
        _valueText.text = intent.GetValueText();
        _abbreviationText.text = intent.GetAbbreviationText();
    }

    public void SetRenderersActive(bool active)
    {
        _icon.gameObject.SetActive(active);
        _valueText.gameObject.SetActive(active);
        _abbreviationText.gameObject.SetActive(active);
        _background.gameObject.SetActive(active);
    }
}