using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : Singleton<Player>
{
    [SerializeField]
    private TMP_Text _healthText;
    public Health Health;

    [SerializeField]
    private TMP_Text _goldText;
    private int _gold;
    public int Gold
    {
        get
        {
            return _gold;
        }
        set
        {
            _gold = value;
            _gold = Mathf.Clamp(_gold, 0, int.MaxValue);
            _goldText.text = $"Gold: {_gold}";
        }
    }

    [SerializeField]
    private TMP_Text _bitsText;
    private int _bits;
    public int Bits
    {
        get
        {
            return _bits;
        }
        set
        {
            _bits = value;
            _bits = Mathf.Clamp(_bits, 0, int.MaxValue);
            _bitsText.text = $"Bits: {_bits}";
        }
    }

    void Start()
    {
        Health = new Health(50);
        Gold = 0;
        Bits = 0;
        UpdateHealthText(Health.CurrentHealth, Health.MaxHealth);
        Health.e_OnHealthChanged += UpdateHealthText;
    }

    private void UpdateHealthText(float currentHealth, float maxHealth)
    {
        _healthText.text = $"Player Health: {currentHealth}/{maxHealth}";
    }
}