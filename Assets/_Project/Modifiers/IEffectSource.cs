using System.Collections.Generic;

public interface IEffectSource
{
    public bool CanApply(Tower recipient);
    public void ApplyEffects(EffectProcessor effectProcessor);
    public abstract string GetDescription();
    public void Initialize(Tower tower);
}