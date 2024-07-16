using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrencyUI : Singleton<CurrencyUI>
{
    public TextMeshProUGUI RedCurrency;
    public TextMeshProUGUI YellowCurrency;
    public TextMeshProUGUI BlueCurrency;
    public TextMeshProUGUI RedDiscount;
    public TextMeshProUGUI YellowDiscount;
    public TextMeshProUGUI BlueDiscount;

    void Start()
    {
        UpdateCurrency();
    }

    public void UpdateCurrency()
    {
        RedCurrency.text = CurrencyManager.s_Instance.RedCurrency.Amount.ToString();
        YellowCurrency.text = CurrencyManager.s_Instance.YellowCurrency.Amount.ToString();
        BlueCurrency.text = CurrencyManager.s_Instance.BlueCurrency.Amount.ToString();
        RedDiscount.text = CurrencyManager.s_Instance.RedDiscount.Amount.ToString();
        YellowDiscount.text = CurrencyManager.s_Instance.YellowDiscount.Amount.ToString();
        BlueDiscount.text = CurrencyManager.s_Instance.BlueDiscount.Amount.ToString();
    }
}
