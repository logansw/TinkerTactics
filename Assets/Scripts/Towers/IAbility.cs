public interface IAbility
{
    // TODO: Set this up so that the data is pulled in from a ScriptableObject.
    // Currently, these values are set in the Awake method of the implementing class.
    public string Name { get; set; }
    public float Range { get; set; }
    public float Sweep { get; set; }

    public abstract void Initialize();
    public abstract void Activate();
    public abstract string GetTooltipText();
}