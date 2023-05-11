using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public static bool gamePaused = false;

    public GameObject Player;
    public GameObject Weapon;
    public GameObject pauseMenuUI;
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;

        Weapon.GetComponent<Weapon>().enabled = true;
        Player.GetComponent<FirstPersonController>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;

        gamePaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;

        Weapon.GetComponent<Weapon>().enabled = false;
        Player.GetComponent<FirstPersonController>().enabled = false;
        Cursor.lockState = CursorLockMode.None;

        gamePaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

        WinLose.WavesCount = 0;
        WinLose.AngelsLeft = 0;
        WinLose.AngelsKilled = 0;

        Timer.TimeisRunning = false;
        Timer.TimeElapsed = 0;

        gamePaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}