using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamagingSpell : CardEffect
{
    public int Damage;
    public float Range;
    public StatAmmo Ammo;

    public override void Initialize(Card parentCard)
    {
        base.Initialize(parentCard);
    }

    public override TargetPreview GetTargetPreview()
    {
        TargetPreview preview = new GameObject().AddComponent<TargetPreview>();
        preview.Initialize(PreviewSprite);
        preview.transform.localScale = new Vector3(Range, Range, 1);
        return preview;
    }

    public override string GetDescription()
    {
        return Description;
    }

    public override TargetingRules GetTargetingRules()
    {
        return new FieldTargetingRule();
    }

    public override bool CanPrepare()
    {
        bool result = base.CanPrepare() && StateController.CurrentState.Equals(StateType.Playing);
        if (!StateController.CurrentState.Equals(StateType.Playing))
        {
            ToastManager.s_Instance.AddToast("Cannot cast red spells outside of battle.");
        }
        return result;
    }

    public override void Cast()
    {
        ActivateEffect();
        Player.s_Instance.Energy -= GetCost();
        if (Ammo.Current <= 0)
        {
            _parentCard.Discard();
        }
        else
        {
            Cost.Current = 0;
            _parentCard.Render(true);
        }
    }

    public override void ActivateEffect()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        List<Enemy> enemies = TargetCalculator.GetEnemiesInRange(mousePos, Range);
        foreach (Enemy enemy in enemies)
        {
            enemy.Health.TakeDamage(Damage);
        }
        Ammo.Current -= 1;
        BattleManager.s_Instance.UndoTimeScale();
    }

    public override void OnCardClicked()
    {
        BattleManager.s_Instance.SetTimeScale(1f);
    }

    public override void OnCardReturned()
    {
        BattleManager.s_Instance.UndoTimeScale();
    }

    public override void OnDrawn()
    {
        Cost.Reset();
        _parentCard.Render(true);
        Ammo.Current = Ammo.Base;
    }

    public override string GetCardType()
    {
        return "SPELL";
    }

    public override Color GetColor()
    {
        return new Color(1f, 0.4862745f, 0.5677084f, 1f);
    }

    public override string GetInvalidTargetMessage()
    {
        return "Invalid target.";
    }
}