using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerInfoPanel : MonoBehaviour
{
    [SerializeField] private Tower _tower;
    [SerializeField] private TMP_Text _energyText;
    [SerializeField] private Button _abilityOneButton;
    [SerializeField] private Button _abilityTwoButton;
    [SerializeField] private TMP_Text _abilityOneIcon;
    [SerializeField] private TMP_Text _abilityTwoIcon;

    public void Initialize(Tower tower)
    {
        _tower = tower;
        _abilityOneButton.onClick.AddListener(_tower.BasicAttack.Activate);
        _abilityTwoButton.onClick.AddListener(_tower.Ability.Activate);
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
        _energyText.text = $"{_tower.Energy}/3";
        _abilityOneIcon.text = $"{_tower.BasicAttack.Name} ({_tower.BasicAttack.EnergyCost})";
        _abilityTwoIcon.text = $"{_tower.Ability.Name} ({_tower.Ability.EnergyCost})";
    }
}
