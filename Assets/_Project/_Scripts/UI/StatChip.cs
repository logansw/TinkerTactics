using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatChip : MonoBehaviour
{
    [SerializeField] private TMP_Text _statTypeText;
    [SerializeField] private TMP_Text _valueText;
    private Stat _stat;
    private bool _showMax;

    private void Render()
    {
        if (_showMax)
        {
            _valueText.text = $"{_stat.Current}/{_stat.Max}";
        }
        else
        {
            _valueText.text = _stat.Current.ToString();
        }
    }
    
    public void Initialize(Stat stat, bool showMax, string abbreviation)
    {
       _stat = stat;
       _showMax = showMax;
       _statTypeText.text = abbreviation;
       Render();
       _stat.e_OnStatChanged += Render;
    }
}