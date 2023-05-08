using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;

    public GameObject target;

    public float moveSpeed = 10f;
    public int damage = 4;
    public int health = 100;
    public Collider triggerCollider;

    WaveText waveText;

    private void Start()
    {
        waveText = GameObject.Find("GameController").GetComponent<WaveText>();

        //Player Reference exception catch
        try
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        catch
        {
            target = null;
        }
    }
    private void Update()
    {
        Movement();

        if (Time.time >= PlayerScript.nextHit)
        {
            triggerCollider.enabled = true;
        }
    }

    private void FixedUpdate()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void Movement()
    {
        try
        {
            if (target)
            {
                agent.destination = target.transform.position;
                agent.speed = moveSpeed;
            }
        }
        catch
        {

        }
    }
    public void takeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();

            WinLose.AngelsKilled += 1;
            waveText.AngelsLeft();
        }


    }

    public void Die()
    {
        //var deathAni = Instantiate(deathEffect, transform.position, transform.rotation);

        //(deathAni, 0.1f);
        Debug.Log("Enemy Dead");
        Destroy(gameObject);


    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision + " Touched.");

        PlayerScript health = collision.GetComponent<PlayerScript>();
        triggerCollider.enabled = false;

        if (health.currentHealth > 0 && Time.time >= PlayerScript.nextHit)
        {
            PlayerScript.nextHit = Time.time + health.hitCD;
            health.takedamage(damage);
        }
    }
}
