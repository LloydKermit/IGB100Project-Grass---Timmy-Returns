using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class SeraphRotate : MonoBehaviour
{
    private bool hasBeenHit = false;
    private Vector3 lastPosition;
    private Vector3 lookDir;
    private Vector3 initialPlayerPosition;
    private GameObject target;
    public GameObject[] Innereyes;
    public GameObject[] eyes;
    public GameObject[] firePoint;
    private Renderer[] eyeRend;
    public Material charged;
    public Material cooldown;

    Enemy enemy;
    LineRenderer[] lineRenderer;
    PlayerScript player;

    [SerializeField] private bool isShooting = false;

    // Start is called before the first frame update
    void Start()
    {
        eyeRend = new Renderer[Innereyes.Length];
        lineRenderer = new LineRenderer[Innereyes.Length];

        for (int i = 0; i < Innereyes.Length; i++)
        {
            eyeRend[i] = Innereyes[i].GetComponent<Renderer>();
            lineRenderer[i] = Innereyes[i].GetComponent<LineRenderer>();
        }

        StartCoroutine(ShootingCooldown());

        enemy = GetComponent<Enemy>();
        player = GameObject.Find("PlayerCapsule").GetComponent<PlayerScript>();

        //Player Reference exception catch
        try
        {
            target = GameObject.FindGameObjectWithTag("RayTarget");
        }
        catch
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isShooting == false)
        {
            for (int i = 0; i < eyes.Length; i++)
            {
                // Rotate Timmy to face cursor
                lookDir = target.transform.position - eyes[i].transform.position;
                Quaternion rotation = Quaternion.LookRotation(-lookDir);
                eyes[i].transform.rotation = rotation;
            }
        }
        else
        {
            hasBeenHit = false;
            initialPlayerPosition = target.transform.position;
            StartCoroutine(Shooting());
        }

        Debug.DrawRay(firePoint[0].transform.position, target.transform.position, Color.red);
        Debug.Log("lookDir: " + lookDir);
    }

    IEnumerator Shooting()
    {
        for (int i = 0; i < eyeRend.Length; i++)
        {
            eyeRend[i].material = charged;
            yield return new WaitForSeconds(0.1f);
        }

        StartCoroutine(Shoot());
        isShooting = false;

        for (int i = 0; i < eyeRend.Length; i++)
        {
            eyeRend[i].material = cooldown;
        }

        StartCoroutine(ShootingCooldown());
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < firePoint.Length; i++)
        {
            Vector3 direction = initialPlayerPosition - firePoint[i].transform.position;
            lastPosition = direction.normalized;

            lineRenderer[i].SetPosition(0, firePoint[i].transform.position);

            RaycastHit hit;
            if (Physics.Raycast(firePoint[i].transform.position, lastPosition, out hit, Mathf.Infinity, ~1 << 7))
            {
                if (hit.transform.tag == "Player")
                {
                    lineRenderer[i].SetPosition(1, hit.point);
                    Debug.Log("Player hit");
                    if (hasBeenHit == false)
                    {
                        enemy.player.takedamage(enemy.damage);
                        hasBeenHit = true;
                    }
                }
                else
                {
                    lineRenderer[i].SetPosition(1, hit.point);
                }
            }
            else
            {
                lineRenderer[i].SetPosition(1, firePoint[i].transform.position + lastPosition * 500);
            }

            lineRenderer[i].enabled = true;
        }

        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < firePoint.Length; i++)
        {
            lineRenderer[i].enabled = false;
        }
    }

    IEnumerator ShootingCooldown()
    {
        yield return new WaitForSeconds(5f);
        isShooting = true;
    }
}
