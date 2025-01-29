using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

/// <summary>
/// High-level component for a tower. Contains the core logic for attacking enemies.
/// </summary>
[RequireComponent(typeof(PlotAssigner))]
public class Tower : MonoBehaviour, ISelectable, ILiftable
{
    public string Name;
    [HideInInspector] public RangeIndicator RangeIndicator;
    [HideInInspector] public BasicAttack BasicAttack;
    public bool Active;
    private BarUI _ammoBar;
    protected BarUI _abilityBar;
    [HideInInspector] public ModifierProcessor ModifierProcessor;
    public int TinkerLimit;
    private Liftable _liftable;
    public Card ParentCard;
    public int EnergyCost => ParentCard.EnergyCost;

    public StatDamage Damage;
    public StatAttackSpeed AttackSpeed;
    public StatReloadSpeed ReloadSpeed;
    public StatAmmo MaxAmmo;
    public StatInt CurrentAmmo;

    public PlotAssigner PlotAssigner;

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
        _liftable = GetComponent<Liftable>();
        ModifierProcessor = GetComponent<ModifierProcessor>();
        PlotAssigner = GetComponent<PlotAssigner>();
    }

    public virtual void Initialize()
    {
        PlotAssigner.AssignToPlotBelow();
        SelectionManager.s_Instance.ForceNewSelectable(this);
    }

    protected virtual void Update()
    {
        if (!Active)
        {
            return;
        }
        BasicAttack.Tick();
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