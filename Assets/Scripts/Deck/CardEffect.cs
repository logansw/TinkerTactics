using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardEffect : MonoBehaviour
{
    public Sprite PreviewSprite;
    protected Card _parentCard;
    public StatInt Cost;
    public string Name;
    public bool Consume;
    public string Description;

    public virtual void Initialize(Card parentCard)
    {
        _parentCard = parentCard;
    }
    
    public abstract TargetPreview GetTargetPreview();
    public virtual int GetCost()
    {
        return Cost.Current;
    }
    public virtual string GetName()
    {
        return Name;
    }
    public abstract string GetDescription();
    public abstract TargetingRules GetTargetingRules();
    /// <summary>
    /// Returns true if the card can be prepared to be cast (i.e. eligible to be dragged from hand)
    /// </summary>
    public virtual bool CanPrepare()
    {
        return Player.s_Instance.Energy >= GetCost();
    }
    /// <summary>
    /// Returns true if the card can be cast at the target position.
    /// </summary>
    /// <param name="targetPosition"></param>
    public virtual bool CanCast(Vector3 targetPosition)
    {
        return GetTargetingRules().ValidTarget(targetPosition);
    }
    public virtual void Cast()
    {
        ActivateEffect();
        Player.s_Instance.Energy -= GetCost();
        if (ConsumeAfterUse())
        {
            _parentCard.Consume();
        }
        else
        {
            _parentCard.Discard();
        }
    }
    public abstract void ActivateEffect();
    public abstract void OnDrawn();
    public virtual bool ConsumeAfterUse()
    {
        return Consume;
    }
    public abstract string GetCardType();
}
