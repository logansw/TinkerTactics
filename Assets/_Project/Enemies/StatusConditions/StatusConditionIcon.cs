using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(BoxCollider2D))]
public class StatusConditionIcon : MonoBehaviour
{
    [SerializeField] public SpriteRenderer _icon;
    [SerializeField] public TMP_Text _stackText;
    [SerializeField] public TMP_Text _abbreviationText;
    private string _description;

    public void Render(StatusCondition statusCondition)
    {
        gameObject.SetActive(true);
        _icon.color = statusCondition.IconColor;
        _stackText.text = statusCondition.GetStackText();
        _abbreviationText.text = statusCondition.GetAbbreviationText();
        _description = statusCondition.GetDescriptionText();
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
        DisplayStatusConditionTooltip();
    }

    void OnMouseExit()
    {
        HideToolTip();
    }

    public void DisplayStatusConditionTooltip()
    {
        TooltipManager.s_Instance.DisplayTooltip(_description);
    }

    public void HideToolTip()
    {
        TooltipManager.s_Instance.HideTooltip();
    }
}