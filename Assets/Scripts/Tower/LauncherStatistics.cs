using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherStatistics
{
    public float TotalDamageDealt;
    public int TotalShotsFired;
    public int TotalHits;
    public int TotalMisses;
    public int TotalEnemiesKilled;

    public LauncherStatistics()
    {
        TotalDamageDealt = 0;
        TotalShotsFired = 0;
        TotalHits = 0;
        TotalMisses = 0;
        TotalEnemiesKilled = 0;
    }
}
