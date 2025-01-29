using UnityEngine;

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

    void Start()
    {
        Initialize();
    }

    void OnEnable()
    {
        _tower = GetComponent<Tower>();
        _cooldownClock = new InternalClock(_tower.AbiiltyCooldown, _tower.gameObject);
        _cooldownClock.e_OnTimerDone += SetAbilityReady;
        _abilityBar.RegisterClock(_cooldownClock);
        SetAbilityReady();
    }

    void OnDisable()
    {
        _cooldownClock.Delete();
    }

    public abstract void Initialize();
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
        _abilityBar.Locked = true;
    }

    protected void ResumeAbilityCD()
    {
        _abilityBar.Locked = false;
        _abilityReady = false;
        _cooldownClock.Reset();
    }
    private void SetAbilityReady()
    {
        LockAbilityStatus(true);
        _abilityReady = true;
    }
}