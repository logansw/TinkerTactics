using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CurrencyManager is a singleton class that manages the player's currency.
/// </summary>
public class CurrencyManager : Singleton<CurrencyManager>
{    
    public Currency RedCurrency { get; private set; }
    public Currency YellowCurrency { get; private set; }
    public Currency BlueCurrency { get; private set; }
    public Currency RedDiscount { get; private set; }
    public Currency YellowDiscount { get; private set; }
    public Currency BlueDiscount { get; private set; }

    void OnEnable()
    {
        EnemyManager.e_OnWaveCleared += AddPassiveCurrency;
    }

    void OnDisable()
    {
        EnemyManager.e_OnWaveCleared -= AddPassiveCurrency;
    }

    protected override void Awake()
    {
        base.Awake();
        RedCurrency = new Currency("Red", 3);
        YellowCurrency = new Currency("Yellow", 3);
        BlueCurrency = new Currency("Blue", 3);
        RedDiscount = new Currency("Red", 0);
        YellowDiscount = new Currency("Yellow", 0);
        BlueDiscount = new Currency("Blue", 0);
    }

    void AddPassiveCurrency()
    {
        Debug.Log("Add Passive Currency");
        RedCurrency.AddAmount(1);
        YellowCurrency.AddAmount(1);
        BlueCurrency.AddAmount(1);
        CurrencyUI.s_Instance.UpdateCurrency();
    }
}
