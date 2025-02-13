using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class IconTooltip : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private List<StatChip> _statChips;

    public void Initialize(ITooltipTargetable tooltipSource)
    {
        _image.sprite = tooltipSource.GetIcon();
        _name.text = tooltipSource.GetTooltipName();
        _description.text = tooltipSource.GetTooltipDescription();
    }
}
