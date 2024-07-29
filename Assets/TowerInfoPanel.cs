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
