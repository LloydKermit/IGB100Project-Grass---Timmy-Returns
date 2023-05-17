using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;
using StarterAssets;
using UnityEngine.Diagnostics;
using System.Linq;

public class UpgradeScript : MonoBehaviour
{
    [Header("Sprites for Image")]
    public Sprite[] iconSprites;

    [Header("The Buttons")]
    public Button[] upgradeButtons;
    public Button confirmButton;


    [Header("List of Upgrade Details")]
    public List<string> upgradeNames;
    public List<string> upgradeDescriptions;

    [Header("Text Mesh Pro")]
    
    public TextMeshProUGUI[] upgradeName;
    public TextMeshProUGUI[] upgradeDescription;

    private int selectedButtonIndex = 0;

    [Header("Game Objects")]
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Weapon;
    [SerializeField] private GameObject Angels;

    private PlayerScript playerScript;
    private Weapon weapon;
    private Enemy enemy;

    [Header("Upgrade Menu")]
    [SerializeField] private GameObject upgradePanel;

    private Dictionary<int, string> availableUpgrades = new Dictionary<int, string>();
    private List<UpgradeButton> assignedUpgrades = new List<UpgradeButton>();

    private void Start()
    {
        playerScript = Player.GetComponent<PlayerScript>();
        weapon = Weapon.GetComponent<Weapon>();

        int availableIndex = 0;
        foreach (var upgrade in upgradeNames)
        {
            availableUpgrades.Add(availableIndex, upgrade);
            availableIndex++;
            Debug.Log(upgrade);
        }

        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            int buttonIndex = i; // Capture the button index
            upgradeButtons[i].onClick.AddListener(() => SelectButton(buttonIndex));
        }

        confirmButton.onClick.AddListener(ApplySelectedUpgrade);
    }

    private void ApplySelectedUpgrade()
    {
        UpgradeButton selectedButton = assignedUpgrades[selectedButtonIndex];

        string upgradeName = assignedUpgrades[selectedButtonIndex].UpgradeName;
        if (availableUpgrades.ContainsValue(upgradeName))
        {
            int upgradeID = availableUpgrades.FirstOrDefault(x => x.Value == upgradeName).Key;
            availableUpgrades.Remove(upgradeID);
        }

        switch (assignedUpgrades[selectedButtonIndex].UpgradeName)
        {
            case "Swift as the Wind":
                SwiftAsTheWind();
                break;
            case "Savage Strength":
                SavageStrength();
                break;
            case "Bullet Fury":
                BulletFury();
                break;
            case "Healing Herbs":
                HealingHerbs();
                break;
            case "Stone Skin":
                StoneSkin();
                break;
            case "Leaf Launch":
                LeafLaunch();
                break;
            case "Binding Brambles":
                BindingBrambles();
                break;
            case "Bountiful Harvest":
                BountifulHarvest();
                break;
            default:
                Debug.LogWarning("Unknown power-up selected!");
                break;
        }

        assignedUpgrades.Clear();
        

        Time.timeScale = 1;

        Weapon.GetComponent<Weapon>().enabled = true;
        Player.GetComponent<FirstPersonController>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;

        upgradePanel.SetActive(false);
    }

    private void SelectButton(int buttonIndex)
    {
        selectedButtonIndex = buttonIndex;
    }
    public void Upgrade()
    {
        List<int> ranUpgrade = new List<int>();

        //Grab 3 random upgrades
        while (ranUpgrade.Count < 3 && availableUpgrades.Count >= 3)
        {
            int randomIndex = Random.Range(0, availableUpgrades.Count);

            int upgradeID = availableUpgrades.Keys.ElementAt(randomIndex);

            if (!ranUpgrade.Contains(upgradeID))
            {
                ranUpgrade.Add(upgradeID);
            }

            if (availableUpgrades.Count < 3)
            {
                break;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            Upgrades upgrade = new Upgrades(iconSprites[ranUpgrade[i]], upgradeNames[ranUpgrade[i]], upgradeDescriptions[ranUpgrade[i]]);

            upgradeButtons[i].image.sprite = upgrade.UpgradeIcon;
            upgradeName[i].text = upgrade.UpgradeName;
            upgradeDescription[i].text = upgrade.UpgradeDescription;

            UpgradeButton upgradeButton = new UpgradeButton(upgrade.UpgradeIcon, upgrade.UpgradeName, upgrade.UpgradeDescription, upgradeButtons[i]);

            assignedUpgrades.Add(upgradeButton);
        }
    }
    private void SwiftAsTheWind()
    {
        //25% increase in speed
        playerScript.SetMovement(1.25f);
    }
    private void SavageStrength()
    {
        //40% increase in dmg
        weapon.SetWeaponDmg(10);
    }

    private void BulletFury()
    {
        //50% increase in fire rate
        weapon.SetWeaponFireRate(1.5f);
    }

    private void HealingHerbs()
    {   
        //25% increase in HP
        playerScript.SetHealth(50);
    }

    private void StoneSkin()
    {
        //Reduce damage by 10%
        playerScript.StoneSkinOn();
    }

    private void LeafLaunch()
    {
        playerScript.SetJumpHeight(0.8f);
    }

    private void BindingBrambles()
    {
        playerScript.BindingBramblesOn();
    }
    
    private void BountifulHarvest()
    {
        playerScript.BountifulHarvestOn();
    }
}
