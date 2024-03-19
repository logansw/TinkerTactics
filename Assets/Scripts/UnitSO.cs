using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitSO", menuName = "ScriptableObjects/Unit")]
public class UnitSO : ScriptableObject
{
    public string Name;
    public int MaxHealth;
    public int SegmentCount;
    public int MovementSpeed;
    public int Armor;
}
