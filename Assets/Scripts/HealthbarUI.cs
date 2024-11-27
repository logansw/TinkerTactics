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
    [SerializeField] private GameObject _breakpoint;

    public void RegisterHealth(Health health)
    {
        Health = health;
        Health.e_OnHealthChanged += UpdateHealthbar;
        UpdateHealthbar(Health.CurrentHealth, Health.MaxHealth);
    }

    private void UpdateHealthbar(float currentHealth, float maxHealth)
    {
        float healthPercentage = currentHealth / maxHealth;
        _remainingHealth.sizeDelta = new Vector2(_background.rect.width * healthPercentage, _remainingHealth.sizeDelta.y);
        _healthText.text = $"Boss: {currentHealth}/{maxHealth}";

        if (Health.GetLowerBreakpoint() == int.MinValue || maxHealth == 0)
        {
            return;
        }
        _breakpoint.transform.localPosition = new Vector3(_background.rect.width * Health.GetLowerBreakpoint() / maxHealth, 0, 0);
    }
}