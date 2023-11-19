using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayGame(string sceneName)
    {
        // Load the scene
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        // Quit the game
        Application.Quit();
    }
}
