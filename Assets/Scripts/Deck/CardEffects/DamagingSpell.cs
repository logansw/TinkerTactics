using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamagingSpell : CardEffect
{
    public int Cost;
    public string Name;
    public int Damage;
    public float Range;
    public int Ammo;

    public override TargetPreview GetTargetPreview()
    {
        TargetPreview preview = new GameObject().AddComponent<TargetPreview>();
        preview.Initialize(PreviewSprite);
        preview.transform.localScale = new Vector3(Range, Range, 1);
        return preview;
    }

    public override int GetCost()
    {
        return Cost;
    }

    public override string GetName()
    {
        return Name;
    }

    public override string GetDescription()
    {
        return "Damaging Spell";
    }

    public override TargetingRules GetTargetingRules()
    {
        return new FieldTargetingRule();
    }

    public override bool CanPrepare()
    {
        if (Player.s_Instance.Energy >= GetCost())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override bool CanCast(Vector3 targetPosition)
    {
        if (GetTargetingRules().ValidTarget(targetPosition))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void Cast()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        List<Enemy> enemies = TargetCalculator.GetEnemiesInRange(mousePos, Range);
        foreach (Enemy enemy in enemies)
        {
            enemy.Health.TakeDamage(Damage);
        }
        Player.s_Instance.Energy -= GetCost();
        Ammo -= 1;
        if (Ammo <= 0)
        {
            _parentCard.Discard();
        }
        else
        {
            Cost = 0;
            _parentCard.Render(true);
        }
    }
}
