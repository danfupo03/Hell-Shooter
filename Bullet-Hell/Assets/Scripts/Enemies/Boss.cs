using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float bulletSpeed;

    public BossLifeManager lifeManager;

    public EnemyManager enemyCounter;

    private int life = 25;

    private Animator anim;

    void Awake()
    {
        lifeManager = FindObjectOfType<BossLifeManager>();
        enemyCounter = FindObjectOfType<EnemyManager>();
    }

    void Start()
    {
        lifeManager.lifeCount = life;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (life <= 0)
        {
            DestroyEnemy();
        }
    }

    public IEnumerator Appearance()
    {

        Vector3 targetPos = new Vector3(6, 0, -7);

        Vector3 currentPos = transform.position;

        float timeElapsed = 0;
        float timeToMove = 3;

        while (timeElapsed < timeToMove)
        {
            anim.Play("WalkForward");
            transform.position = Vector3.Lerp(currentPos, targetPos, timeElapsed / timeToMove);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator FireCircle()
    {
        for (int i = 0; i < 20; i++)
        {
            anim.Play("Attack04");
            for (int j = 0; j < 360; j += 10)
            {
                CreateBullet(Quaternion.Euler(0, j, 0) * Vector3.forward);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator FireSpiral()
    {
        anim.Play("Attack03Start");
        for (int i = 0; i < 765; i += 10)
        {
            anim.Play("Attack03Maintain");
            CreateBullet(Quaternion.Euler(0, i, 0) * Vector3.forward);
            CreateBullet(Quaternion.Euler(0, i + 45, 0) * Vector3.forward);
            CreateBullet(Quaternion.Euler(0, i + 90, 0) * Vector3.forward);
            CreateBullet(Quaternion.Euler(0, i + 135, 0) * Vector3.forward);
            CreateBullet(Quaternion.Euler(0, i + 180, 0) * Vector3.forward);
            CreateBullet(Quaternion.Euler(0, i + 225, 0) * Vector3.forward);
            CreateBullet(Quaternion.Euler(0, i + 270, 0) * Vector3.forward);
            CreateBullet(Quaternion.Euler(0, i + 315, 0) * Vector3.forward);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator FireFlower()
    {
        anim.Play("Attack02Start");
        for (int i = 0; i < 810; i += 10)
        {
            anim.Play("Attack02Maintain");
            CreateBullet(Quaternion.Euler(0, i, 0) * Vector3.forward);
            CreateBullet(Quaternion.Euler(0, i + 45, 0) * Vector3.forward);
            CreateBullet(Quaternion.Euler(0, i + 90, 0) * Vector3.forward);
            CreateBullet(Quaternion.Euler(0, i + 135, 0) * Vector3.forward);
            CreateBullet(Quaternion.Euler(0, i + 180, 0) * Vector3.forward);
            CreateBullet(Quaternion.Euler(0, i + 225, 0) * Vector3.forward);
            CreateBullet(Quaternion.Euler(0, i + 270, 0) * Vector3.forward);
            CreateBullet(Quaternion.Euler(0, i + 315, 0) * Vector3.forward);

            CreateBullet(Quaternion.Euler(0, -i, 0) * Vector3.forward);
            CreateBullet(Quaternion.Euler(0, -i + 45, 0) * Vector3.forward);
            CreateBullet(Quaternion.Euler(0, -i + 90, 0) * Vector3.forward);
            CreateBullet(Quaternion.Euler(0, -i + 135, 0) * Vector3.forward);
            CreateBullet(Quaternion.Euler(0, -i + 180, 0) * Vector3.forward);
            CreateBullet(Quaternion.Euler(0, -i + 225, 0) * Vector3.forward);
            CreateBullet(Quaternion.Euler(0, -i + 270, 0) * Vector3.forward);
            CreateBullet(Quaternion.Euler(0, -i + 315, 0) * Vector3.forward);
            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator FireStar()
    {
        for (int i = 0; i < 15; i++)
        {
            anim.Play("Attack04");
            for (int j = 0; j < 360; j += 36)
            {
                CreateBullet(Quaternion.Euler(0, j, 0) * Vector3.forward);
            }
            yield return new WaitForSeconds(0.5f);
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
                case 41:
                    StartCoroutine(Appearance());
                    break;
                case 45:
                    StartCoroutine(FireCircle());
                    break;
                case 55:
                    StartCoroutine(FireSpiral());
                    break;
            }
        }

        if (TimeManager.Hour == 1)
        {
            switch (TimeManager.Minute)
            {
                case 5:
                    StartCoroutine(FireFlower());
                    break;
                case 15:
                    StartCoroutine(FireStar());
                    break;
                case 28:
                    StartCoroutine(FireCircle());
                    StartCoroutine(FireSpiral());
                    break;
                case 38:
                    StartCoroutine(FireFlower());
                    StartCoroutine(FireStar());
                    break;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            anim.Play("GetHit");
            life -= 1;
            lifeManager.lifeCount -= 1;

            Destroy(other.gameObject);
        }
    }

    void DestroyEnemy()
    {
        enemyCounter.enemyCount--;
        Destroy(gameObject);
    }
}
