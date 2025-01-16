using UnityEngine;

[CreateAssetMenu(fileName = "NewSubWave", menuName = "SubWave")]
public class SubWaveSO : ScriptableObject
{
    public string subWaveName;
    public EnemyType[] enemies;
    public int[] enemyCounts;
    public float spawnInterval;

    [System.Serializable]
    public class EnemyType
    {
        public GameObject enemyPrefab;
    }
}