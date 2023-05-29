using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public static bool gamePaused = false;

    public GameObject Player;
    public GameObject Weapon;
    public GameObject pauseMenuUI;
    public GameObject winMenuUI;
    public GameObject deathMenuUI;
    public TextMeshProUGUI timer;
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
        timer.enabled = false;

        gamePaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;

        Weapon.GetComponent<Weapon>().enabled = false;
        Player.GetComponent<FirstPersonController>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        timer.enabled = true;

        gamePaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

        gamePaused = false;

        WinLose.WavesCount = 0;
        WinLose.AngelsLeft = 0;
        WinLose.AngelsKilled = 0;
        WinLose.BossDead = false;
        WinLose.canInteract = false;
        WinLose.hasInteracted = true;

        Timer.TimeisRunning = true;
        Timer.TimeElapsed = 0;
        timer.enabled = false;

        OuterTrigger.gateClosed = false;
    }

    public void Win()
    {
        winMenuUI.SetActive(true);

        Time.timeScale = 0;

        Weapon.GetComponent<Weapon>().enabled = false;
        Player.GetComponent<FirstPersonController>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        timer.enabled = true;

        gamePaused = true;
        Timer.TimeisRunning = false;
    }

    public void Death()
    {
        deathMenuUI.SetActive(true);

        Time.timeScale = 0;

        Weapon.GetComponent<Weapon>().enabled = false;
        Player.GetComponent<FirstPersonController>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        timer.enabled = true;

        gamePaused = true;
        Timer.TimeisRunning = false;
    }

    public void Retry()
    {
        Time.timeScale = 1f;

        Weapon.GetComponent<Weapon>().enabled = true;
        Player.GetComponent<FirstPersonController>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        gamePaused = false;

        WinLose.WavesCount = 0;
        WinLose.AngelsLeft = 0;
        WinLose.AngelsKilled = 0;
        WinLose.BossDead = false;
        WinLose.canInteract = false;
        WinLose.hasInteracted = true;

        Timer.TimeisRunning = true;
        Timer.TimeElapsed = 0;
        timer.enabled = false;

        OuterTrigger.gateClosed = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}