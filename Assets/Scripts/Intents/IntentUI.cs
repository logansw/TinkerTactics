using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntentUI : MonoBehaviour
{
    public SpriteRenderer Icon;
    public TMP_Text ValueText;
    public TMP_Text AbbreviationText;

    public void Render(Intent intent)
    {
        gameObject.SetActive(true);
        Icon.color = intent.IconColor;
        ValueText.text = intent.GetValueText();
        AbbreviationText.text = intent.GetAbbreviationText();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}