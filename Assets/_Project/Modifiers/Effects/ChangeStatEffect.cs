public class ChangeStatEffect : IStatChangerEffect
{
    private Stat _stat;
    private StatChangeType _statChangeType;
    private float _amount;
    private float _originalValue;

    public ChangeStatEffect(Stat stat, StatChangeType statChangeType, float amount)
    {
        _stat = stat;
        _statChangeType = statChangeType;
        _amount = amount;
        _originalValue = _stat.Current;
    }

    public void ChangeStat()
    {
        switch (_statChangeType)
        {
            case StatChangeType.Additive:
                _stat.Current += _amount;
                break;
            case StatChangeType.Multiplicative:
                _stat.Current *= _amount;
                break;
            case StatChangeType.Set:
                _stat.Current = _amount;
                break;
        }
    }

    public void UndoChange()
    {
        switch (_statChangeType)
        {
            case StatChangeType.Additive:
                _stat.Current -= _amount;
                break;
            case StatChangeType.Multiplicative:
                _stat.Current /= _amount;
                break;
            case StatChangeType.Set:
                _stat.Current = _originalValue;
                break;
        }
    }
}
