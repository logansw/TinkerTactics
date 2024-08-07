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
            SpawnEnemy(currentWave.subWaves[currentSubWaveIndex].enemies[currentEnemyIndex].enemyPrefab);
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
            yield return new WaitForSeconds(0.2f);
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