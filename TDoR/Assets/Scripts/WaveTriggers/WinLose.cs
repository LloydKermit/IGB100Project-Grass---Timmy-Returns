using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLose : MonoBehaviour 
{
    public static int WavesCount = 0;
    public static int AngelsLeft = 0;
    public static int AngelsKilled = 0;
    public static bool BossDead = false;

    public GameObject upgradePrompt;

    WaveText waveText;

    public void Start()
    {
        waveText = GetComponent<WaveText>();
    }

    public void Update()
    {
        if (waveText.waveCleared == true && WavesCount != 0)
        {
            upgradePrompt.layer = 6;
        }
        else
        {
            upgradePrompt.layer = 0;
        }
    }

    //public PauseMenu winDeathMenu;

    //public void Win()
    //{
    //    winDeathMenu.winMenuUI.SetActive(true);
    //    Time.timeScale = 0f;
    //}

    //public void Lose()
    //{
    //    winDeathMenu.retryMenuUI.SetActive(true);
    //}


}
