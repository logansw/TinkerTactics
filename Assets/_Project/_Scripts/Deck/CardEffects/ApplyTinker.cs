using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyTinker : CardEffect
{
    private Tinker _tinker;
    public Tinker Tinker
    {
        get
        {
            if (_tinker == null)
            {
                _tinker = gameObject.GetComponent<Tinker>();
            }
            return _tinker;
        }
        set
        {
            _tinker = value;
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
        return Tinker.GetDescription();
    }
    
    public override bool CanPrepare()
    {
        bool result = !StateController.CurrentState.Equals(StateType.Playing) && base.CanPrepare();
        if (StateController.CurrentState.Equals(StateType.Playing))
        {
            ToastManager.s_Instance.AddToast("Cannot apply modifiers during a wave.");
        }
        return result;
    }

    public override TargetingRules GetTargetingRules()
    {
        if (_targetingRule == null)
        {
            _targetingRule = new TileplotTargetingRule(false, true, this);
        }
        return _targetingRule;
    }

    public override bool CanCast(Vector3 targetPosition)
    {
        bool validTarget = GetTargetingRules().CheckValidTarget(targetPosition);
        if (!validTarget)
        {
            ToastManager.s_Instance.AddToast(GetInvalidTargetMessage());
            return false;
        }

        _recipient = _targetingRule.TargetTilePlot.Towers[0];
        return Tinker.CanApply(_recipient);
    }

    public override void ActivateEffect()
    {
        Tower recipient = _targetingRule.TargetTilePlot.Towers[0];
        if (Tinker.CanApply(recipient))
        {
            Tinker.Initialize(recipient);
            Tinker.ApplyEffects(recipient.EffectProcessor);
        }
    }

    public override void OnDrawn()
    {
        Cost.Reset();
        _parentCard.Render(true);
    }

    public override string GetCardType()
    {
        return "TINKER";
    }

    public override Color GetColor()
    {
        return new Color(0.4669811f, 0.6507807f, 1f, 1f);
    }

    public override void OnCardClicked()
    {
        // Do nothing
    }

    public override void OnCardReturned()
    {
        // Do nothing
    }

    public override string GetInvalidTargetMessage()
    {
        return "Must be played on a tower.";
    }
}