using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    public int damage;
    public int health;
    public Collider triggerCollider;
    public GameObject target;

    public float Binded = 0;

    BossHPBar bossHPBar;
    WaveText waveText;
    public PlayerScript player;

    private void Start()
    {
        //Player Reference exception catch
        try
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        catch
        {
            target = null;
        }

        player = GameObject.Find("PlayerCapsule").GetComponent<PlayerScript>();
        bossHPBar = GameObject.Find("BossHPBar").GetComponent<BossHPBar>();
        waveText = GameObject.Find("GameController").GetComponent<WaveText>();

        if (this.tag == "Angel")
        {
            health = 100;
            damage = 20;
        }
        if (this.tag == "Archangel")
        {
            health = 250;
            damage = 40;
        }
        if (this.tag == "Seraph")
        {
            health = 5000;
            damage = 50;

            bossHPBar.SetMaxHealth(health);
        }
    }

    public void takeDamage(int damage)
    {
        Binded = -2;

        health -= damage;

        if (this.tag == "Seraph")
        {
            bossHPBar.SetHealth(health);
        }

        if (health <= 0)
        {
            Die();

            WinLose.AngelsKilled += 1;
            waveText.AngelsLeft();

            if (this.tag == "Seraph")
            {
                WinLose.BossDead = true;
            }
        }
    }

    public void Die()
    {
        //var deathAni = Instantiate(deathEffect, transform.position, transform.rotation);

        //(deathAni, 0.1f);
        Debug.Log("Enemy Dead");
        Destroy(gameObject);
    }

    public IEnumerator TakeHit()
    {
        player.takedamage(damage);
        yield return new WaitForSeconds(1);
        StartCoroutine(TakeHit());
    }

}
    