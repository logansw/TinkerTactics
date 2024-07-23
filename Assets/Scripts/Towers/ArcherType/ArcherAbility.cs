using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Archer))]
public class ArcherAbility : MonoBehaviour, IAbility
{
    private Archer Archer;
    [SerializeField] private string _name;
    public string Name { get; set; }
    [SerializeField] private int _energyCost;
    public int EnergyCost { get; set; }
    [SerializeField] private int _range;
    public float Range { get; set; }

    void Awake()
    {
        Archer = GetComponent<Archer>();
    }

    void OnEnable()
    {
        Name = _name;
        EnergyCost = _energyCost;
        Range = _range;
    }

    public void Activate()
    {
        Archer.MaxEnergy += 1;
    }

    public string GetTooltipText()
    {
        return $"{Name}: Increase Max Energy by 1.";
    }
}