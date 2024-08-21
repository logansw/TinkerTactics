using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TooltipManager : Singleton<TooltipManager>
{
    [SerializeField] private TMP_Text _tooltipText;

    public void DisplayTooltip(string text)
    {
        _tooltipText.text = text;
        _tooltipText.transform.parent.gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        _tooltipText.transform.parent.gameObject.SetActive(false);
    }
}