using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrencyUI : Singleton<CurrencyUI>
{
    public TextMeshProUGUI YellowCurrency;

    void Start()
    {
        UpdateCurrency();
    }

    public void UpdateCurrency()
    {
        YellowCurrency.text = CurrencyManager.s_Instance.YellowCurrency.Amount.ToString();
    }
}
