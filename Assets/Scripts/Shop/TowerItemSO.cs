using UnityEngine;

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
    public int RedDiscount;
    public int YellowDiscount;
    public int BlueDiscount;

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
        CurrencyManager.s_Instance.RedDiscount.AddAmount(RedDiscount);
        CurrencyManager.s_Instance.YellowDiscount.AddAmount(YellowDiscount);
        CurrencyManager.s_Instance.BlueDiscount.AddAmount(BlueDiscount);
    }
}