using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float bulletSpeed;

    void Update()
    {

    }

    public IEnumerator Appearance()
    {
        transform.position = new Vector3(-347, 45, 20);
        Vector3 targetPos = new Vector3(-347, 13, 20);

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
                CreateBullet(Quaternion.Euler(0, 0, j) * Vector3.up);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator FireSpiral()
    {
        for (int i = 0; i < 720; i += 10)
        {
            CreateBullet(Quaternion.Euler(0, 0, i) * Vector3.up);
            CreateBullet(Quaternion.Euler(0, 0, i + 90) * Vector3.up);
            CreateBullet(Quaternion.Euler(0, 0, i + 180) * Vector3.up);
            CreateBullet(Quaternion.Euler(0, 0, i + 270) * Vector3.up);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator FireFlower()
    {
        for (int i = 0; i < 720; i += 10)
        {
            CreateBullet(Quaternion.Euler(0, 0, i) * Vector3.up);
            CreateBullet(Quaternion.Euler(0, 0, i + 90) * Vector3.up);
            CreateBullet(Quaternion.Euler(0, 0, i + 180) * Vector3.up);
            CreateBullet(Quaternion.Euler(0, 0, i + 270) * Vector3.up);

            CreateBullet(Quaternion.Euler(0, 0, -i) * Vector3.up);
            CreateBullet(Quaternion.Euler(0, 0, -i + 90) * Vector3.up);
            CreateBullet(Quaternion.Euler(0, 0, -i + 180) * Vector3.up);
            CreateBullet(Quaternion.Euler(0, 0, -i + 270) * Vector3.up);
            yield return new WaitForSeconds(0.1f);
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
                case 46:
                    StartCoroutine(FireFlower());
                    break;
                default:
                    break;
            }
        }
    }
}
