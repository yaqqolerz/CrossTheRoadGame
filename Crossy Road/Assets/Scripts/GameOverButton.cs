using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButton : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void BacktoMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
