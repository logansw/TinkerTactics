using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployTower : CardEffect
{
    public Tower Tower;
    public StatInt Cost;

    public override void Initialize(Card parentCard)
    {
        base.Initialize(parentCard);
        Cost.Initialize();
    }

    public override TargetPreview GetTargetPreview()
    {
        TargetPreview preview = new GameObject().AddComponent<TargetPreview>();
        preview.Initialize(PreviewSprite);
        return preview;
    }

    public override int GetCost()
    {
        return Cost.Current;
    }

    public override string GetName()
    {
        return Tower.Name;
    }

    public override string GetDescription()
    {
        return "Tower description here";
    }

    public override TargetingRules GetTargetingRules()
    {
        return new TileplotTargetingRule(true, false);
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
        Instantiate(Tower, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        Player.s_Instance.Energy -= GetCost();
        _parentCard.Consume();
    }

    public override void OnDrawn()
    {
        // Do nothing
    }
}