using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seraph : MonoBehaviour
{
    public int health;
    public int damage;
    private bool hasBeenHit = false;

    private Vector3 lookDir;
    private Vector3 lastPosition;
    private GameObject target;

    public GameObject[] eyes;
    public GameObject[] Innereyes;
    public GameObject[] firePoint;

    public Material charged;
    public Material cooldown;

    private Renderer[] eyeRend;

    private LayerMask seraph;

    [SerializeField] private bool isShooting = false;

    Enemy enemy;
    LineRenderer lineRenderer;
    PlayerScript player;
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        eyeRend = new Renderer[Innereyes.Length];

        for (int e = 0; e < eyes.Length; e++)
        {
            eyeRend[e] = Innereyes[e].GetComponent<Renderer>();
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

    }

    void FixedUpdate()
    {
        if (isShooting == false)
        {
            // Rotate Timmy to face cursor
            lookDir = target.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(-lookDir);
            transform.rotation = rotation;
        }
        else if (isShooting == true)
        {
            hasBeenHit = false;
            lastPosition = -lookDir;
            StartCoroutine(Shooting());
        }
    }

    IEnumerator Shooting()
    {
        for (int m = 0; m < eyeRend.Length; m++)
        {
            eyeRend[m].material = charged;
            yield return new WaitForSeconds(0.1f);
        }

        Shoot();
        isShooting = false;

        for (int j = 0; j < firePoint.Length; j++)
        {
            eyeRend[j].material = cooldown;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void Shoot()
    {
        for (int i = 0; i < firePoint.Length; i++)
        {
            lineRenderer.SetPosition(0, firePoint[i].transform.position);

            RaycastHit hit;
            if (Physics.Raycast(firePoint[i].transform.position, lastPosition, out hit, 500, ~1 << 7))
            {
                if (hit.transform.tag == "Player")
                {
                    lineRenderer.SetPosition(1, hit.point);
                    Debug.Log("Player hit");
                    if (hasBeenHit == false)
                    {
                        enemy.player.takedamage(enemy.damage);
                        hasBeenHit = true;
                    }
                }
                else
                {
                    lineRenderer.SetPosition(1, hit.point);
                }
            }
            StartCoroutine(ShootLaster());
        }
    }

    IEnumerator ShootingCooldown()
    {
        yield return new WaitForSeconds(1f);
        isShooting = true;
        StartCoroutine(ShootingCooldown());
    }

    IEnumerator ShootLaster()
    {
        lineRenderer.enabled = true;
        yield return null;
        //yield return new WaitForSeconds(0.2f);
        //lineRenderer.enabled = false;
    }
}
