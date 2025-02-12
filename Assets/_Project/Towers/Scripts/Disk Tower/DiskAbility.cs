using UnityEngine;

public class DiskAbiilty : Ability
{
    [Header("Stats")]
    public float AttackSpeedMultiplier;
    private float _initialAttackSpeed;
    private float _initialReloadSpeed;

    [Header("References")]
    private DiskTower _diskTower;

    public override void Initialize()
    {
        base.Initialize();
        _diskTower = GetComponent<DiskTower>();
        _initialAttackSpeed = _tower.AttackSpeed.Current;
        _initialReloadSpeed = _tower.ReloadSpeed.Current;
    }

    void OnDisable()
    {
        _tower.AttackSpeed.Current = _initialAttackSpeed;
        _tower.ReloadSpeed.Current = _initialReloadSpeed;
    }

    public override void Execute()
    {
        _tower.AttackSpeed.Current *= AttackSpeedMultiplier;
        _tower.ReloadSpeed.Current *= AttackSpeedMultiplier;
        _tower.BasicAttack.SetClocks();

        ResumeAbilityCD();
    }

    public override bool CanActivate()
    {
        return _abilityReady;
    }
}