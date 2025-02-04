using UnityEngine;

public class Currency
{
    public string Name { get; private set; }
    public int Amount { get; private set; }

    public Currency(string name, int amount)
    {
        Name = name;
        Amount = amount;
    }

    public void AddAmount(int amount)
    {
        Amount += amount;
    }

    public bool SubtractAmount(int amount)
    {
        if (Amount >= amount)
        {
            Amount -= amount;
            return true;
        }
        return false;
    }
}