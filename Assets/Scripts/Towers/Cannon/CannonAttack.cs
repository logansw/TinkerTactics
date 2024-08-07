using UnityEngine;

public class CannonAttack : MonoBehaviour, IAbility
{
    public string Name { get; set; }
    public float Range { get; set; }
    public float Sweep { get; set; }

    public void Initialize()
    {
        Name = "Cannon Attack";
        Range = 3f;
        Sweep = 180f;
    }

    public void Activate()
    {
        Debug.Log("Cannon Attack");
    }

    public string GetTooltipText()
    {
        return "Cannon Attack";
    }
}