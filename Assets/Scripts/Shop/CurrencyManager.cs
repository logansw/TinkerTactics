using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CurrencyManager is a singleton class that manages the player's currency.
/// </summary>
public class CurrencyManager : Singleton<CurrencyManager>
{    
    public Currency YellowCurrency { get; private set; }

    public override void Initialize()
    {
        base.Initialize();
        YellowCurrency = new Currency("Yellow", 5);
    }

    public void AddPassiveCurrency()
    {
        Debug.Log("Add Passive Currency");
        YellowCurrency.AddAmount(5);
        CurrencyUI.s_Instance.UpdateCurrency();
    }
}