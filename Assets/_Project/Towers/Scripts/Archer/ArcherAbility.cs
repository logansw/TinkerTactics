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
        _tower.CurrentAmmo.Current = _tower.MaxAmmo.Current;
        _tower.AttackSpeed.Current *= AttackSpeedMultiplier;
        _tower.ReloadSpeed.Current *= AttackSpeedMultiplier;
        _tower.BasicAttack.SetClocks();
        yield return new WaitForSeconds(4f);
        _tower.AttackSpeed.Current /= AttackSpeedMultiplier;
        _tower.ReloadSpeed.Current /= AttackSpeedMultiplier;
        _tower.BasicAttack.SetClocks();
        _abilityActive = false;

        ResumeAbilityCD();
    }

    public override bool CanActivate()
    {
        return _abilityReady;
    }
}
