using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float bulletSpeed;

    // void Start()
    // {
    //     StartCoroutine(FireCircle());
    //     StartCoroutine(FireSpiral(5));
    //     StartCoroutine(FireFlower(5));
    // }

    void Update()
    {

    }

    IEnumerator FireCircle()
    {

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 360; j += 10)
            {
                CreateBullet(Quaternion.Euler(0, 0, j) * Vector2.up);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator FireSpiral()
    {
        for (int i = 0; i < 360; i += 10)
        {
            CreateBullet(Quaternion.Euler(0, 0, i) * Vector2.up);
            CreateBullet(Quaternion.Euler(0, 0, i + 90) * Vector2.up);
            CreateBullet(Quaternion.Euler(0, 0, i + 180) * Vector2.up);
            CreateBullet(Quaternion.Euler(0, 0, i + 270) * Vector2.up);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator FireFlower()
    {
        for (int i = 0; i < 360; i += 10)
        {
            CreateBullet(Quaternion.Euler(0, 0, i) * Vector2.up);
            CreateBullet(Quaternion.Euler(0, 0, i + 90) * Vector2.up);
            CreateBullet(Quaternion.Euler(0, 0, i + 180) * Vector2.up);
            CreateBullet(Quaternion.Euler(0, 0, i + 270) * Vector2.up);

            CreateBullet(Quaternion.Euler(0, 0, -i) * Vector2.up);
            CreateBullet(Quaternion.Euler(0, 0, -i + 90) * Vector2.up);
            CreateBullet(Quaternion.Euler(0, 0, -i + 180) * Vector2.up);
            CreateBullet(Quaternion.Euler(0, 0, -i + 270) * Vector2.up);
            yield return new WaitForSeconds(0.1f);
        }
    }

    void CreateBullet(Vector2 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();

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
        switch (TimeManager.Minute)
        {
            case 1:
                StartCoroutine(FireCircle());
                break;
            case 10:
                StartCoroutine(FireSpiral());
                break;
            case 20:
                StartCoroutine(FireFlower());
                break;
            default:
                break;
        }

    }
}
