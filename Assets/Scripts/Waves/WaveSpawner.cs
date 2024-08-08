using System.Collections;
using UnityEngine;

public class WaveSpawner : Singleton<WaveSpawner>
{
    public bool FinishedSpawning = false;
    public WaveSO[] waves;
    private int currentWaveIndex = 0;
    private int currentSubWaveIndex = 0;
    private int currentEnemyIndex = 0;

    void OnEnable()
    {
    }

    void OnDisable()
    {
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
            SubWaveSO currentSubwave = currentWave.subWaves[currentSubWaveIndex];
            for (int i = 0; i < currentSubwave.enemyCounts[currentEnemyIndex]; i++)
            {
                SpawnEnemy(currentWave.subWaves[currentSubWaveIndex].enemies[currentEnemyIndex].enemyPrefab);
                yield return new WaitForSeconds(1f);
            }
            currentEnemyIndex++;
            if (currentEnemyIndex >= currentWave.subWaves[currentSubWaveIndex].enemies.Length)
            {
                currentSubWaveIndex++;
                currentEnemyIndex = 0;
                if (currentSubWaveIndex >= currentWave.subWaves.Length)
                {
                    currentWaveIndex++;
                    currentSubWaveIndex = 0;
                    yield return new WaitForSeconds(10f);
                }
                if (currentWaveIndex > waves.Length)
                {
                    FinishedSpawning = true;
                }
            }
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        // Spawn enemy at a random point in a circle around the spawn point
        float distance = Random.Range(10, 15);
        float theta = Random.Range(0, 360);
        float thetaRad = theta * Mathf.Deg2Rad;
        Vector2 spawnPoint = new Vector2(distance * Mathf.Cos(thetaRad), distance * Mathf.Sin(thetaRad));
        Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
    }
}