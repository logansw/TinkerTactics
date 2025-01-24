public interface IHealthIndicator
{
    public Health Health { get; set; }

    public virtual void Initialize(Health health)
    {
        RegisterHealth(health);
        UpdateUI(health.CurrentHealth, health.MaxHealth);
    }
    public void RegisterHealth(Health health)
    {
        Health = health;
        health.e_OnHealthChanged += UpdateUI;
    }
    public void UpdateUI(float currentHealth, float maxHealth);
}