using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerDialog : Popup
{
    // Tier
    [SerializeField] private TMP_Text _tier;

    // Name
    [SerializeField] private TMP_Text _name;

    // TooltipTriggers
    [SerializeField] private AbilityTooltipTrigger _abilityTooltipTrigger;
    [SerializeField] private RangeTooltipTrigger _rangeTooltipTrigger;
    [SerializeField] private IconTooltip _abilityTooltip;
    private RangeGraphic _rangeGraphic;

    // Stats
    [SerializeField] private StatChip _powerStatChip;
    [SerializeField] private StatChip _attackSpeedStatChip;
    [SerializeField] private StatChip _ammoStatChip;
    [SerializeField] private StatChip _reloadSpeedStatChip;
    [SerializeField] private StatChip _abilityCooldownStatChip;

    // TinkerTooltipTriggers
    [SerializeField] private List<AbilityTooltipTrigger> _tinkerTooltipTriggers;
    [SerializeField] private TinkerEmpty _tinkerEmpty;

    // Action Buttons
    private List<Button> _buttons;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _upgradeButton;

    private Tower _tower;

    public void Initialize(Tower tower)
    {
        _tower = tower;
        _name.text = tower.name;
        _powerStatChip.Initialize(tower.Damage, false, "POW");
        _attackSpeedStatChip.Initialize(tower.AttackSpeed, false, "AS");
        _reloadSpeedStatChip.Initialize(tower.ReloadSpeed, false, "RS");
        _ammoStatChip.Initialize(tower.Ammo, true, "Ammo");
        _abilityCooldownStatChip.Initialize(tower.AbiiltyCooldown, true, "CD");
        _abilityTooltipTrigger.Initialize(tower.Ability);
        _rangeTooltipTrigger.Initialize(tower.RangeIndicator.TowerRangeData);

        foreach (AbilityTooltipTrigger tinkerSlot in _tinkerTooltipTriggers)
        {
            tinkerSlot.Initialize(_tinkerEmpty);
        }
        
        RenderUpgradeButton();
    }

    void OnEnable()
    {
        _upgradeButton.onClick.AddListener(UpgradeTower);
    }

    void OnDisable()
    {
        _upgradeButton.onClick.RemoveAllListeners();
    }

    private void UpgradeTower()
    {
        Player.s_Instance.Gold -= (int)_tower.UpgradeCost;
        _tower.InitialDamage *= 1.2f;
        _tower.Damage.Current = _tower.InitialDamage;
        _tower.UpgradeCost += 10;
        RenderUpgradeButton();
    }

    private void RenderUpgradeButton()
    {
        _upgradeButton.interactable = Player.s_Instance.Gold >= _tower.UpgradeCost;
        _upgradeButton.GetComponentInChildren<TMP_Text>().text = $"Upgrade ({_tower.UpgradeCost}g)";
    }

    // Positions activated buttons properly
    public void PositionButtons()
    {
        throw new System.NotImplementedException();
    }
}
