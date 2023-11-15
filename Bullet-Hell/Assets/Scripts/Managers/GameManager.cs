using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI bulletCountText;
    public int bulletCount = 0;

    void Update()
    {
        bulletCountText.text = "Bullets: " + bulletCount;
    }
}
