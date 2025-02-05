using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnerManager : Singleton<WaveSpawnerManager>
{
    [SerializeField] private WaveSpawner _waveSpawnerPrefab;
    public List<WaveSpawner> WaveSpawners = new List<WaveSpawner>();
    public int CurrentWaveIndex;
    public Warlord Warlord;

    public override void Initialize()
    {
        base.Initialize();
        CurrentWaveIndex = 0;
        Warlord = EnemyManager.s_Instance.CurrentWarlord;
    }

    public void PrepareNextWave()
    {
        UnassignSpawners();
        AssignRandomLanesToSpawners(Warlord.WaveHolder.Waves[CurrentWaveIndex]);
        CurrentWaveIndex++;
        if (CurrentWaveIndex > Warlord.WaveHolder.Waves.Count - 1)
        {
            CurrentWaveIndex--;
        }
    }

    public void StartWaves()
    {
        foreach (WaveSpawner waveSpawner in WaveSpawners)
        {
            if (waveSpawner.HasEnemies())
            {
                waveSpawner.BeginLane();
                waveSpawner.Render(false);
            }
            else
            {
                waveSpawner.FinishedSpawning = true;
            }
        }
    }

    public void GenerateWaveSpawner(TilePath startTile)
    {
        WaveSpawner waveSpawner = Instantiate(_waveSpawnerPrefab, startTile.transform.position, Quaternion.identity, transform);
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
            waveSpawner.Warlords.Clear();
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
                // Make healthbar invisible until the 3rd wave
                if (CurrentWaveIndex == 0)
                {
                    HealthbarUI.s_Instance.gameObject.SetActive(false);
                }
                else if (CurrentWaveIndex == 2)
                {
                    HealthbarUI.s_Instance.gameObject.SetActive(true);
                }

                int randomIndex = Random.Range(0, WaveSpawners.Count);
                AssignLaneToSpawner(wave.Lanes[i], WaveSpawners[randomIndex]);
                if (CurrentWaveIndex != 0 && CurrentWaveIndex % 3 == 2 || CurrentWaveIndex == Warlord.WaveHolder.Waves.Count - 1)
                {
                    AssignWarlordToSpawner(Warlord, WaveSpawners[randomIndex]);
                    if (CurrentWaveIndex == 2)
                    {
                        Warlord.Health.AddBreakpoint(Warlord.Health.MaxHealth - Warlord.Health.MaxHealth * 0.1f);
                        Warlord.Health.AddBreakpoint(Warlord.Health.MaxHealth - Warlord.Health.MaxHealth * 0.3f);
                        Warlord.Health.AddBreakpoint(Warlord.Health.MaxHealth - Warlord.Health.MaxHealth * 0.6f);
                    }
                }
            }
            else
            {
                AssignLaneToRandomUniqueSpawner(wave.Lanes[i]);
            }
        }
    }

    public void ClearWaveSpawners()
    {
        for (int i = WaveSpawners.Count - 1; i >= 0; i--)
        {
            WaveSpawner waveSpawner = WaveSpawners[i];
            Destroy(waveSpawner.gameObject);
        }
        WaveSpawners = new List<WaveSpawner>();
    }
}