using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : Singleton<Player>
{
    public const int MAX_ENERGY = 4;
    public Health Health;
    private int _energy;
    public int Energy
    {
        get
        {
            return _energy;
        }
        set
        {
            _energy = value;
            if (_energy < 0)
            {
                _energy = 0;
            }
            _energyText.text = $"Player Energy: {_energy}";
        }
    }
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _energyText;

    void Start()
    {
        Health = new Health(50, 1);
        Energy = MAX_ENERGY;
        UpdateHealthText(Health.CurrentHealth, Health.MaxHealth);
        Health.e_OnHealthChanged += UpdateHealthText;
    }

    private void UpdateHealthText(float currentHealth, float maxHealth)
    {
        _healthText.text = $"Player Health: {currentHealth}/{maxHealth}";
    }
}
