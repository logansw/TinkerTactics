using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class WaveSpawner : MonoBehaviour
{
    public bool FinishedSpawning = false;
    private int CurrentSquadIndex = 0;
    private int CurrentEnemyIndex = 0;
    [SerializeField] private BoxCollider2D _boxCollider2d;
    public List<Warlord> Warlords = new List<Warlord>();
    private PlaceholderArt _placeholderArt;
    public Lane CurrentLane;
    public TilePath StartTile;
    public bool IsAssigned;

    void Awake()
    {
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

    public void BeginLane()
    {
        FinishedSpawning = false;
        CurrentSquadIndex = 0;
        CurrentEnemyIndex = 0;
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
        return CurrentLane != null;
    }

    public IEnumerator SpawnEnemies()
    {
        while (!FinishedSpawning)
        {
            // Spawn the current enemy in the squad
            Squad currentSquad = CurrentLane.Squads[CurrentSquadIndex];
            SpawnEnemy(currentSquad.Enemy);
            yield return new WaitForSeconds(0.5f);
            CurrentEnemyIndex++;
            // Check if the squad has finished spawning
            if (CurrentEnemyIndex >= currentSquad.Count)
            {
                CurrentSquadIndex++;
                CurrentEnemyIndex = 0;
            }
            // Check if all squads have been spawned
            if (CurrentSquadIndex >= CurrentLane.Squads.Count)
            {
                FinishedSpawning = true;
            }
        }
        foreach (Warlord warlord in Warlords)
        {
            warlord.Respawn(this);
            yield return new WaitForSeconds(0.5f);
        }
        FinishedSpawning = true;
    }

    private void SpawnEnemy(Enemy enemyPrefab)
    {
        Enemy enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity).GetComponent<Enemy>();
        enemy.Initialize(StartTile);
    }

    public string PreviewWave()
    {
        if (!HasEnemies())
        {
            return "";
        }
        
        StringBuilder sb = new StringBuilder();
        Dictionary<Enemy, int> enemyCounts = new Dictionary<Enemy, int>();

        foreach (Warlord warlord in Warlords)
        {
            sb.Append($"{warlord.gameObject.name}\n");
        }

        foreach (Squad squad in CurrentLane.Squads)
        {
            if (!enemyCounts.ContainsKey(squad.Enemy))
            {
                enemyCounts.Add(squad.Enemy, squad.Count);
            }
            else
            {
                enemyCounts[squad.Enemy] += squad.Count;
            }
        }

        foreach (KeyValuePair<Enemy, int> enemyCount in enemyCounts)
        {
            sb.Append($"{enemyCount.Key.gameObject.name} x {enemyCount.Value}\n");
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