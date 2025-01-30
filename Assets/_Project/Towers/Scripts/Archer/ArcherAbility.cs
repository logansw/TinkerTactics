using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAbility : Ability
{
    [Header("Stats")]
    public float AttackSpeedMultiplier;

    [Header("References")]
    [SerializeField] private GameObject _projectilePrefab;
    private Archer _archerTower;
    private bool _abilityActive;

    public override void Initialize()
    {
        _archerTower = GetComponent<Archer>();
    }

    public override void Execute()
    {
        StartCoroutine(IncreaseAttackSpeed());
    }

    void OnDisable()
    {
        if (_abilityActive)
        {
            _tower.AttackSpeed.Current /= AttackSpeedMultiplier;
            _tower.ReloadSpeed.Current /= AttackSpeedMultiplier;
            _tower.BasicAttack.SetClocks();
        }
    }

    private IEnumerator IncreaseAttackSpeed()
    {
        LockAbilityStatus(false);
        _abilityActive = true;
        _tower.BasicAttack.Execute();
        Debug.Log(_tower.AttackSpeed.Current);
        _tower.CurrentAmmo.Current = _tower.MaxAmmo.Current;
        _tower.AttackSpeed.Current *= AttackSpeedMultiplier;
        _tower.ReloadSpeed.Current *= AttackSpeedMultiplier;
        Debug.Log(_tower.AttackSpeed.Current);
        _tower.BasicAttack.SetClocks();
        yield return new WaitForSeconds(4f);
        _tower.AttackSpeed.Current /= AttackSpeedMultiplier;
        _tower.ReloadSpeed.Current /= AttackSpeedMultiplier;
        _tower.BasicAttack.SetClocks();
        Debug.Log(_tower.AttackSpeed.Current);
        _abilityActive = false;

        ResumeAbilityCD();
    }

    public override bool CanActivate()
    {
        return _abilityReady;
    }
}
