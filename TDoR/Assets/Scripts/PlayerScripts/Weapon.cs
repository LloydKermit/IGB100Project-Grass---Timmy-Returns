using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Weapon : MonoBehaviour
{
    Animation Fireanimation;

    public Transform muzzleFlashPoint;
    public GameObject muzzleFlash;
    public GameObject EnemyhitEffect;
    public GameObject NonEnemyhitEffect;
    public AudioSource AR15;

    public int ARdamage = 25;
    public float ARrange = 100f;
    public float ARfireRate = 0.1f;

    public Camera fpsCam;


    private float ARnextFire = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Fireanimation = GetComponent<Animation>();
    }
    void Update()
    {

        if (Input.GetButton("Fire1") && Time.time >= ARnextFire)
        {
            AR15.Play();
            ARnextFire = Time.time + ARfireRate;
            ARShoot();
        }
        
    }

    void ARShoot()
    {
        Fireanimation.Play("fire");
        GameObject flash = Instantiate(muzzleFlash, muzzleFlashPoint.position, muzzleFlashPoint.rotation);
        flash.transform.SetParent(muzzleFlashPoint);
        flash.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        Destroy(flash, 0.05f);

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, ARrange))
        {
            //Damage Enemies
            if (hit.transform.tag == "Angel" || hit.transform.tag == "Archangel" || hit.transform.tag == "Seraph")
            {
                hit.transform.GetComponent<Enemy>().takeDamage(ARdamage);

                GameObject EnemyImpact = Instantiate(EnemyhitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(EnemyImpact, 1f);
            }
            else
            {
                GameObject NonEnemyImpact = Instantiate(NonEnemyhitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(NonEnemyImpact, 1f);
            }
        }
    }

    //    Animation Fireanimation;

    //    // Weapon Attributes
    //    public float ARDmg = 25.0f;
    //    public float fireRate = 0.1f;
    //    public int damage = 1;
    //    private float fireTime;

    //    // Fire origin and Target object
    //    public GameObject firePoint;
    //    public GameObject Target;

    //    // Start is called before the first frame update
    //    void Start()
    //    {
    //        Fireanimation = GetComponent<Animation>();
    //    }

    //    // Update is called once per frame
    //    void Update()
    //    {
    //        ARFiring();
    //    }

    //    private void ARFiring()
    //    {
    //        if (Input.GetMouseButton(0) && Time.time > fireTime)
    //        {
    //            Fireanimation.Play("fire");

    //            //Raycast Projectile
    //            RaycastHit hit;
    //            if (Physics.Raycast(firePoint.transform.position, -(firePoint.transform.position - Target.transform.position).normalized, out hit, 50.0f))
    //            {

    //                //Damage Enemies
    //                if (hit.transform.tag == "Enemy")
    //                {
    //                    hit.transform.GetComponent<Enemy>().takeDamage(damage);
    //                }

    //            }
    //;
    //        }
    //    }
}
