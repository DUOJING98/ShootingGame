using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    public int WaveNumber => waveNumber;
    public float TimeBetweenWaves => timeBetweenWaves;
    [SerializeField] GameObject waveUI;

    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] float timeBetweenSpawns = 1f;
    [SerializeField] float timeBetweenWaves = 1f;
    [SerializeField] int minEnemyCount = 4;
    [SerializeField] int maxEnemyCount = 10;
    [SerializeField] bool spawnEnemy = true;

    WaitForSeconds waitTimeBetweenSpawns;
    WaitForSeconds waitTimeBetweenWaves;
    WaitUntil waitUntilNoEnemy;

    int waveNumber = 1;
    int enemyCount;
    [SerializeField] List<GameObject> enemies;

    protected override void Awake()
    {
        base.Awake();
        enemies = new List<GameObject>();
        waitTimeBetweenSpawns = new WaitForSeconds(timeBetweenSpawns);
        waitTimeBetweenWaves = new WaitForSeconds(timeBetweenWaves);
        waitUntilNoEnemy = new WaitUntil(() => enemies.Count == 0);
    }



    IEnumerator Start()
    {
        while (spawnEnemy)
        {
            yield return waitUntilNoEnemy;
            waveUI.SetActive(true);
            yield return waitTimeBetweenWaves;
            waveUI.SetActive(false);
            yield return StartCoroutine(nameof(RandomSpawnCoroutine));
        }
    }

    IEnumerator RandomSpawnCoroutine()
    {
        enemyCount = Mathf.Clamp(enemyCount, minEnemyCount + waveNumber / 3, maxEnemyCount);
        if (enemies.Count == 0)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                //var enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
                //PoolManager.Release(enemy);
                //enemies.Add(PoolManager.Release(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)]));
                GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
                float paddingX = 1f;
                float paddingY = 1f;

                Vector2 spawnPos = ViewPort.instance.RandomEnemySpawnPosition(paddingX, paddingY);

                
                GameObject enemy = PoolManager.Release(prefab, spawnPos);
                enemies.Add(enemy);

                yield return waitTimeBetweenSpawns;
            }
            waveNumber++;
        }
    }
    public void RemoveFromList(GameObject enemy) => enemies.Remove(enemy);
}
