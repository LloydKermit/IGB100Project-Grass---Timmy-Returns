using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class WaveText : MonoBehaviour
{
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI AngelCounter;
    public AngelSpawner angelSpawner;

    public bool waveCleared;

    public void Awake()
    {
        waveText.text = "Wave: " + WinLose.WavesCount.ToString();
        AngelCounter.text = "Angels Left: " + WinLose.AngelsKilled.ToString() + " / " + WinLose.AngelsLeft.ToString();

        waveCleared = false;
    }
    public void NextWave()
    {
        waveCleared = false;

        if (WinLose.WavesCount < 7)
        {
            waveText.text = "Wave: " + WinLose.WavesCount.ToString();
        }
        else if (WinLose.WavesCount >= 7)
        {
            waveText.text = "Judgement";
        }
    }

    public void WaveDone()
    {
        WinLose.canInteract = true;
        waveCleared = true;
        AngelCounter.text = "Wave Cleared";
    }

    public void AngelsLeft()
    {
        if (WinLose.WavesCount < 7)
        {
            AngelCounter.text = "Angels Killed: " + WinLose.AngelsKilled.ToString() + " / " + (WinLose.AngelsLeft + angelSpawner.maxArch).ToString();
        }
        else
        {
            AngelCounter.text = "Kill the Cube";
        }
    }
}
