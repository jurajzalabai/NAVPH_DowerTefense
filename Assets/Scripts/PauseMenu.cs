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
    // Start is called before the first frame update

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

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void Retry(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

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

    // Update is called once per frame
    void Update()
    {
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
