
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public int WaveIndex;
    public List<GameObject> Enemies = new List<GameObject>();
    public int EnemyCount = 5;
}

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    [Header("Wave Configuration")]
    public List<Wave> Waves = new List<Wave>();
    public Transform[] SpawnPoints;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void SpawnWave(int waveIndex)
    {
        Wave wave = Waves[waveIndex];

        for (int i = 0; i < wave.EnemyCount; i++)
        {
            Transform spawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
            GameObject enemy = Instantiate(wave.Enemies[0], spawnPoint.position, Quaternion.identity);
            enemy.GetComponent<EnemyAI>().SetWaveIndex(waveIndex);
        }
    }

    public int GetWaveEnemiesCount(int waveIndex)
    {
        return GameObject.FindGameObjectsWithTag($"Wave{waveIndex}").Length;
    }
}
