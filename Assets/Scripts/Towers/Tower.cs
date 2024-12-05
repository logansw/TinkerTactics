using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using TMPro;
using UnityEngine;

/// <summary>
/// High-level component for a tower. Contains the core logic for attacking enemies.
/// </summary>
public class Tower : MonoBehaviour, ISelectable, ILiftable
{
    private TilePlot _tilePlot;
    public TilePlot TilePlot
    {
        get
        {
            return _tilePlot;
        }
        private set
        {
            _tilePlot = value;
            Active = _tilePlot.IsActivated;
        }
    }
    public string Name;
    [HideInInspector] public RangeIndicator RangeIndicator;
    [HideInInspector] public BasicAttack BasicAttack;
    public StatRange Range;
    public StatSweep Sweep;
    public bool Active;
    private BarUI _ammoBar;
    public ModifierProcessor ModifierProcessor;
    public int TinkerLimit;
    public int WidgetLimit = 5;
    private Liftable _liftable;
    public Card ParentCard;

    public virtual string GetTooltipText()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine(Name);
        sb.AppendLine($"Damage: {BasicAttack.Damage.CalculatedFinal}");
        sb.AppendLine($"Range: {Range.CalculatedFinal}");
        sb.AppendLine($"Sweep: {Sweep.CalculatedFinal}");
        sb.AppendLine($"Ammo: {BasicAttack.CurrentAmmo.Current}/{BasicAttack.MaxAmmo.CalculatedFinal}");
        sb.AppendLine($"Attack Speed: {BasicAttack.AttackSpeed.CalculatedFinal}");
        sb.AppendLine($"Reload Speed: {BasicAttack.ReloadSpeed.CalculatedFinal}");
        sb.AppendLine($"Tinkers Equipped: {ModifierProcessor.TinkerCount}/{TinkerLimit}");
        sb.AppendLine($"Widgets Equipped: {ModifierProcessor.WidgetCount}/{WidgetLimit}");

        return sb.ToString();
    }

    protected virtual void Awake()
    {
        BasicAttack = GetComponent<BasicAttack>();
        BasicAttack.Initialize(this);
        RangeIndicator = GetComponentInChildren<RangeIndicator>();
        RangeIndicator.Initialize(this);
        _ammoBar = GetComponentInChildren<BarUI>();
        _ammoBar.RegisterStat(BasicAttack.CurrentAmmo);
        _liftable = GetComponent<Liftable>();
    }

    void Start()
    {
        AssignTowerToTilePlot();
    }

    protected virtual void Update()
    {
        if (!Active)
        {
            return;
        }
        if (BasicAttack.CanActivate())
        {
            BasicAttack.Attack();
        }
    }

    protected virtual void OnEnable()
    {
        ModifierProcessor.e_OnModifierAdded += RecalculateStats;
        IdleState.e_OnIdleStateEnter += () => { BasicAttack.CurrentAmmo.Current = BasicAttack.MaxAmmo.CalculatedFinal;};
    }

    protected virtual void OnDisable()
    {
        ModifierProcessor.e_OnModifierAdded -= RecalculateStats;
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

    /// <summary>
    /// Assigns the tower to a tile plot by performing a raycast from the tower's position.
    /// If an empty tile plot is found, the tower is assigned to it. 
    /// If an occupied tile plot is found, the towers are swapped.
    /// If no tile plot is found, the tower is returned to its previous position.
    /// </summary>
    public void AssignTowerToTilePlot()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, new Vector2(1f, 1f), 0f, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.GetComponent<TilePlot>() != null)
            {
                TilePlot otherPlot = hit.collider.GetComponent<TilePlot>();
                if (otherPlot.AddTower(this))
                {
                    TilePlot = otherPlot;
                    return;
                }
                else
                {
                    // Swap Towers
                    Tower otherTower = otherPlot.Towers[0];
                    TilePlot temp = TilePlot;
                    otherPlot.RemoveTower(otherTower);
                    otherPlot.AddTower(this);
                    TilePlot.RemoveTower(this);
                    TilePlot.AddTower(otherTower);
                    TilePlot = otherPlot;
                    otherTower.TilePlot = temp;
                    return;
                }
            }
        }
        // Return to plot if no tile plot is found
        TilePlot.AddTower(this);
        ReturnToPlot();
    }

    private void ReturnToPlot()
    {
        transform.position = TilePlot.transform.position;
    }

    public void OnLift()
    {
        if (TilePlot != null)
        {
            TilePlot.RemoveTower(this);
            DeckManager.s_Instance.ShowReturnTray(true);
        }
    }

    public void OnDrop()
    {
        if (ReturnTray.s_Instance.IsHovered)
        {
            TowerManager.s_Instance.RemoveTower(this);
            DeckManager.s_Instance.RestoreCard(ParentCard);
        }
        else
        {
            AssignTowerToTilePlot();
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

    private void RecalculateStats()
    {
        ModifierProcessor.CalculateRange(Range);
        ModifierProcessor.CalculateSweep(Sweep);
        ModifierProcessor.CalculateDamage(BasicAttack.Damage);
        ModifierProcessor.CalculateMaxAmmo(BasicAttack.MaxAmmo);
        ModifierProcessor.CalculateAttackSpeed(BasicAttack.AttackSpeed);
        ModifierProcessor.CalculateReloadSpeed(BasicAttack.ReloadSpeed);
    }
}