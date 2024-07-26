using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerInfoPanel : MonoBehaviour
{
    [SerializeField] private Tower _tower;
    [SerializeField] private GameObject _rangeIndicator;
    [SerializeField] private GameObject _rangeIndicatorPrefab;
    [SerializeField] private TMP_Text _energyText;
    [SerializeField] private Button _abilityOneButton;
    [SerializeField] private Button _abilityTwoButton;
    [SerializeField] private TMP_Text _abilityOneIcon;
    [SerializeField] private TMP_Text _abilityTwoIcon;

    public void Initialize(Tower tower)
    {
        _tower = tower;
        _abilityOneButton.onClick.AddListener(_tower.CastBasicAttack);
        _abilityTwoButton.onClick.AddListener(_tower.CastAbility);
        Render();
    }

    void OnEnable()
    {
        _abilityOneButton.onClick.AddListener(Render);
        _abilityTwoButton.onClick.AddListener(Render);
    }

    void OnDisable()
    {
        _abilityOneButton.onClick.RemoveListener(Render);
        _abilityTwoButton.onClick.RemoveListener(Render);
    }

    public void Render()
    {
        _energyText.text = $"{_tower.Energy}/{_tower.MaxEnergy}";
        _abilityOneIcon.text = $"{_tower.BasicAttack.Name} ({_tower.BasicAttack.EnergyCost})";
        _abilityTwoIcon.text = $"{_tower.Ability.Name} ({_tower.Ability.EnergyCost})";
    }

    /// <summary>
    /// Display a tooltip for the ability and display its range.
    /// </summary>
    /// <param name="ability"></param>
    public void DisplayBasicAttackTooltip()
    {
        IAbility ability = _tower.BasicAttack;
        ShowRangeIndicator(true, ability.Range);
        TooltipManager.s_Instance.DisplayTooltip(ability.GetTooltipText());
    }

    public void DisplayAbilityTooltip()
    {
        IAbility ability = _tower.Ability;
        ShowRangeIndicator(true, ability.Range);
        TooltipManager.s_Instance.DisplayTooltip(ability.GetTooltipText());
    }

    public void DisplayTowerTooltip()
    {
        TooltipManager.s_Instance.DisplayTooltip(_tower.GetTooltipText());
    }

    public void HideToolTip()
    {
        ShowRangeIndicator(false, 0);
        TooltipManager.s_Instance.HideTooltip();
    }

    public void ShowRangeIndicator(bool active, float range)
    {
        if (_rangeIndicator == null)
        {
            _rangeIndicator = Instantiate<GameObject>(_rangeIndicatorPrefab);
        }
        
        _rangeIndicator.transform.position = _tower.transform.position;
        _rangeIndicator.transform.localScale = new Vector3(range * 2f, range * 2f, 1f);
        _rangeIndicator.SetActive(active);

    }
}
