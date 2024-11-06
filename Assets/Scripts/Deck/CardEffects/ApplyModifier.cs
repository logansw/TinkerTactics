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
        bool validTarget = GetTargetingRules().CheckValidTarget(targetPosition);
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
            Debug.LogError("No card type defined for modifier type.");
            return "MODIFIER";
        }
    }

    public override Color GetColor()
    {
        if (Modifier is WidgetBase)
        {
            return new Color(0.6745283f, 0.9072257f, 1f, 1f);
        }
        else if (Modifier is TinkerBase)
        {
            return new Color(0.4669811f, 0.6507807f, 1f, 1f);
        }
        else
        {
            Debug.LogError("No color defined for modifier type.");
            return new Color(1f, 1f, 1f, 1f);
        }
    }

    public override void OnCardClicked()
    {
        // Do nothing
    }

    public override void OnCardReturned()
    {
        // Do nothing
    }
}