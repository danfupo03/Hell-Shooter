using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnRate;

    [SerializeField]
    private GameObject[] enemyPrefab;

    [SerializeField]
    private bool canSpawn = true;

    public EnemyManager enemyCounter;

    void Awake()
    {
        enemyCounter = FindObjectOfType<EnemyManager>();
    }

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (canSpawn)
        {
            yield return wait;

            int random = Random.Range(0, enemyPrefab.Length);

            GameObject enemy = enemyPrefab[random];

            Instantiate(enemy, transform.position, Quaternion.identity);

            enemyCounter.enemyCount++;
        }
    }
}
