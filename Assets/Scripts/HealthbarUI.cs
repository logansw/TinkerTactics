using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthbarUI : Singleton<HealthbarUI>
{
    private Health Health;
    [SerializeField] private RectTransform _background;
    [SerializeField] private RectTransform _remainingHealth;
    [SerializeField] private TMP_Text _healthText;

    public void RegisterHealth(Health health)
    {
        Health = health;
        Health.e_OnHealthChanged += UpdateHealthbar;
    }

    private void UpdateHealthbar(float currentHealth, float maxHealth)
    {
        float healthPercentage = currentHealth / maxHealth;
        _remainingHealth.sizeDelta = new Vector2(_background.rect.width * healthPercentage, _remainingHealth.sizeDelta.y);
        _healthText.text = $"{currentHealth}/{maxHealth}";
    }
}
