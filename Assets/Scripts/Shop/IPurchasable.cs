/// <summary>
/// Interface for items purchasable through the shop.
/// </summary>

public interface IPurchasable
{
    string Name { get; }
    int RedCost { get; }
    int YellowCost { get; }
    int BlueCost { get; }
    bool Purchase();
}