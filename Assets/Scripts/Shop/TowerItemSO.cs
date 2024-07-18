using UnityEngine;
using System;

[CreateAssetMenu(fileName = "NewTowerItem", menuName = "Tower Item")]
public class TowerItemSO : ScriptableObject
{
    public string Name;
    public string Description;
    public int Tier;
    public int RedCost;
    public int YellowCost;
    public int BlueCost;
    public Tower TowerPrefab;
    public int RedProduction;
    public int YellowProduction;
    public int BlueProduction;

    public bool CanAfford()
    {
        return CurrencyManager.s_Instance.RedCurrency.Amount >= RedCost &&
               CurrencyManager.s_Instance.YellowCurrency.Amount >= YellowCost &&
               CurrencyManager.s_Instance.BlueCurrency.Amount >= BlueCost;
    }

    public void Purchase()
    {
        CurrencyManager.s_Instance.RedCurrency.SubtractAmount(RedCost);
        CurrencyManager.s_Instance.YellowCurrency.SubtractAmount(YellowCost);
        CurrencyManager.s_Instance.BlueCurrency.SubtractAmount(BlueCost);
        CurrencyManager.s_Instance.RedProduction.AddAmount(RedProduction);
        CurrencyManager.s_Instance.YellowProduction.AddAmount(YellowProduction);
        CurrencyManager.s_Instance.BlueProduction.AddAmount(BlueProduction);
        CurrencyUI.s_Instance.UpdateCurrency();
        Instantiate(TowerPrefab);
    }
}