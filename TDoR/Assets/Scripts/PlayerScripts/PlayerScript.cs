using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    FirstPersonController firstPersonController;
    WinLose winlose;

    public int maxHealth = 200;
    public int currentHealth;

    public HealthBar healthBar;

    public bool StoneSkin = false;
    public bool BindingBrambles = false;
    public bool BountifulHarvest = false;
    public bool PiercingThorn = false;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 200;

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        firstPersonController = GetComponent<FirstPersonController>();
        winlose = GameObject.Find("GameController").GetComponent<WinLose>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Die()
    {
        winlose.Lose();
    }

    public void Heal()
    {
        currentHealth = maxHealth;
    }
    public void takedamage(int damage)
    {
        if (StoneSkin)
        {
            damage -= 4;

            currentHealth -= damage;

            healthBar.SetHealth(currentHealth);
        }
        else
        {
            currentHealth -= damage;

            healthBar.SetHealth(currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void SetMovement(float moveSpeedMod)
    {
        firstPersonController.MoveSpeed *= moveSpeedMod;
        firstPersonController.SprintSpeed *= moveSpeedMod;
    }

    public void SetHealth(int healthMod)
    {
        maxHealth += healthMod;
        currentHealth = maxHealth;
    }

    public void StoneSkinOn()
    {
        StoneSkin = true;
    }

    public void SetJumpHeight(float jumpHeightMod)
    {
        firstPersonController.JumpHeight += jumpHeightMod;
    }

    public void BindingBramblesOn()
    {
        BindingBrambles = true;
    }

    public void BountifulHarvestOn()
    {
        BountifulHarvest = true;
    }

    //Used for Bountiful Harvest
    public void LifeLeach()
    {
        if (currentHealth != maxHealth)
        {
            currentHealth += 1;
        }
    }

    public void PiercingThornOn()
    {
        PiercingThorn = true;
    }
}
