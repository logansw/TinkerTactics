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
    private Liftable _liftable;
    public Card ParentCard;
    public int EnergyCost => ParentCard.EnergyCost;
    public EventBus EventBus;

    public virtual string GetTooltipText()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine(Name);
        sb.AppendLine($"Damage: {BasicAttack.Damage.Current}");
        sb.AppendLine($"Range: {Range.Current}");
        sb.AppendLine($"Sweep: {Sweep.Current}");
        sb.AppendLine($"Ammo: {BasicAttack.CurrentAmmo.Current}/{BasicAttack.MaxAmmo.Current}");
        sb.AppendLine($"Attack Speed: {BasicAttack.AttackSpeed.Current}");
        sb.AppendLine($"Reload Speed: {BasicAttack.ReloadSpeed.Current}");
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
        _liftable = GetComponent<Liftable>();
    }

    public void Initialize()
    {
        AssignTowerToTilePlot();
        EventBus = new EventBus();
    }

    protected virtual void Update()
    {
        if (!Active)
        {
            return;
        }
        if (BasicAttack.CanActivate())
        {
            BasicAttack.Execute();
        }
    }

    protected virtual void OnEnable()
    {
        IdleState.e_OnIdleStateEnter += () => { BasicAttack.CurrentAmmo.Reset(); };
    }

    protected virtual void OnDisable() { }

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
            Recall();
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

    public void Recall()
    {
        TowerManager.s_Instance.RemoveTower(this);
        DeckManager.s_Instance.RestoreCard(ParentCard);
        Player.s_Instance.RenderEnergyText();
    }
}