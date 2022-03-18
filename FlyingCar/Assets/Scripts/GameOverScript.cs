using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public void TryAgain()
    {
        Debug.Log("Game!");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }
}
