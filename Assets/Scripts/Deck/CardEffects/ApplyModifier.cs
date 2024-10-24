using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyModifier : CardEffect
{
    private ModifierBase _modifier;
    public ModifierBase Modifier
    {
        get
        {
            if (_modifier == null)
            {
                _modifier = gameObject.GetComponent<ModifierBase>();
            }
            return _modifier;
        }
        set
        {
            _modifier = value;
        }
    }
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
        return Modifier.GetDescription();
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

    public override string GetCardType()
    {
        if (Modifier is WidgetBase)
        {
            return "WIDGET";
        }
        else if (Modifier is TinkerBase)
        {
            return "TINKER";
        }
        else
        {
            return "MODIFIER";
        }
    }
}