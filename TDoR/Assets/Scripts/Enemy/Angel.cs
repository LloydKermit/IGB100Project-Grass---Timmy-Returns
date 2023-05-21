using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Angel : MonoBehaviour
{
    NavMeshAgent agent;

    public GameObject target;

    public float moveSpeed = 0f;
    public float angelmovespeed = 15f;

    public int damage = 20;
    public int health = 100;
    public Collider triggerCollider;

    Enemy enemy;
    PlayerScript player;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        moveSpeed = angelmovespeed;
        player = GameObject.Find("PlayerCapsule").GetComponent<PlayerScript>();

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

        enemy.Binded += Time.deltaTime;

        if (player.BindingBrambles == true && this.tag == "Angel")
        {
            if (enemy.Binded >= 0)
            {
                moveSpeed = angelmovespeed;
            }
            else
            {
                moveSpeed = angelmovespeed * 0.8f;
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
}
