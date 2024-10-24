using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

/// <summary>
/// High-level component for a tower. Contains the core logic for attacking enemies.
/// </summary>
public class Tower : MonoBehaviour, ISelectable, ILiftable
{
    private TilePlot _tilePlot;
    private TilePlot TilePlot
    {
        get
        {
            return _tilePlot;
        }
        set
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

    /// <summary>
    /// Assigns the tower to a tile plot by performing a raycast from the tower's position.
    /// If an empty tile plot is found, the tower is assigned to it. 
    /// If an occupied tile plot is found, the towers are swapped.
    /// If no tile plot is found, the tower is returned to its previous position.
    /// </summary>
    public void AssignTowerToTilePlot()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.zero);
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
                    otherPlot.RemoveTower(otherTower);
                    otherPlot.AddTower(this);
                    TilePlot.RemoveTower(this);
                    TilePlot.AddTower(otherTower);
                    TilePlot = otherPlot;
                }
            }
        }
        // Return to plot if no tile plot is found
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
        }
    }

    public void OnDrop()
    {
        AssignTowerToTilePlot();
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