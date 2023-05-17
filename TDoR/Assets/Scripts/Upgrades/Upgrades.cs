using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades
{
    private Sprite Icon;
    private string Name;
    private string Description;

    public Upgrades(Sprite icon, string name, string description)
    {
        Icon = icon;
        Name = name;
        Description = description;
    }

    //Get and set ID of task
    public Sprite UpgradeIcon { get { return Icon; } set { Icon = value; } }
    public string UpgradeName { get { return Name; } set { Name = value; } }
    public string UpgradeDescription { get { return Description; } set { Description = value; } }

}

