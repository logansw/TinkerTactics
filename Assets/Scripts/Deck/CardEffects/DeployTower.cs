using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployTower : CardEffect
{
    public Tower Tower;

    public override void Initialize(Card parentCard)
    {
        base.Initialize(parentCard);
    }

    public override TargetPreview GetTargetPreview()
    {
        TargetPreview preview = new GameObject().AddComponent<TargetPreview>();
        preview.Initialize(PreviewSprite);
        return preview;
    }

    public override string GetName()
    {
        return Tower.Name;
    }

    public override string GetDescription()
    {
        return Description;
    }

    public override TargetingRules GetTargetingRules()
    {
        return new TileplotTargetingRule(true, false);
    }

    public override void ActivateEffect()
    {
        TowerManager.s_Instance.AddTower(Tower, Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    public override void OnDrawn()
    {
        // Do nothing
    }

    public override string GetCardType()
    {
        return "TOWER";
    }

    public override Color GetColor()
    {
        return new Color(1f, 0.8906723f, 0.4858491f, 1f);
    }
}