using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(BoxCollider2D))]
public class EffectIcon : MonoBehaviour
{
    [SerializeField] public SpriteRenderer _icon;
    [SerializeField] public TMP_Text _stackText;
    [SerializeField] public TMP_Text _abbreviationText;
    private string _description;

    public void Render(Effect effect)
    {
        gameObject.SetActive(true);
        _icon.color = effect.IconColor;
        _stackText.text = effect.GetStackText();
        _abbreviationText.text = effect.GetAbbreviationText();
        _description = effect.GetDescriptionText();
    }

    public void Hide()
    {
        if (gameObject != null)
        {
            gameObject.SetActive(false);
        }
    }

    void OnMouseEnter()
    {
        DisplayEffectTooltip();
    }

    void OnMouseExit()
    {
        HideToolTip();
    }

    public void DisplayEffectTooltip()
    {
        TooltipManager.s_Instance.DisplayTooltip(_description);
    }

    public void HideToolTip()
    {
        TooltipManager.s_Instance.HideTooltip();
    }
}