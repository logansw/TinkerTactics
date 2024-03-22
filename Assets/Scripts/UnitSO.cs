using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitSO", menuName = "ScriptableObjects/Unit")]
public class UnitSO : ScriptableObject
{
    public float MaxHealth;
    public int SegmentCount;
    public float MovementSpeed;
    public float Armor;
}
