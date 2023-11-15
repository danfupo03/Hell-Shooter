using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LifeManager : MonoBehaviour
{
    public TextMeshProUGUI lifeCountText;
    public int lifeCount = 0;

    void Update()
    {
        lifeCountText.text = "Lives: " + lifeCount;
    }
}