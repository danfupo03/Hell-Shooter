using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private Transform gunOffset;

    private float speed = 30;
    private float horizontalInput;
    private float forwardInput;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            speed = 5;
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            speed = 30;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    void CreateBullet(Vector2 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, gunOffset.position, transform.rotation);
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();

        rigidbody.velocity = bulletSpeed * direction;
    }

    void Fire()
    {
        CreateBullet(Vector2.up);
    }
}