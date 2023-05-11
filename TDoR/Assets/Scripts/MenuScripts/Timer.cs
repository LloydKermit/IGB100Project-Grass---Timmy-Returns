using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static InspectorGadgets.AutoPrefs;

public class Timer : MonoBehaviour
{
    [Header("TextMeshGUI")]
    public TextMeshProUGUI TimerText;

    public static bool TimeisRunning;
    public static float TimeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        TimeisRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeisRunning)
        {
            TimeElapsed += Time.deltaTime;

            float minutes = TimeElapsed / 60;
            float seconds = TimeElapsed % 60;

            TimerText.text = string.Format("Time Elapsed: {0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            return;
        }
    }
}