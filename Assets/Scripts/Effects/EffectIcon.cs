using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EffectIcon : MonoBehaviour
{
    public SpriteRenderer Icon;
    public TMP_Text StackText;
    public TMP_Text AbbreviationText;

    public void Render(Effect effect)
    {
        gameObject.SetActive(true);
        Icon.color = effect.IconColor;
        StackText.text = effect.GetStackText();
        AbbreviationText.text = effect.GetAbbreviationText();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}