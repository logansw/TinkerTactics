using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardEffect : MonoBehaviour
{
    public Sprite PreviewSprite;
    protected Card _parentCard;
    public Stat Cost;
    public string Name;
    public string Description;

    public virtual void Initialize(Card parentCard)
    {
        _parentCard = parentCard;
    }
    
    public abstract TargetPreview GetTargetPreview();
    public virtual int GetCost()
    {
        return Cost.CurrentInt;
    }
    public virtual string GetName()
    {
        return Name;
    }
    public abstract TargetingRules GetTargetingRules();
    /// <summary>
    /// Returns true if the card can be prepared to be cast (i.e. eligible to be dragged from hand)
    /// </summary>
    public virtual bool CanPrepare()
    {
        return true;
    }
    /// <summary>
    /// Returns true if the card can be cast at the target position.
    /// </summary>
    /// <param name="targetPosition"></param>
    public virtual bool CanCast(Vector3 targetPosition)
    {
        bool result = GetTargetingRules().CheckValidTarget(targetPosition);
        if (!result)
        {
            ToastManager.s_Instance.AddToast(GetInvalidTargetMessage());
        }
        return result;
    }
    public virtual void Cast()
    {
        ActivateEffect();
        _parentCard.Discard();
    }
    public abstract void ActivateEffect();
    public abstract void OnDrawn();
    public abstract string GetCardType();
    public abstract string GetDescription();
    public abstract Color GetColor();
    public abstract void OnCardClicked();
    public abstract void OnCardReturned();
    public abstract string GetInvalidTargetMessage();
}
