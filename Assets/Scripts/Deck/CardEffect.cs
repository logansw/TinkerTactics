using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardEffect : MonoBehaviour
{
    public Sprite PreviewSprite;
    protected Card _parentCard;

    public virtual void Initialize(Card parentCard)
    {
        _parentCard = parentCard;
    }
    
    public abstract TargetPreview GetTargetPreview();
    public abstract int GetCost();
    public abstract string GetName();
    public abstract string GetDescription();
    public abstract TargetingRules GetTargetingRules();
    /// <summary>
    /// Returns true if the card can be prepared to be cast (i.e. eligible to be dragged from hand)
    /// </summary>
    public abstract bool CanPrepare();
    /// <summary>
    /// Returns true if the card can be cast at the target position.
    /// </summary>
    /// <param name="targetPosition"></param>
    public abstract bool CanCast(Vector3 targetPosition);
    public abstract void Cast();
    public abstract void OnDrawn();
}
