using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;

    public GameObject target;

    public int damage = 4;
    public int health = 50;


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
        if (target)
        {
            agent.destination = target.transform.position;
        }
    }
    public void takeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //var deathAni = Instantiate(deathEffect, transform.position, transform.rotation);

        //(deathAni, 0.1f);
        Debug.Log("Enemy Dead");
        Destroy(gameObject);


    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    TimmyHealth health = collision.GetComponent<TimmyHealth>();

    //    if (health != null)
    //    {
    //        health.TakeDamage(damage);
    //    }
    //}
}
