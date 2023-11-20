using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossLifeManager : MonoBehaviour
{
    public TextMeshProUGUI lifeCountText;
    public int lifeCount = 0;

    public GameOver gameOver;

    void Update()
    {
        if (lifeCount <= 0)
        {
            gameOver.Setup();
        }
        lifeCountText.text = "Boss life: " + lifeCount;
    }
}