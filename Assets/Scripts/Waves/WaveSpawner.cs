using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
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
    [SerializeField] private BoxCollider2D _boxCollider2d;

    void Awake()
    {
        s_WaveSpawners.Add(this);
    }

    private void OnMouseEnter()
    {
        string wavePreview = PreviewWave();
        if (wavePreview != "")
        {
            TooltipManager.s_Instance.DisplayTooltip(PreviewWave());
        }
    }

    private void OnMouseExit()
    {
        TooltipManager.s_Instance.HideTooltip();
    }

    public void BeginWave()
    {
        FinishedSpawning = false;
        currentSubWaveIndex = 0;
        currentEnemyIndex = 0;
        StartCoroutine(SpawnEnemies());
    }

    public void Render(bool active)
    {
        foreach (Transform child in transform)
        {
            bool hasNextWave = waves[currentWaveIndex].subWaves.Length > 0;
            child.gameObject.SetActive(active && hasNextWave);
        }
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

    public string PreviewWave()
    {
        WaveSO nextWave = waves[currentWaveIndex];
        if (nextWave == null || nextWave.subWaves.Length == 0)
        {
            return "";
        }

        StringBuilder sb = new StringBuilder();
        Dictionary<SubWaveSO.EnemyType, int> enemyCounts = new Dictionary<SubWaveSO.EnemyType, int>();
        foreach (SubWaveSO subWave in nextWave.subWaves)
        {
            for (int i = 0; i < subWave.enemies.Length; i++)
            {
                if (!enemyCounts.ContainsKey(subWave.enemies[i]))
                {
                    enemyCounts.Add(subWave.enemies[i], subWave.enemyCounts[i]);
                }
                else
                {
                    enemyCounts[subWave.enemies[i]] += subWave.enemyCounts[i];
                }
            }
        }

        foreach (KeyValuePair<SubWaveSO.EnemyType, int> enemyCount in enemyCounts)
        {
            sb.Append($"{enemyCount.Key.enemyPrefab.name} x {enemyCount.Value}\n");
        }

        return sb.ToString();
    }
}