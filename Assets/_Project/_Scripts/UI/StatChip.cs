using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatChip : MonoBehaviour
{
    [SerializeField] private TMP_Text _statTypeText;
    [SerializeField] private TMP_Text _valueText;
    private StatBase _stat;

    public void RegisterStat(StatBase<T> stat)
    {
        stat.e_OnStatChanged += Render;        
    }
    
    public void RegisterStatCeiling(StatBase<U> stat)
    {
        stat.e_OnStatChanged += Render;
    }

    private void Render()
    {

    }
    
    public void Initialize(string statTypeText, int value)
    {
        _statTypeText.text = statTypeText;
        _valueText.text = value.ToString();
    }

    public void Initialize(string statTypeText, float value)
    {
        _statTypeText.text = statTypeText;
        _valueText.text = value.ToString();
    }

    public void Initialize(string statTypeText, string valueText)
    {
        _statTypeText.text = statTypeText;
        _valueText.text = valueText.ToString();
    }
}
