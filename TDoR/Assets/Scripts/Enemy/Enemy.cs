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
    public int damage = 20;
    public int health = 100;
    public Collider triggerCollider;

    PlayerScript player;
    WaveText waveText;

    private void Start()
    {
        moveSpeed = 10f;
        if (this.tag == "Angel")
        {
            damage = 20;
            health = 100;

            player = GameObject.Find("PlayerCapsule").GetComponent<PlayerScript>();
        }
        else if (this.tag == "Seraph")
        {
            damage = 500;
            health = 10000;

            player = GameObject.Find("PlayerCapsule").GetComponent<PlayerScript>();
        }
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
        if (player.BindingBrambles == true)
        {
            StartCoroutine(SlowDown());
        }

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

    private void OnTriggerEnter(Collider collision)
    {
        StartCoroutine(TakeHit());
    }

    private void OnTriggerExit(Collider collision)
    {
        StopAllCoroutines();
    }

    IEnumerator TakeHit()
    {
        player.takedamage(damage);
        yield return new WaitForSeconds(1);
        StartCoroutine(TakeHit());
    }

    IEnumerator SlowDown()
    {
        moveSpeed -= 1f;
        yield return new WaitForSeconds(1f);
        moveSpeed += 10f;
    }
}
