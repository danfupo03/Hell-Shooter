using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float bulletSpeed;

    public BossLifeManager lifeManager;

    public EnemyManager enemyCounter;

    private float life = 10;

    void Awake()
    {
        lifeManager = FindObjectOfType<BossLifeManager>();
        enemyCounter = FindObjectOfType<EnemyManager>();
    }

    void Start()
    {
        lifeManager.lifeCount = 100;
    }

    void Update()
    {
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator Appearance()
    {
        Vector3 targetPos = new Vector3(-1013, 0, 12);

        Vector3 currentPos = transform.position;

        float timeElapsed = 0;
        float timeToMove = 3;

        while (timeElapsed < timeToMove)
        {
            transform.position = Vector3.Lerp(currentPos, targetPos, timeElapsed / timeToMove);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator FireCircle()
    {
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 360; j += 10)
            {
                CreateBullet(Quaternion.Euler(0, j, 0) * Vector3.forward);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator FireSpiral()
    {
        for (int i = 0; i < 720; i += 10)
        {
            CreateBullet(Quaternion.Euler(0, i, 0) * Vector3.forward);
            CreateBullet(Quaternion.Euler(0, i + 90, 0) * Vector3.forward);
            CreateBullet(Quaternion.Euler(0, i + 180, 0) * Vector3.forward);
            CreateBullet(Quaternion.Euler(0, i + 270, 0) * Vector3.forward);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator FireFlower()
    {
        for (int i = 0; i < 720; i += 10)
        {
            CreateBullet(Quaternion.Euler(0, i, 0) * Vector3.forward);
            CreateBullet(Quaternion.Euler(0, i + 90, 0) * Vector3.forward);
            CreateBullet(Quaternion.Euler(0, i + 180, 0) * Vector3.forward);
            CreateBullet(Quaternion.Euler(0, i + 270, 0) * Vector3.forward);

            CreateBullet(Quaternion.Euler(0, -i, 0) * Vector3.forward);
            CreateBullet(Quaternion.Euler(0, -i + 90, 0) * Vector3.forward);
            CreateBullet(Quaternion.Euler(0, -i + 180, 0) * Vector3.forward);
            CreateBullet(Quaternion.Euler(0, -i + 270, 0) * Vector3.forward);
            yield return new WaitForSeconds(0.3f);
        }
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
                case 10:
                    StartCoroutine(FireCircle());
                    break;
                case 31:
                    StartCoroutine(FireSpiral());
                    break;
                case 47:
                    StartCoroutine(FireFlower());
                    break;
                default:
                    break;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            life -= 1;
            lifeManager.lifeCount -= 1;

            Destroy(other.gameObject);
        }
    }

    void OnDestroy()
    {
        enemyCounter.enemyCount--;
    }
}
