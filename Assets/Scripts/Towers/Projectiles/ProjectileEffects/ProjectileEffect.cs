using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;

public abstract class ProjectileEffect : MonoBehaviour
{
    public int Stacks { get; set; }
    public abstract void OnProjectileHitPreDamage(Enemy hit);
    public abstract void OnProjectileHitPostDamage(Enemy hit);
    public abstract void OnProjectileLaunched();
}
