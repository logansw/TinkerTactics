using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour, IHealthIndicator
{
    public Health Health { get; set; }
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform _remainingHealth;
    [SerializeField] private GameObject _breakpointPrefab;
    [SerializeField] private TMP_Text _healthText;

    public void Initialize(Health health)
    {
        RegisterHealth(health);
        UpdateUI(health.CurrentHealth, health.MaxHealth);
    }

    public void RegisterHealth(Health health)
    {
        Health = health;
        Health.e_OnHealthChanged += UpdateUI;
    }

    public void UpdateUI(float currentHealth, float maxHealth)
    {
        float healthPercentage = currentHealth / maxHealth;
        _remainingHealth.localScale = new Vector3(healthPercentage, 1, 1);
        _healthText.text = $"{currentHealth}/{maxHealth}";
    }
}