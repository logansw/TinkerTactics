using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerInfoPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _energyText;
    [SerializeField] private TMP_Text _abilityOneIcon;
    [SerializeField] private TMP_Text _abilityTwoIcon;
    [SerializeField] private TMP_Text _abilityThreeIcon;

    public void Render(Tower tower)
    {
        _energyText.text = $"{tower.Energy}/3";
        _abilityOneIcon.text = $"{tower.BasicAttack.Name} ({tower.BasicAttack.EnergyCost})";
        _abilityTwoIcon.text = $"{tower.Ability.Name} ({tower.Ability.EnergyCost})";
    }
}
