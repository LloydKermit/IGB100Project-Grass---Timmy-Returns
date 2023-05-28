using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WinLose : MonoBehaviour 
{
    public static int WavesCount = 0;
    public static int AngelsLeft = 0;
    public static int AngelsKilled = 0;
    public static bool BossDead = false;
    public static bool canInteract = false;

    public PauseMenuScript Menu;
    public GameObject upgradePrompt;

    WaveText waveText;

    private IInteractable _interactable;

    public void Start()
    {
        waveText = GetComponent<WaveText>();
    }

    public void Update()
    {
        if (canInteract)
        {
            upgradePrompt.layer = 6;
        }
        else
        {
            upgradePrompt.layer = 0;
        }

        if (BossDead)
        {
            Win();
        }
    }

    public void Win()
    {
        Menu.Win();
    }

    public void Lose()
    {
        Menu.Death();
    }


}
