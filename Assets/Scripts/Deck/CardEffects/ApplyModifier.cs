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

    public override bool CanCast(Vector3 targetPosition)
    {
        bool validTarget = GetTargetingRules().ValidTarget(targetPosition);
        if (!validTarget) { return false; }

        _recipient = _targetingRule.TargetTilePlot.Towers[0];
        return Modifier.CanAddModifier(_recipient);
    }

    public override void ActivateEffect()
    {
        Tower recipient = _targetingRule.TargetTilePlot.Towers[0];
        Modifier.TryAddModifier(recipient);
    }

    public override void OnDrawn()
    {
        Cost.Reset();
        _parentCard.Render(true);
    }
}