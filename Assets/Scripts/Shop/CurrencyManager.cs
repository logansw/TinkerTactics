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

    protected override void Awake()
    {
        base.Awake();
        RedCurrency = new Currency("Red", 10);
        YellowCurrency = new Currency("Yellow", 10);
        BlueCurrency = new Currency("Blue", 10);
        RedDiscount = new Currency("Red", 0);
        YellowDiscount = new Currency("Yellow", 0);
        BlueDiscount = new Currency("Blue", 0);
    }
}
