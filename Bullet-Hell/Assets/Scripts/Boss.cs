using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float bulletSpeed;

    void Start()
    {
        //StartCoroutine(FireCircle());
        // StartCoroutine(FireSpiral(5));
        StartCoroutine(FireFlower(5));
    }

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

    IEnumerator FireSpiral(int times)
    {
        for (int i = 0; i < (360 * times); i += 10)
        {
            CreateBullet(Quaternion.Euler(0, 0, i) * Vector2.up);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator FireFlower(int times)
    {
        for (int i = 0; i < (360 * times); i += 10)
        {
            CreateBullet(Quaternion.Euler(0, 0, i) * Vector2.up);
            CreateBullet(Quaternion.Euler(0, 0, i + 90) * Vector2.up);
            CreateBullet(Quaternion.Euler(0, 0, i + 180) * Vector2.up);
            CreateBullet(Quaternion.Euler(0, 0, i + 270) * Vector2.up);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.5f);
    }

    void CreateBullet(Vector2 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();

        rigidbody.velocity = bulletSpeed * direction;
    }
}
