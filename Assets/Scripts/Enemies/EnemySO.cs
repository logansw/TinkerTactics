using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "Enemy")]
public class EnemySO : ScriptableObject
{
    public float MaxHealth;
    public int SegmentCount;
    public int MovementSpeed;
    public float Armor;
}