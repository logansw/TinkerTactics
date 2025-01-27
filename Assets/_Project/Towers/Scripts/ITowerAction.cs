using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITowerAction
{
    public abstract void Execute();
    public abstract void OnActionStart();
    public abstract void OnActionComplete();
    public abstract bool CanActivate();
}