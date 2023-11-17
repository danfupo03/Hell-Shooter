using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float bulletSpeed;

    public EnemyManager enemyCounter;

    private float life = 5;

    Animator anim;

    void Awake()
    {
        enemyCounter = FindObjectOfType<EnemyManager>();
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void WakeUp()
    {
        anim.Play("IdleBattle");
    }

    private IEnumerator FireCircle()
    {
        for (int i = 0; i < 10; i++)
        {
            anim.Play("Attack02");
            for (int j = 0; j < 360; j += 10)
            {
                CreateBullet(Quaternion.Euler(0, j, 0) * Vector3.forward);
            }
            yield return new WaitForSeconds(0.7f);
        }
    }

    private IEnumerator FireCones(int times)
    {
        for (int i = 0; i < times; i++)
        {
            anim.Play("Attack01");

            Quaternion characterRotation = transform.rotation;

            Vector3 baseDirection = characterRotation * Vector3.forward;

            float angleBetweenBullets = 15f;

            CreateBullet(baseDirection);
            CreateBullet(Quaternion.Euler(0, -angleBetweenBullets, 0) * baseDirection);
            CreateBullet(Quaternion.Euler(0, angleBetweenBullets, 0) * baseDirection);
            CreateBullet(Quaternion.Euler(0, -2 * angleBetweenBullets, 0) * baseDirection);
            CreateBullet(Quaternion.Euler(0, 2 * angleBetweenBullets, 0) * baseDirection);

            yield return new WaitForSeconds(0.7f);
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
                case 25:
                    WakeUp();
                    break;
                case 26:
                    StartCoroutine(FireCircle());
                    break;
                case 35:
                    StartCoroutine(FireCones(5));
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

            Destroy(other.gameObject);
        }
    }

    void OnDestroy()
    {
        enemyCounter.enemyCount--;
    }
}
