using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private Health Health;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform _remainingHealth;
    [SerializeField] private GameObject _breakpointPrefab;
    [SerializeField] private TMP_Text _healthText;

    public void Initialize(Health health)
    {
        RegisterHealth(health);
        DrawBreakpoints();
        UpdateHealthbar(health.CurrentHealth, health.MaxHealth);
    }

    public void RegisterHealth(Health health)
    {
        Health = health;
        Health.e_OnHealthChanged += UpdateHealthbar;
    }

    private void UpdateHealthbar(float currentHealth, float maxHealth)
    {
        float healthPercentage = currentHealth / maxHealth;
        _remainingHealth.localScale = new Vector3(healthPercentage, 1, 1);
        _healthText.text = $"{currentHealth}/{maxHealth}";
    }

    private void DrawBreakpoints()
    {
        float barWidth = _spriteRenderer.bounds.size.x;
        int segmentCount = Health.SegmentCount;
        float segmentSize = barWidth / segmentCount;

        for (int i = 1; i < segmentCount; i++) {
            GameObject breakpoint = Instantiate(_breakpointPrefab, transform);
            float xPosition = -(barWidth/2) + i * segmentSize;
            breakpoint.transform.localPosition = new Vector3(xPosition, 0, 0);
        }
    }
}