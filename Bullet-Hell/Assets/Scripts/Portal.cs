using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
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
                case 40:
                    Appearance();
                    break;
                case 43:
                    Destroy();
                    break;
            }
        }
    }

    void Appearance()
    {
        transform.position = new Vector3(6f, 2f, 1.60f);

        Vector3 currentPosition = transform.position;
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
