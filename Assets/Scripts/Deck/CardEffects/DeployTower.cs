using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployTower : CardEffect
{
    public Tower Tower;
    private Tower _towerInstance;

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

    public override bool CanPrepare()
    {
        bool result = !StateController.CurrentState.Equals(StateType.Playing) && base.CanPrepare();
        if (TowerManager.s_Instance.GetTotalEnergyCost() + _parentCard.EnergyCost > Player.s_Instance.Energy)
        {
            ToastManager.s_Instance.AddToast("Not enough energy to deploy tower.");
            return false;
        }
        if (StateController.CurrentState.Equals(StateType.Playing))
        {
            ToastManager.s_Instance.AddToast("Cannot deploy towers during a wave.");
        }
        return result;
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
        return new TileplotTargetingRule(true, false, this);
    }

    public override void ActivateEffect()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (_towerInstance == null)
        {
            _towerInstance = TowerManager.s_Instance.AddTower(Tower, new Vector3(mousePosition.x, mousePosition.y, -0.1f));
            _towerInstance.ParentCard = _parentCard;
        }
        else
        {
            _towerInstance.gameObject.SetActive(true);
            _towerInstance.transform.position = new Vector3(mousePosition.x, mousePosition.y, -0.1f);
            TowerManager.s_Instance.Towers.Add(_towerInstance);
        }
        _towerInstance.Initialize();
        Player.s_Instance.RenderEnergyText();
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
        return "Tower must be played on an empty Tile";
    }
}