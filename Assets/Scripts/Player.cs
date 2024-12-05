using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : Singleton<Player>
{
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
            RenderEnergyText();
        }
    }
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _energyText;

    void Start()
    {
        Health = new Health(50);
        UpdateHealthText(Health.CurrentHealth, Health.MaxHealth);
        Health.e_OnHealthChanged += UpdateHealthText;
    }

    void OnEnable()
    {
        IdleState.e_OnIdleStateEnter += IncreaseEnergyCapacity;
    }

    void OnDisable()
    {
        IdleState.e_OnIdleStateEnter -= IncreaseEnergyCapacity;
    }

    private void IncreaseEnergyCapacity()
    {
        Energy += 1;
    }

    private void UpdateHealthText(float currentHealth, float maxHealth)
    {
        _healthText.text = $"Player Health: {currentHealth}/{maxHealth}";
    }

    public void SetEnergy(int amount)
    {
        Energy = amount;
    }

    public void RenderEnergyText()
    {
        _energyText.text = $"Energy Used: {TowerManager.s_Instance.GetTotalEnergyCost()}/{Energy}";
    }
}