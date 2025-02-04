using UnityEngine;

public class HealthInfill : MonoBehaviour, IHealthIndicator
{
    public Health Health { get; set; }
    [SerializeField] private SpriteRenderer _infillSpriteRenderer;
    
    public void UpdateUI(float currentHealth, float maxHealth)
    {
        float infillScale = Mathf.Sqrt(currentHealth / maxHealth);
        _infillSpriteRenderer.transform.localScale = Vector2.one * infillScale;
    }
}