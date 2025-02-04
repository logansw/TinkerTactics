using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadAmmo : CardEffect
{
    private List<Tower> _recipients;
    private FieldTargetingRule _targetingRule;
    public float Range;
    public int ReloadAmount;

    public override TargetPreview GetTargetPreview()
    {
        TargetPreview preview = new GameObject().AddComponent<TargetPreview>();
        preview.Initialize(PreviewSprite);
        preview.transform.localScale = new Vector3(Range, Range, 1);
        return preview;
    }

    public override string GetDescription()
    {
        return "Reload ammo for towers in range";
    }
    
    public override bool CanPrepare()
    {
        bool result = StateController.CurrentState.Equals(StateType.Playing) && base.CanPrepare();
        if (!StateController.CurrentState.Equals(StateType.Playing))
        {
            ToastManager.s_Instance.AddToast("Red cards can only be played during battle.");
        }
        return result;
    }

    public override TargetingRules GetTargetingRules()
    {
        if (_targetingRule == null)
        {
            _targetingRule = new FieldTargetingRule(this);
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

        _recipients = new List<Tower>();
        RaycastHit2D[] hits = Physics2D.CircleCastAll(targetPosition, Range, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            Tower tower = hit.collider.GetComponent<Tower>();
            if (tower != null)
            {
                _recipients.Add(tower);
            }
        }

        if (_recipients.Count == 0)
        {
            ToastManager.s_Instance.AddToast("No towers in range.");
            return false;
        }
        else
        {
            ActivateEffect();
            return true;
        }

    }

    public override void ActivateEffect()
    {
        foreach (Tower tower in _recipients)
        {
            tower.BasicAttack.ChangeCurrentAmmo(ReloadAmount);
        }
    }

    public override void OnDrawn()
    {
        Cost.Reset();
        _parentCard.Render(true);
    }

    public override string GetCardType()
    {
        return "SPELL";
    }

    public override Color GetColor()
    {
        return new Color(1f, 0.4862745f, 0.5677084f, 1f);
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
