using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameManager bulletCounter;

    public string targetTag = "Enemy";

    private float minX = -7;
    private float maxX = 20f;
    private float minZ = -22f;
    private float maxZ = 7f;

    void Awake()
    {
        bulletCounter = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (transform.position.x > maxX || transform.position.x < minX ||
            transform.position.z > maxZ || transform.position.z < minZ)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        bulletCounter.bulletCount++;
    }

    void OnDestroy()
    {
        bulletCounter.bulletCount--;
    }
}
