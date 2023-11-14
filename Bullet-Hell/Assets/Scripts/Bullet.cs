using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameManager bulletCounter;

    void Awake()
    {
        bulletCounter = FindObjectOfType<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        bulletCounter.bulletCount++;
        // Destroy(gameObject, 7);
    }


    void OnDestroy()
    {
        bulletCounter.bulletCount--;
    }

    // When the bullet becomes invisible for the camera, destroy it
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
