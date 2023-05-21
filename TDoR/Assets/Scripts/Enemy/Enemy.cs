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

    public float Binded = 0;

    WaveText waveText;

    private void Start()
    {
        waveText = GameObject.Find("GameController").GetComponent<WaveText>();

        if (this.tag == "Angel")
        {
            damage = 20;
            health = 100;
        }
    }

    public void takeDamage(int damage)
    {
        Binded = -2;

        health -= damage;

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
}
