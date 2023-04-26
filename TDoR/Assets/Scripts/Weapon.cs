using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Weapon : MonoBehaviour
{
    Animation Fireanimation;

    // Weapon Attributes
    public float ARDmg = 25.0f;
    public float fireRate = 0.1f;
    public int damage = 1;
    private float fireTime;

    // Fire origin and Target object
    public GameObject firePoint;
    public GameObject Target;

    // Start is called before the first frame update
    void Start()
    {
        Fireanimation = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        ARFiring();
    }

    private void ARFiring()
    {
        if (Input.GetMouseButton(0) && Time.time > fireTime)
        {
            Fireanimation.Play("fire");

            //Raycast Projectile
            RaycastHit hit;
            if (Physics.Raycast(firePoint.transform.position, -(firePoint.transform.position - Target.transform.position).normalized, out hit, 50.0f))
            {

                //Damage Enemies
                if (hit.transform.tag == "Enemy")
                {
                    hit.transform.GetComponent<Enemy>().takeDamage(damage);
                }

            }
;
        }
    }
}
