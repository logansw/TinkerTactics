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
        return "Damaging Spell";
    }

    public override TargetingRules GetTargetingRules()
    {
        return new FieldTargetingRule();
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
        Player.s_Instance.Energy -= GetCost();
        Ammo.Current -= 1;
    }

    public override void OnDrawn()
    {
        Cost.Reset();
        _parentCard.Render(true);
        Ammo.Current = Ammo.Base;
    }
}