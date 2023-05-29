using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Angel : MonoBehaviour
{
    NavMeshAgent agent;

    public float moveSpeed = 0f;
    public float angelmovespeed = 15f;

    public Collider triggerCollider;

    Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        moveSpeed = angelmovespeed;
    }
    private void Update()
    {
        Movement();

        enemy.Binded += Time.deltaTime;

        if (enemy.player.BindingBrambles == true && this.tag == "Angel")
        {
            if (enemy.Binded >= 0)
            {
                moveSpeed = angelmovespeed;
            }
            else
            {
                moveSpeed = angelmovespeed * 0.5f;
            }
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
            if (enemy.target)
            {
                agent.destination = enemy.target.transform.position;
                agent.speed = moveSpeed;
            }
        }
        catch
        {

        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        StartCoroutine(enemy.TakeHit());
    }

    private void OnTriggerExit(Collider collision)
    {
        StopAllCoroutines();
    }
}
