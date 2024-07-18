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
    public Currency RedProduction { get; private set; }
    public Currency YellowProduction { get; private set; }
    public Currency BlueProduction { get; private set; }

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
        RedCurrency = new Currency("Red", 7);
        YellowCurrency = new Currency("Yellow", 7);
        BlueCurrency = new Currency("Blue", 7);
        RedProduction = new Currency("Red", 4);
        YellowProduction = new Currency("Yellow", 4);
        BlueProduction = new Currency("Blue", 4);
    }

    void AddPassiveCurrency()
    {
        RedCurrency.AddAmount(RedProduction.Amount);
        YellowCurrency.AddAmount(YellowProduction.Amount);
        BlueCurrency.AddAmount(BlueProduction.Amount);
        CurrencyUI.s_Instance.UpdateCurrency();
    }
}
