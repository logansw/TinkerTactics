using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyModifier : CardEffect
{
    public ModifierBase Modifier;
    private Tower _recipient;
    private TileplotTargetingRule _targetingRule;

    public override TargetPreview GetTargetPreview()
    {
        TargetPreview preview = new GameObject().AddComponent<TargetPreview>();
        preview.Initialize(PreviewSprite);
        return preview;
    }

    public override int GetCost()
    {
        return Cost.Current;
    }

    public override string GetName()
    {
        return name;
    }

    public override string GetDescription()
    {
        return "Apply a modifier to a target";
    }

    public override TargetingRules GetTargetingRules()
    {
        if (_targetingRule == null)
        {
            _targetingRule = new TileplotTargetingRule(false, true);
        }
        return _targetingRule;
    }

    public override bool CanPrepare()
    {
        return Player.s_Instance.Energy >= GetCost();
    }

    public override bool CanCast(Vector3 targetPosition)
    {
        bool validTarget = GetTargetingRules().ValidTarget(targetPosition);
        if (!validTarget) { return false; }

        _recipient = _targetingRule.TargetTilePlot.Towers[0];
        return Modifier.CanAddModifier(_recipient);
    }

    public override void Cast()
    {
        Tower recipient = _targetingRule.TargetTilePlot.Towers[0];
        Modifier.TryAddModifier(recipient);

        if (ConsumeAfterUse())
        {
            _parentCard.Consume();
        }
        else
        {
            _parentCard.Discard();
        }
    }

    public override void OnDrawn()
    {
        Cost.Reset();
        _parentCard.Render(true);
    }

    public virtual bool ConsumeAfterUse()
    {
        return false;
    }
}