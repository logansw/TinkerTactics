using System.Text;
using UnityEngine;

[RequireComponent(typeof(PlotAssigner))]
public class Tower : MonoBehaviour, ISelectable, ILiftable
{
    private BarUI _ammoBar;
    protected BarUI _abilityBar;
    [HideInInspector] public RangeIndicator RangeIndicator;
    [HideInInspector] public BasicAttack BasicAttack;
    [HideInInspector] public ModifierProcessor ModifierProcessor;
    [HideInInspector] public Card ParentCard;
    [HideInInspector] public PlotAssigner PlotAssigner;
    public bool Active => PlotAssigner.TilePlot.IsActivated;

    [Header("Data")]
    public string Name;
    public int TinkerLimit;
    public StatDamage Damage;
    public StatAttackSpeed AttackSpeed;
    public StatReloadSpeed ReloadSpeed;
    public StatAmmo MaxAmmo;
    public StatInt CurrentAmmo;
    public float AbiiltyCooldown;
    private Ability _ability;

    public virtual string GetTooltipText()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine(Name);
        sb.AppendLine($"Damage: {Damage.Current}");
        sb.AppendLine($"Ammo: {CurrentAmmo.Current}/{MaxAmmo.Current}");
        sb.AppendLine($"Attack Speed: {AttackSpeed.Current}");
        sb.AppendLine($"Reload Speed: {ReloadSpeed.Current}");
        sb.AppendLine($"Tinkers Equipped: {ModifierProcessor.TinkerCount}/{TinkerLimit}");

        return sb.ToString();
    }

    protected virtual void Awake()
    {
        BasicAttack = GetComponent<BasicAttack>();
        BasicAttack.Initialize(this);

        RangeIndicator = GetComponentInChildren<RangeIndicator>();
        RangeIndicator.Initialize(this);

        _ammoBar = transform.Find("AmmoBar").GetComponent<BarUI>();
        _ammoBar.RegisterStat(CurrentAmmo);

        _abilityBar = transform.Find("AbilityBar").GetComponent<BarUI>();
        ModifierProcessor = GetComponent<ModifierProcessor>();
        PlotAssigner = GetComponent<PlotAssigner>();
        _ability = GetComponent<Ability>();
    }

    public virtual void Initialize()
    {
        PlotAssigner.AssignToPlotBelow();
        SelectionManager.s_Instance.ForceNewSelectable(this);
    }

    protected virtual void Update()
    {
        if (!Active) { return; }
        if (BasicAttack.CanActivate())
        {
            if (_ability.CanActivate())
            {
                _ability.Execute();
                BasicAttack.AnimateBasicAttack(0.2f);
                BasicAttack.AttackClock.Reset();
                BasicAttack.SetCannotAttack();
            }
            else
            {
                BasicAttack.Execute();
            }
        }
    }

    protected virtual void OnEnable()
    {
        IdleState.e_OnIdleStateEnter += () => { CurrentAmmo.Reset(); };
        IdleState.e_OnIdleStateEnter += Recall;
    }

    protected virtual void OnDisable()
    {
        IdleState.e_OnIdleStateEnter -= Recall;
    }

    public void OnSelect()
    {
        TooltipManager.s_Instance.DisplayTooltip(GetTooltipText());
        RangeIndicator.SetVisible(true);
    }

    public void OnDeselect()
    {
        TooltipManager.s_Instance.HideTooltip();
        RangeIndicator.SetVisible(false);
    }

    public bool IsSelectable()
    {
        return true;
    }

    public void OnLift()
    {
        if (PlotAssigner.TilePlot != null)
        {
            PlotAssigner.TilePlot.RemoveTower(this);
            DeckManager.s_Instance.ShowReturnTray(true);
        }
    }

    public void OnDrop()
    {
        if (ReturnTray.s_Instance.IsHovered)
        {
            Recall();
        }
        else
        {
            PlotAssigner.AssignToPlotBelow();
        }
        DeckManager.s_Instance.ShowReturnTray(false);
    }

    public void OnHover()
    {
        
    }

    public void OnHeld()
    {
        // Do nothing
    }

    public void Recall()
    {
        TowerManager.s_Instance.RemoveTower(this);
        DeckManager.s_Instance.RestoreCard(ParentCard);
    }
}