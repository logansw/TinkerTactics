using TMPro;
using UnityEngine;
using UnityEngine.AI;

public abstract class Ability : MonoBehaviour, ITowerAction
{
    private AbilityBar _abilityBar;
    private InternalClock _cooldownClock;
    protected Tower _tower;
    protected bool _abilityReady;

    void Awake()
    {
        _abilityBar = transform.Find("AbilityBar").GetComponent<AbilityBar>();
    }

    void OnEnable()
    {
        _tower = GetComponent<Tower>();
        _cooldownClock = new InternalClock(_tower.AbiiltyCooldown.Max, _tower.gameObject);
        _cooldownClock.e_OnTimerDone += SetAbilityReady;
        _abilityBar.RegisterClock(_cooldownClock);
        SetAbilityReady();
        _tower.AbiiltyCooldown.e_OnStatChanged += RebuildClock;
    }

    void OnDisable()
    {
        _cooldownClock.Delete();
    }

    public virtual void Initialize()
    {
        RebuildClock();
    }

    public abstract bool CanActivate();
    public abstract void Execute();

    protected void LockAbilityStatus(bool ready)
    {
        if (ready)
        {
            _abilityBar.SetFill(1.0f);
        }
        else
        {
            _abilityBar.SetFill(0.0f);
        }
        _abilityReady = ready;
        _abilityBar.Locked = true;
        _cooldownClock.Paused = true;
    }

    protected void ResumeAbilityCD()
    {
        _abilityBar.Locked = false;
        _abilityReady = false;
        _cooldownClock.Reset();
        _cooldownClock.Paused = false;
    }
    private void SetAbilityReady()
    {
        LockAbilityStatus(true);
        _abilityReady = true;
    }

    public void RebuildClock()
    {
        if (_cooldownClock != null)
        {
            _cooldownClock.Delete();
        }
        _cooldownClock = new InternalClock(_tower.AbiiltyCooldown.Max, _tower.gameObject);
        _cooldownClock.e_OnTimerDone += SetAbilityReady;
        _abilityBar.RegisterClock(_cooldownClock);
    }
}