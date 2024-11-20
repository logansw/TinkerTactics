using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    public Enemy Enemy;
    public Color32 IconColor;
    public int Stacks;
    protected EffectTracker _effectTracker;

    public virtual void Awake()
    {
        Enemy = GetComponent<Enemy>();
    }

    public virtual void Initialize(float duration, int stacks, EffectTracker effectTracker)
    {
        // Nothing by default
        _effectTracker = effectTracker;
    }

    public abstract void AddStacks(int count);
    public abstract void RemoveStacks(int count);

    public virtual void OnEnable()
    {
    }

    public virtual void OnDisable()
    {
    }

    public virtual void Remove()
    {
        Enemy.EffectTracker.RemoveEffect(this);
        Destroy(this);
    }

    public virtual bool CheckRules()
    {
        return true;
    }

    public abstract string GetStackText();
    public abstract string GetAbbreviationText();
    public abstract string GetDescriptionText();
}
