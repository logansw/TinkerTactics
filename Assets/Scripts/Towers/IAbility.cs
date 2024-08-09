using UnityEngine;

public interface IAbility
{
    // TODO: Set this up so that the data is pulled in from a ScriptableObject.
    // Currently, these values are set in the Awake method of the implementing class.
    public string Name { get; set; }
    public float Cooldown { get; set; }
    public float InternalClock { get; set; }

    public abstract void Initialize();
    public abstract void Activate();
    public abstract string GetTooltipText();
    public virtual bool IsReloaded()
    {
        return InternalClock >= Cooldown;
    }
    public virtual void Reload()
    {
        InternalClock = 0;
    }
}