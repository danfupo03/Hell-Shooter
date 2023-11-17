using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnRate;

    [SerializeField]
    private GameObject[] enemyPrefab;

    public EnemyManager enemyCounter;
    Vector3 spawnpoint1;
    Vector3 spawnpoint2;

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

    private void SpawnBomb()
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
        if (TimeManager.Hour == 0)
        {
            switch (TimeManager.Minute)
            {
                case 1:
                    SpawnBomb();
                    break;
                case 5:
                    SpawnBomb();
                    break;
                case 9:
                    SpawnBomb();
                    break;
                case 13:
                    SpawnBomb();
                    break;
                case 17:
                    SpawnBomb();
                    break;
                case 21:
                    SpawnBomb();
                    break;
            }
        }
    }
}
