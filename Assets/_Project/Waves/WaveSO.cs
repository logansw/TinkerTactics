using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWave", menuName = "Wave")]
public class WaveSO : ScriptableObject
{
    public string waveName;
    public SubWaveSO[] subWaves;
}
