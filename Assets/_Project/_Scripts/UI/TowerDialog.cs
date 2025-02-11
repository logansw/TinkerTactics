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
    [SerializeField] private TooltipTrigger _abilityToolipTrigger;
    [SerializeField] private TooltipTrigger _rangeTooltipTrigger;
    [SerializeField] private IconTooltip _abilityTooltip;
    private RangeGraphic _rangeGraphic;

    // Stats
    [SerializeField] private StatChip _powerStat;
    [SerializeField] private StatChip _attackSpeedStat;
    [SerializeField] private StatChip _ammoStat;
    [SerializeField] private StatChip _reloadSpeedStat;
    [SerializeField] private StatChip _abilityCooldownStat;

    // TinkerTooltipTriggers
    [SerializeField] private List<TooltipTrigger> _tinkerTooltipTriggers;

    // Action Buttons
    private List<Button> _buttons;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _upgradeButton;

    public void Initialize(Tower tower)
    {
        _name.text = tower.name;
        _powerStat.Initialize("POW", tower.Damage.Current);
        _powerStat.Initialize("AS", tower.AttackSpeed.Current);
        _powerStat.Initialize();
        _powerStat.Initialize();
        _powerStat.Initialize();
    }

    // Positions activated buttons properly
    public void PositionButtons()
    {
        throw new System.NotImplementedException();
    }
}
