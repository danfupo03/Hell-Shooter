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

    private Animator playerAnim;

    private float life = 10;

    void Awake()
    {
        lifeManager = FindObjectOfType<LifeManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        lifeManager.lifeCount = 10;
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);

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
        playerAnim.Play("Attack01");

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
