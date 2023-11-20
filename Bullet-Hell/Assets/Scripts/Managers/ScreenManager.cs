using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public List<GameObject> gameStoppers = new List<GameObject>(); // List of game objects that can stop the game

    void Update()
    {
        // Check if any of the gameStopper GameObjects is active
        if (CheckForActiveGameStoppers())
        {
            StopGame();
        }
    }

    bool CheckForActiveGameStoppers()
    {
        // Check if any of the gameStopper GameObjects is active
        foreach (GameObject stopper in gameStoppers)
        {
            if (stopper != null && stopper.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    void StopGame()
    {
        // Add any logic to stop the game here
        Time.timeScale = 0f; // Set time scale to 0 to freeze the game
        Debug.Log("Game Stopped!");
    }
}
