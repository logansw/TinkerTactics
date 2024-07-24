using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CurrencyManager is a singleton class that manages the player's currency.
/// </summary>
public class CurrencyManager : Singleton<CurrencyManager>
{    
    public Currency YellowCurrency { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        YellowCurrency = new Currency("Yellow", 15);
    }

    public void AddPassiveCurrency()
    {
        Debug.Log("Add Passive Currency");
        YellowCurrency.AddAmount(5 + Mathf.Min(5, YellowCurrency.Amount / 10));
        CurrencyUI.s_Instance.UpdateCurrency();
    }
}