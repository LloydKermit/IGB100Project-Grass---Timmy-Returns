using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{

    public string UpgradeName;
    public int index;

    public Upgrades(string upgradeName, int index)
    {
        UpgradeName = upgradeName;
    }
}
