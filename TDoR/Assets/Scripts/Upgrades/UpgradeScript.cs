using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeScript : MonoBehaviour
{
    List<Upgrades> upgradesList = new List<Upgrades>();

    // Start is called before the first frame update
    void Start()
    {
        upgradesList.Add(new Upgrades("Speed1", 1));
        upgradesList.Add(new Upgrades("Speed2", 2));
        upgradesList.Add(new Upgrades("Speed3", 3));
        upgradesList.Add(new Upgrades("Speed4", 4));
        upgradesList.Add(new Upgrades("Speed5", 5));
        upgradesList.Add(new Upgrades("Speed6", 6));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectUpgrade()
    {

    }

}
