using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITowerAction
{
    public abstract bool CanActivate();
    public abstract void Execute();
}