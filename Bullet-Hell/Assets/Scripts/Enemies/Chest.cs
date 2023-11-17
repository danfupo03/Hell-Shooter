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
        anim.Play("WakeUp");
    }

    private IEnumerator FireCircle()
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

    private IEnumerator FireCones()
    {
        for (int j = 0; j < 360; j += 10)
        {
            Vector3 baseDirection = Vector3.forward;

            float angleBetweenBullets = 15f;

            CreateBullet(baseDirection);

            Vector3 leftDirection = Quaternion.Euler(0, -angleBetweenBullets, 0) * baseDirection;
            CreateBullet(leftDirection);

            Vector3 rightDirection = Quaternion.Euler(0, angleBetweenBullets, 0) * baseDirection;
            CreateBullet(rightDirection);
        }
        yield return new WaitForSeconds(0.5f);
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
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            life -= 1;

            Destroy(other.gameObject);
        }
    }

    void OnDestroy()
    {
        enemyCounter.enemyCount--;
    }
}
