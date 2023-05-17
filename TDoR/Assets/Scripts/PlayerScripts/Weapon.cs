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
    private PlayerScript playerScript;

    public int ARdamage = 25;
    public float ARrange = 100f;
    public float ARfireRate = 0.2f;

    [SerializeField] private GameObject Player;

    public Camera fpsCam;


    private float ARnextFire = 0f;

    // Start is called before the first frame update
    void Start()
    {
        ARdamage = 25;
        ARfireRate = 0.2f;

        playerScript = Player.GetComponent<PlayerScript>();
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

                //Bountiful Harvest
                if (playerScript.BountifulHarvest == true)
                {
                    playerScript.LifeLeach();
                }

                GameObject EnemyImpact = Instantiate(EnemyhitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(EnemyImpact, 1f);
            }
            else if (hit.transform.tag != "FenceBorder")
            {
                GameObject NonEnemyImpact = Instantiate(NonEnemyhitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(NonEnemyImpact, 1f);
            }
        }
    }

    public void SetWeaponDmg(int weaponDmgMod)
    {
        ARdamage += weaponDmgMod;
    }

    public void SetWeaponFireRate(float weaponFireRateMod)
    {
        ARfireRate /= weaponFireRateMod;
    }
}
