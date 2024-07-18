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
    public TextMeshProUGUI RedProduction;
    public TextMeshProUGUI YellowProduction;
    public TextMeshProUGUI BlueProduction;

    void Start()
    {
        UpdateCurrency();
    }

    public void UpdateCurrency()
    {
        RedCurrency.text = CurrencyManager.s_Instance.RedCurrency.Amount.ToString();
        YellowCurrency.text = CurrencyManager.s_Instance.YellowCurrency.Amount.ToString();
        BlueCurrency.text = CurrencyManager.s_Instance.BlueCurrency.Amount.ToString();
        RedProduction.text = CurrencyManager.s_Instance.RedProduction.Amount.ToString();
        YellowProduction.text = CurrencyManager.s_Instance.YellowProduction.Amount.ToString();
        BlueProduction.text = CurrencyManager.s_Instance.BlueProduction.Amount.ToString();
    }
}
