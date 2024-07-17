using System.Collections;
using UnityEngine;


public class WaveSpawner : Singleton<WaveSpawner>
{
    public bool finishedSpawning = false;
    public WaveSO[] waves;
    public Transform[] spawnPoints;
    private int currentWaveIndex = 0;
    private int currentSubWaveIndex = 0;

    public void BeginWave()
    {
        StartCoroutine(SpawnSubWaves());
    }

    private IEnumerator SpawnSubWaves()
    {
        finishedSpawning = false;
        currentSubWaveIndex = 0;
        WaveSO currentWave = waves[currentWaveIndex];
        while (currentSubWaveIndex < currentWave.subWaves.Length)
        {
            yield return StartCoroutine(SpawnSubWave(currentWave.subWaves[currentSubWaveIndex]));
            yield return new WaitForSeconds(3); // Time between waves, adjust as needed
            currentSubWaveIndex++;
        }
        finishedSpawning = true;
        currentWaveIndex++;
    }

    private IEnumerator SpawnSubWave(SubWaveSO subWave)
    {
        for (int i = 0; i < subWave.enemies.Length; i++)
        {
            for (int j = 0; j < subWave.enemyCounts[i]; j++)
            {
                SpawnEnemy(subWave.enemies[i].enemyPrefab);
                yield return new WaitForSeconds(Random.Range(0.25f, 1f)); // Time between individual enemy spawns, adjust as needed
            }
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}