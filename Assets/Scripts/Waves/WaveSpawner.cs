using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner[] s_WaveSpawners = new WaveSpawner[2];
    [SerializeField] private int _waveSpawnerIndex;
    public bool FinishedSpawning = false;
    public WaveSO[] waves;
    public int currentWaveIndex;
    private int currentSubWaveIndex = 0;
    private int currentEnemyIndex = 0;
    public TilePath SpawnPoint;
    [SerializeField] private BoxCollider2D _boxCollider2d;
    public List<Warlord> Warlords = new List<Warlord>();
    private PlaceholderArt _placeholderArt;

    void Awake()
    {
        s_WaveSpawners[_waveSpawnerIndex] = this;
        _placeholderArt = GetComponent<PlaceholderArt>();
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
            child.gameObject.SetActive(active && HasEnemies());
        }
        if (Warlords.Count > 0)
        {
            _placeholderArt.SetColor(new Color(0.6328792f, 0f, 0.8313726f, 1f));
        }
        else
        {
            _placeholderArt.SetColor(new Color(0.9339623f, 0.4836051f, 0.2423016f, 1f));
        }
    }

    public bool HasEnemies()
    {
        return waves[currentWaveIndex] != null && (Warlords.Count > 0 || waves[currentWaveIndex].subWaves.Length > 0);
    }

    public IEnumerator SpawnEnemies()
    {
        foreach (Warlord warlord in Warlords)
        {
            warlord.Respawn(this);
            yield return new WaitForSeconds(0.5f);
        }
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
        Enemy enemy = Instantiate(enemyPrefab, SpawnPoint.transform.position, Quaternion.identity).GetComponent<Enemy>();
        enemy.Initialize(SpawnPoint);
    }

    public string PreviewWave()
    {
        if (!HasEnemies())
        {
            return "";
        }

        WaveSO nextWave = waves[currentWaveIndex];
        StringBuilder sb = new StringBuilder();
        Dictionary<SubWaveSO.EnemyType, int> enemyCounts = new Dictionary<SubWaveSO.EnemyType, int>();

        foreach (Warlord warlord in Warlords)
        {
            sb.Append($"{warlord.gameObject.name}\n");
        }

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

    public void RegisterWarlord(Warlord warlord)
    {
        warlord.e_OnWarlordEnd += UnregisterWarlord;
        Warlords.Add(warlord);
    }

    public void UnregisterWarlord(Warlord warlord)
    {
        Warlords.Remove(warlord);
        warlord.e_OnWarlordEnd -= UnregisterWarlord;
    }
}