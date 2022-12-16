using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{

    public static bool gamePaused = false;
    public GameObject pauseMenu;
    public GameObject textPlay;

    public GameObject typeOfGameManager;
    public GameObject typeOfGameUnlimitedManager;

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }
    // pause game
    public void Pause()
    {
        // display pause canvas and set time scaling to 0
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    // retry scene with given sceneNumber
    public void Retry(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    // return to main menu
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    private void Awake()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;

        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            textPlay.GetComponent<TextMeshProUGUI>().text = "Play game";
        }
        else
        {
            textPlay.GetComponent<TextMeshProUGUI>().text = "Retry";
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (PlayerPrefs.GetInt("type") == 0)
            {
                typeOfGameManager.SetActive(true);
                typeOfGameUnlimitedManager.SetActive(false);

            }
            else
            {
                typeOfGameUnlimitedManager.SetActive(true);
                typeOfGameManager.SetActive(false);
            }
        }
    }

    void Update()
    {
        // if Escape key is pressed, pause or resume game according to current gamePause state
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gamePaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }
}
