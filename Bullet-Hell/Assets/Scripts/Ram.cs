using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ram : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float bulletSpeed;

    public EnemyManager enemyCounter;

    void Awake()
    {
        enemyCounter = FindObjectOfType<EnemyManager>();
    }

    public IEnumerator Pos1()
    {
        Vector3 targetPos = new Vector3(-1013, 0, 12);

        Vector3 currentPos = transform.position;

        float timeElapsed = 0;
        float timeToMove = 1;

        while (timeElapsed < timeToMove)
        {
            transform.position = Vector3.Lerp(currentPos, targetPos, timeElapsed / timeToMove);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    void FireCircle()
    {
        for (int j = 0; j < 360; j += 10)
        {
            CreateBullet(Quaternion.Euler(0, j, 0) * Vector3.forward);
        }

        Destroy(gameObject);
    }

    void CreateBullet(Vector3 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();

        rigidbody.velocity = bulletSpeed * direction;
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
        if (TimeManager.Hour == 0 && TimeManager.Minute == 2)
        {
            StartCoroutine(Pos1());
        }
        else if (TimeManager.Hour == 0 && TimeManager.Minute == 4)
        {
            FireCircle();
        }
    }

    void OnDestroy()
    {
        enemyCounter.enemyCount--;
    }
}
