using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public float difficulty;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayTutorial()
    {
        SceneManager.LoadScene(4);
    }

    // set game dificulty, value represents float number which is used as multiplication factor for enemy health
    public void SetDifficulty(float value)
    {
        PlayerPrefs.SetFloat("difficulty", value);
    }

    public void SetType(int value)
    {
        PlayerPrefs.SetInt("type", value);
    }
}