using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float bulletSpeed;

    [SerializeField] private Transform gunOffset;

    public float minX;
    public float maxX;

    public LifeManager lifeManager;

    private float speed = 20;

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
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            MoveCharacterBackward();
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            MoveCharacterForward();
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            MoveCharacterLeft();
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            MoveCharacterRight();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 5;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 20;
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
        playerAnim.Play("Attack02Maintain");

        Vector3 baseDirection = Vector3.forward;

        float angleBetweenBullets = 15f;

        CreateBullet(baseDirection);

        Vector3 leftDirection = Quaternion.Euler(0, -angleBetweenBullets, 0) * baseDirection;
        CreateBullet(leftDirection);

        Vector3 rightDirection = Quaternion.Euler(0, angleBetweenBullets, 0) * baseDirection;
        CreateBullet(rightDirection);
    }

    void MoveCharacterBackward()
    {
        playerAnim.Play("BattleWalkBack");

        Vector3 currentPosition = transform.position;

        currentPosition.z -= speed * Time.deltaTime;

        transform.position = currentPosition;
    }

    void MoveCharacterForward()
    {
        playerAnim.Play("BattleWalkForward");

        Vector3 currentPosition = transform.position;

        currentPosition.z += speed * Time.deltaTime;

        transform.position = currentPosition;
    }

    void MoveCharacterRight()
    {
        playerAnim.Play("BattleWalkRight");

        Vector3 currentPosition = transform.position;

        float newPosition = currentPosition.x + speed * Time.deltaTime;

        float clampedX = Mathf.Clamp(newPosition, minX, maxX);

        transform.position = new Vector3(clampedX, currentPosition.y, currentPosition.z);
    }

    void MoveCharacterLeft()
    {
        playerAnim.Play("BattleWalkLeft");

        Vector3 currentPosition = transform.position;

        float newPosition = currentPosition.x - speed * Time.deltaTime;

        float clampedX = Mathf.Clamp(newPosition, minX, maxX);

        transform.position = new Vector3(clampedX, currentPosition.y, currentPosition.z);
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
