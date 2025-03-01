using System.Collections.Generic;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(PlotAssigner))]
public class Tower : MonoBehaviour, ISelectable, ILiftable
{
    private BarUI _ammoBar;
    [HideInInspector] public RangeIndicator RangeIndicator;
    [HideInInspector] public BasicAttack BasicAttack;
    [HideInInspector] public EffectProcessor EffectProcessor;
    [HideInInspector] public Card ParentCard;
    [HideInInspector] public PlotAssigner PlotAssigner;
    public bool Active => PlotAssigner.TilePlot.IsActivated;

    [Header("Data")]
    public string Name;
    public int TinkerLimit;
    public float InitialDamage;
    public float InitialAttackSpeed;
    public float InitialReloadSpeed;
    public float InitialAmmo;
    public float _initialAbilityCD;
    [HideInInspector] public Stat Damage;
    [HideInInspector] public Stat AttackSpeed;
    [HideInInspector] public Stat ReloadSpeed;
    [HideInInspector] public Stat Ammo;
    [HideInInspector] public Stat AbiiltyCooldown;
    public Ability Ability { get; private set; }
    public float UpgradeCost;
    public int TinkerCount;

    public virtual string GetTooltipText()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine(Name);
        sb.AppendLine($"Damage: {Damage.Current}");
        sb.AppendLine($"Ammo: {Ammo.Current}/{Ammo.Max}");
        sb.AppendLine($"Attack Speed: {AttackSpeed.Current}");
        sb.AppendLine($"Reload Speed: {ReloadSpeed.Current}");
        sb.AppendLine($"Tinkers Equipped: {TinkerCount}/{TinkerLimit}");

        return sb.ToString();
    }

    protected virtual void Awake()
    {
        BasicAttack = GetComponent<BasicAttack>();

        RangeIndicator = GetComponentInChildren<RangeIndicator>();

        _ammoBar = transform.Find("AmmoBar").GetComponent<BarUI>();

        EffectProcessor = GetComponent<EffectProcessor>();
        PlotAssigner = GetComponent<PlotAssigner>();
        Ability = GetComponent<Ability>();
    }

    public virtual void Initialize()
    {
        PlotAssigner.AssignToPlotBelow();

        Damage = new Stat(0, 9999, InitialDamage);
        AttackSpeed = new Stat(0, 5, InitialAttackSpeed);
        ReloadSpeed = new Stat(0, 5, InitialReloadSpeed);
        Ammo = new Stat(0, InitialAmmo, InitialAmmo);
        AbiiltyCooldown = new Stat(0, _initialAbilityCD, 0);
        List<IStatChangerEffect> statChangerEffects = EffectProcessor.Query<IStatChangerEffect>();
        foreach (IStatChangerEffect statChangerEffect in statChangerEffects)
        {
            statChangerEffect.ChangeStat();
        }
        
        BasicAttack.Initialize(this);
        RangeIndicator.Initialize(this);
        _ammoBar.RegisterStat(Ammo);
        Ability.Initialize();
        
        SelectionManager.s_Instance.ForceNewSelectable(this);
    }

    protected virtual void Update()
    {
        if (!Active) { return; }
        if (BasicAttack.CanActivate())
        {
            if (Ability.CanActivate())
            {
                Ability.Execute();
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
        IdleState.e_OnIdleStateEnter += () => { Ammo.Reset(); };
        IdleState.e_OnIdleStateEnter += Recall;
    }

    protected virtual void OnDisable()
    {
        IdleState.e_OnIdleStateEnter -= Recall;
    }

    public void OnSelect()
    {
        PopupManager.s_Instance.ShowTowerDialogBattle(this);
        RangeIndicator.SetVisible(true);
    }

    public void OnDeselect()
    {
        PopupManager.s_Instance.HideTowerDialogBattle();
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