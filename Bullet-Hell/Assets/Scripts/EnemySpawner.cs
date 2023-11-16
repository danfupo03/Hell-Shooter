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

    }

    private void SpawnBoss()
    {
        Vector3 rotation = new Vector3(0, 180, 0);
        Quaternion rotationQuaternion = Quaternion.Euler(rotation);

        GameObject enemy = enemyPrefab[0];
        Instantiate(enemy, transform.position, rotationQuaternion);

        enemyCounter.enemyCount++;
    }

    private void SpawnRam()
    {
        Vector3 rotation = new Vector3(0, 180, 0);
        Quaternion rotationQuaternion = Quaternion.Euler(rotation);

        GameObject enemy = enemyPrefab[1];
        Instantiate(enemy, transform.position, rotationQuaternion);

        enemyCounter.enemyCount++;
    }

    public void OnEnable()
    {
        TimeManager.OnMinuteChanged += TimeCheck;
    }

    public void OnDisable()
    {
        TimeManager.OnMinuteChanged -= TimeCheck;
    }

    private void TimeCheck()
    {
        if (TimeManager.Hour == 0 && TimeManager.Minute == 1)
        {
            SpawnRam();
        }
        else if (TimeManager.Hour == 0 && TimeManager.Minute == 10)
        {
            SpawnRam();
        }
        else if (TimeManager.Hour == 0 && TimeManager.Minute == 30)
        {
            SpawnBoss();
        }
    }
}
