using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour
{
    public AudioSource music;

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
                case 1:
                    Play();
                    break;
                case 37:
                    Destroy(gameObject);
                    break;
            }
        }
    }
    public void Play()
    {
        music.Play();
    }
}
