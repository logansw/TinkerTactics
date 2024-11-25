using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellTower : CardEffect
{
    private Tower _target;
    private TileplotTargetingRule _targetingRule;

    public override TargetPreview GetTargetPreview()
    {
        TargetPreview preview = new GameObject().AddComponent<TargetPreview>();
        preview.Initialize(PreviewSprite);
        return preview;
    }

    public override string GetDescription()
    {
        return Description;
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
        if (!validTarget)
        {
            ToastManager.s_Instance.AddToast(GetInvalidTargetMessage());
            return false;
        }

        _target = _targetingRule.TargetTilePlot.Towers[0];
        return validTarget;
    }

    public override void ActivateEffect()
    {
        TowerManager.s_Instance.RemoveTower(_target);
        DeckManager.s_Instance.RestoreCard(_target.ParentCard);
        Player.s_Instance.Energy += 2;
    }

    public override void OnDrawn()
    {
        // Do nothing
    }

    public override string GetCardType()
    {
        return "SPELL";
    }

    public override Color GetColor()
    {
        return new Color(255/255f, 191/255f, 120/255f, 1f);
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
