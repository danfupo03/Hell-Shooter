using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    public TextMeshProUGUI enemyCountText;
    public int enemyCount;

    void Update()
    {
        enemyCountText.text = "Enemies: " + enemyCount;
    }
}
