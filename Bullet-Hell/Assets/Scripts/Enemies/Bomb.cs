using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float bulletSpeed;

    public EnemyManager enemyCounter;

    Animator anim;

    void Awake()
    {
        enemyCounter = FindObjectOfType<EnemyManager>();
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public IEnumerator Appearance()
    {
        anim.Play("Run");

        transform.position = new Vector3(6, -5, -12);

        float randomX = Random.Range(-4f, 16f);
        float randomZ = Random.Range(-19f, -1f);
        Vector3 targetPos = new Vector3(randomX, 0, randomZ);

        Vector3 currentPos = transform.position;

        float timeElapsed = 0;
        float timeToMove = 1;

        while (timeElapsed < timeToMove)
        {
            transform.position = Vector3.Lerp(currentPos, targetPos, timeElapsed / timeToMove);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        anim.Play("Attack01");
    }

    void FireCircle()
    {
        for (int j = 0; j < 360; j += 10)
        {
            CreateBullet(Quaternion.Euler(0, j, 0) * Vector3.forward);
        }

        Destroy(gameObject);
    }

    void FireOval()
    {
        float radiusX = 5.0f;
        float radiusY = 3.0f;

        for (int angle = 0; angle < 360; angle += 10)
        {
            float radians = Mathf.Deg2Rad * angle;
            float x = radiusX * Mathf.Cos(radians);
            float y = radiusY * Mathf.Sin(radians);

            Vector3 position = new Vector3(x, 0, y);
            CreateBullet(position);
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
        if (TimeManager.Hour == 0)
        {
            switch (TimeManager.Minute)
            {
                case 2:
                    StartCoroutine(Appearance());
                    break;
                case 4:
                    FireCircle();
                    FireOval();
                    break;
                case 6:
                    StartCoroutine(Appearance());
                    break;
                case 8:
                    FireCircle();
                    FireOval();
                    break;
                case 10:
                    StartCoroutine(Appearance());
                    break;
                case 12:
                    FireCircle();
                    FireOval();
                    break;
                case 14:
                    StartCoroutine(Appearance());
                    break;
                case 16:
                    FireCircle();
                    FireOval();
                    break;
                case 18:
                    StartCoroutine(Appearance());
                    break;
                case 20:
                    FireCircle();
                    FireOval();
                    break;
                case 22:
                    StartCoroutine(Appearance());
                    break;
                case 24:
                    FireCircle();
                    FireOval();
                    break;
            }
        }
    }

    void OnDestroy()
    {
        enemyCounter.enemyCount--;
    }
}
