using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ScoreSoulCount : MonoBehaviour
{
    public TextMeshProUGUI waveText;

    public void Awake()
    {
        waveText.text = "Wave: " + WinLose.WavesCount.ToString();
    }
    public void increaseWave()
    {



        //if (WinLose.waveNeeded > 0)
        //{
        //    WinLose.soulsNeeded--;
        //    soulsText.text = "Souls Needed: " + WinLose.soulsNeeded.ToString();
        //}

    }
}
