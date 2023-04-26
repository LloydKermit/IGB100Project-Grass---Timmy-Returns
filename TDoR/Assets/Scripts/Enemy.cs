using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    public int damage = 4;
    public int health = 50;


    private void Start()
    {

    }
    private void Update()
    {

    }

    private void FixedUpdate()
    {

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
