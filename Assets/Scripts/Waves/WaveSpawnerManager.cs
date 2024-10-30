using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnerManager : Singleton<WaveSpawnerManager>
{
    [SerializeField] private WaveSpawner _waveSpawnerPrefab;
    public List<WaveSpawner> WaveSpawners = new List<WaveSpawner>();
    public int CurrentWaveIndex;
    public Warlord Warlord;

    public void PrepareNextWave()
    {
        UnassignSpawners();
        AssignRandomLanesToSpawners(WaveHolder.s_Instance.Waves[CurrentWaveIndex]);
        CurrentWaveIndex++;
    }

    public void StartWaves()
    {
        foreach (WaveSpawner waveSpawner in WaveSpawners)
        {
            waveSpawner.BeginLane();
            waveSpawner.Render(false);
        }
    }

    public void GenerateWaveSpawner(TilePath startTile)
    {
        WaveSpawner waveSpawner = Instantiate(_waveSpawnerPrefab, startTile.transform.position, Quaternion.identity);
        waveSpawner.transform.position = new Vector3(waveSpawner.transform.position.x, waveSpawner.transform.position.y, -0.1f);
        WaveSpawners.Add(waveSpawner);
        waveSpawner.StartTile = startTile;
    }

    private void AssignLaneToSpawner(Lane lane, WaveSpawner waveSpawner)
    {
        waveSpawner.CurrentLane = lane;
        waveSpawner.IsAssigned = true;
        waveSpawner.Render(true);
    }

    private void AssignLaneToRandomUniqueSpawner(Lane lane)
    {
        List<WaveSpawner> unassignedSpawners = WaveSpawners.FindAll(spawner => !spawner.IsAssigned);
        if (unassignedSpawners.Count == 0)
        {
            return;
        }
        WaveSpawner randomSpawner = unassignedSpawners[Random.Range(0, unassignedSpawners.Count)];
        AssignLaneToSpawner(lane, randomSpawner);
    }

    public void UnassignSpawners()
    {
        foreach (WaveSpawner waveSpawner in WaveSpawners)
        {
            waveSpawner.CurrentLane = null;
            waveSpawner.IsAssigned = false;
        }
    }

    private void AssignWarlordToSpawner(Warlord warlord, WaveSpawner waveSpawner)
    {
        waveSpawner.RegisterWarlord(warlord);
    }

    public void AssignRandomLanesToSpawners(Wave wave)
    {
        for (int i = 0; i < wave.Lanes.Count; i++)
        {
            if (i == 0)
            {
                int randomIndex = Random.Range(0, WaveSpawners.Count);
                AssignLaneToSpawner(wave.Lanes[i], WaveSpawners[randomIndex]);
                AssignWarlordToSpawner(Warlord, WaveSpawners[randomIndex]);
            }
            else
            {
                AssignLaneToRandomUniqueSpawner(wave.Lanes[i]);
            }
        }
    }
}