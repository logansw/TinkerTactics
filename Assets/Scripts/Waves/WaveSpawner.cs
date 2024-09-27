using System.Collections;
using UnityEngine;

public class WaveSpawner : Singleton<WaveSpawner>
{
    public bool FinishedSpawning = false;
    public WaveSO[] waves;
    public int currentWaveIndex;
    private int currentSubWaveIndex = 0;
    private int currentEnemyIndex = 0;
    public bool AutoPlay;

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
        if (AutoPlay)
        {
            while (!FinishedSpawning)
            {
                WaveSO currentWave = waves[currentWaveIndex];
                SubWaveSO currentSubwave = currentWave.subWaves[currentSubWaveIndex];
                for (int i = 0; i < currentSubwave.enemyCounts[currentEnemyIndex]; i++)
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
        else
        {
            while (!FinishedSpawning)
            {
                WaveSO currentWave = waves[currentWaveIndex];
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
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        Vector2 spawnPoint;
        // Spawn enemy at a random point in a circle around the spawn point
        if (PathDrawer.s_PathStart == null)
        {
            float distance = Random.Range(10, 15);
            float theta = Random.Range(0, 360);
            float thetaRad = theta * Mathf.Deg2Rad;
            spawnPoint = new Vector2(distance * Mathf.Cos(thetaRad), distance * Mathf.Sin(thetaRad));
        }
        else
        {
            spawnPoint = PathDrawer.s_PathStart.transform.position;
        }
        Enemy enemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity).GetComponent<Enemy>();
    }
}