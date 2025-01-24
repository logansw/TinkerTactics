using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : Singleton<Player>
{
    public Health Health;
    [SerializeField] private TMP_Text _healthText;

    void Start()
    {
        Health = new Health(50);
        UpdateHealthText(Health.CurrentHealth, Health.MaxHealth);
        Health.e_OnHealthChanged += UpdateHealthText;
    }

    private void UpdateHealthText(float currentHealth, float maxHealth)
    {
        _healthText.text = $"Player Health: {currentHealth}/{maxHealth}";
    }

    
}