using System.Collections;
using UnityEngine;


public class WaveSpawner : Singleton<WaveSpawner>
{
    public bool finishedSpawning = false;
    public WaveSO[] waves;
    private int currentWaveIndex = 0;
    private int currentSubWaveIndex = 0;
    private int currentEnemyIndex = 0;

    void OnEnable()
    {
        BattleManager.e_OnPlayerTurnEnd += ContinueWave;
    }

    void OnDisable()
    {
        BattleManager.e_OnPlayerTurnEnd -= ContinueWave;
    }

    public void ContinueWave()
    {
        WaveSO currentWave = waves[currentWaveIndex];
        SpawnEnemy(currentWave.subWaves[currentSubWaveIndex].enemies[currentEnemyIndex].enemyPrefab);
        currentEnemyIndex++;
        if (currentEnemyIndex >= currentWave.subWaves[currentSubWaveIndex].enemies.Length)
        {
            currentSubWaveIndex++;
            currentEnemyIndex = 0;
        }
        if (currentSubWaveIndex >= currentWave.subWaves.Length)
        {
            currentWaveIndex++;
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        Transform spawnPoint = MapManager.s_Instance.StartTile.transform;
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
}