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
    [SerializeField] private GameObject _breakpointsPrefab;
    [SerializeField] private List<GameObject> _breakpoints;
    [SerializeField] private Transform _breakpointWrapper;

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

        int i = 0;
        foreach (float breakpoint in Health.Breakpoints)
        {
            if (i >= _breakpoints.Count)
            {
                GameObject newBreakpoint = Instantiate(_breakpointsPrefab, _breakpointWrapper);
                newBreakpoint.transform.localPosition = Vector3.zero;
                _breakpoints.Add(newBreakpoint);
            }
            _breakpoints[i].transform.localPosition = new Vector3(_background.rect.width * breakpoint / maxHealth, 0, 0);
            i++;
        }
    }
}