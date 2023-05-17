using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton
{
    private Sprite Icon;
    private string Name;
    private string Description;
    private Button Button;

    public UpgradeButton(Sprite icon, string name, string description, Button button)
    {
        Icon = icon;
        Name = name;
        Description = description;
        Button = button;
    }

    //Get and set ID of task
    public Sprite UpgradeIcon { get { return Icon; } set { Icon = value; } }
    public string UpgradeName { get { return Name; } set { Name = value; } }
    public string UpgradeDescription { get { return Description; } set { Description = value; } }
    public Button upgradeButton { get { return Button; } set { Button = value; } }
}
