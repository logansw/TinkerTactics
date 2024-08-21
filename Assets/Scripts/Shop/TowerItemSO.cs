using UnityEngine;
using System;

[CreateAssetMenu(fileName = "NewTowerItem", menuName = "Tower Item")]
public class TowerItemSO : ScriptableObject
{
    public string Name;
    public string Description;
    public int YellowCost;
    public Tower TowerPrefab;
    
    public bool CanAfford()
    {
        return CurrencyManager.s_Instance.YellowCurrency.Amount >= YellowCost;
    }

    public void Purchase()
    {
        CurrencyManager.s_Instance.YellowCurrency.SubtractAmount(YellowCost);
        CurrencyUI.s_Instance.UpdateCurrency();
        Instantiate(TowerPrefab);
    }
}