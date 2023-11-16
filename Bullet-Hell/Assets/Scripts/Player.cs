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

    public LifeManager lifeManager;

    private float speed = 30;
    private float horizontalInput;
    private float forwardInput;

    private float life = 10;

    public float minX = -1052f;
    public float maxX = -965f;
    public float minZ = -28f;
    public float maxZ = -22f;

    void Awake()
    {
        lifeManager = FindObjectOfType<LifeManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        lifeManager.lifeCount = 10;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        Vector3 newPosition = transform.position + (Vector3.forward * Time.deltaTime * speed * forwardInput) + (Vector3.right * Time.deltaTime * speed * horizontalInput);

        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

        transform.position = newPosition;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 5;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 30;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Fire();
        }

        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    void CreateBullet(Vector3 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, gunOffset.position, transform.rotation);
        Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();

        rigidbody.velocity = bulletSpeed * direction;
    }

    void Fire()
    {
        Vector3 baseDirection = Vector3.forward;

        float angleBetweenBullets = 15f;

        CreateBullet(baseDirection);

        Vector3 leftDirection = Quaternion.Euler(0, -angleBetweenBullets, 0) * baseDirection;
        CreateBullet(leftDirection);

        Vector3 rightDirection = Quaternion.Euler(0, angleBetweenBullets, 0) * baseDirection;
        CreateBullet(rightDirection);
    }

    // Handle collision with bullets
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BossBullet"))
        {
            life -= 1;
            lifeManager.lifeCount -= 1;

            Destroy(other.gameObject);
        }
    }
}
