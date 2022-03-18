using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void Play()
    {
        Debug.Log("Game!");
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
        Debug.Log("Exit!");
        Application.Quit();
    }
}
