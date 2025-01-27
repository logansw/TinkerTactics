using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerAbility : MonoBehaviour, ITowerAction
{
    public InternalClock InternalClock;
    protected bool _abilityReloaded;
    protected Tower _tower;
    public virtual void Initialize(Tower parentTower)
    {
        _tower = parentTower;
        InternalClock = new InternalClock(_tower.AbilityCooldown, _tower.gameObject);
        InternalClock.e_OnTimerDone += SetAbilityReloaded;
    }

    public void OnDisable()
    {
        InternalClock.Delete();
    }
    
    public abstract bool CanActivate();
	public abstract void OnActionStart();
	public abstract void Execute();
	public abstract void OnActionComplete();

    private void SetAbilityReloaded()
    {
        _abilityReloaded = true;
    }
}
