using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public interface IDamager
{
    public void OnDamage();
    public ProjectileLauncher Launcher { get; set; }
}