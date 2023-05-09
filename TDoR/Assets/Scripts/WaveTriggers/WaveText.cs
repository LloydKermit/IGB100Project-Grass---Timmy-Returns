using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class WaveText : MonoBehaviour
{
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI AngelCounter;

    public void Awake()
    {
        waveText.text = "Wave: " + WinLose.WavesCount.ToString();
        AngelCounter.text = "Angels Left: " + WinLose.AngelsKilled.ToString() + " / " + WinLose.AngelsLeft.ToString();
    }
    public void NextWave()
    {
        if (WinLose.WavesCount < 7)
        {
            waveText.text = "Wave: " + WinLose.WavesCount.ToString();
        }
        else if (WinLose.WavesCount >= 7)
        {
            waveText.text = "Judgement except there's no boss";
        }
    }

    public void WaveDone()
    {
        AngelCounter.text = "Wave Cleared";
    }

    public void AngelsLeft()
    {
        AngelCounter.text = "Angels Left: " + WinLose.AngelsKilled.ToString() + " / " + WinLose.AngelsLeft.ToString();
    }
}
