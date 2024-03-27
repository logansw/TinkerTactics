using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface ILiftable
{
    public void OnLift();
    public void OnDrop();
    public void OnHover();
    public void OnHeld();
}