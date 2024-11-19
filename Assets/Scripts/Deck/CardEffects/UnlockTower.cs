using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockTower : CardEffect
{
    private Tower _recipient;
    private TileplotTargetingRule _targetingRule;

    public override TargetPreview GetTargetPreview()
    {
        TargetPreview preview = new GameObject().AddComponent<TargetPreview>();
        preview.Initialize(PreviewSprite);
        return preview;
    }

    public override void ActivateEffect()
    {
        _recipient.Unlock();
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
        return base.CanPrepare() && StateController.CurrentState != StateType.Playing && StateController.CurrentState != StateType.Paused;
    }

    public override bool CanCast(Vector3 targetPosition)
    {
        bool validTarget = GetTargetingRules().CheckValidTarget(targetPosition);
        if (!validTarget) { return false; }

        _recipient = _targetingRule.TargetTilePlot.Towers[0];
        return _recipient.IsLocked;
    }

    public override void OnDrawn()
    {
        // Do nothing
    }

    public override string GetCardType()
    {
        return "SPELL";
    }

    public override string GetDescription()
    {
        return "Unlock a tower until the next wave.";
    }

    public override Color GetColor()
    {
        return new Color(158f/255f, 254f/255f, 193f/255f, 1f);
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
