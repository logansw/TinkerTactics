public interface IStatChangerEffect : Effect
{
    public void ChangeStat();
    public void UndoChange();
}

public enum StatChangeType
{
    Additive,
    Multiplicative,
    Set
}