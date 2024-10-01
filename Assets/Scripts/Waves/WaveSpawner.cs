using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WaveSpawner : MonoBehaviour
{
    public static List<WaveSpawner> s_WaveSpawners = new List<WaveSpawner>();    
    public bool FinishedSpawning = false;
    public WaveSO[] waves;
    public int currentWaveIndex;
    private int currentSubWaveIndex = 0;
    private int currentEnemyIndex = 0;
    public TilePath SpawnPoint;

    void Awake()
    {
        s_WaveSpawners.Add(this);
    }

    public void BeginWave()
    {
        FinishedSpawning = false;
        currentSubWaveIndex = 0;
        currentEnemyIndex = 0;
        StartCoroutine(SpawnEnemies());
    }

    public IEnumerator SpawnEnemies()
    {
        while (!FinishedSpawning)
        {
            WaveSO currentWave = waves[currentWaveIndex];
            if (currentWave.subWaves.Length == 0)
            {
                FinishedSpawning = true;
                currentWaveIndex++;
                yield break;
            }
            SubWaveSO currentSubwave = currentWave.subWaves[currentSubWaveIndex];
            for (int i = 0 ; i < currentSubwave.enemyCounts[currentEnemyIndex]; i++)
            {
                SpawnEnemy(currentWave.subWaves[currentSubWaveIndex].enemies[currentEnemyIndex].enemyPrefab);
                yield return new WaitForSeconds(currentSubwave.spawnInterval);
            }
            currentEnemyIndex++;
            if (currentEnemyIndex >= currentWave.subWaves[currentSubWaveIndex].enemies.Length)
            {
                currentSubWaveIndex++;
                currentEnemyIndex = 0;
                if (currentSubWaveIndex >= currentWave.subWaves.Length)
                {
                    currentWaveIndex++;
                    FinishedSpawning = true;
                }
            }
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        Instantiate(enemyPrefab, SpawnPoint.transform.position, Quaternion.identity).GetComponent<Enemy>();
    }
}