/// <summary>
/// Interface for items purchasable through the shop.
/// </summary>

public interface IPurchasable
{
    string Name { get; }
    int YellowCost { get; }
    bool Purchase();
}