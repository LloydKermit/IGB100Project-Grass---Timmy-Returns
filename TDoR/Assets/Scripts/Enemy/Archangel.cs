using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archangel : MonoBehaviour
{
    private bool hasBeenHit = false;

    private Vector3 lookDir;
    private Vector3 lastPosition;
    private GameObject target;

    public GameObject Staff;
    public GameObject firePoint;

    public Material charged;
    public Material cooldown;

    private Renderer staffRend;

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
        StartCoroutine(ShootingCooldown());

        staffRend = Staff.GetComponent<Renderer>();
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
            lookDir = target.transform.position - firePoint.transform.position;
            Quaternion rotation = Quaternion.LookRotation(lookDir);
            transform.rotation = rotation;
        }
        else
        {
            hasBeenHit = false;
            lastPosition = lookDir;
            StartCoroutine(Shooting());
        }
    }

    IEnumerator Shooting()
    {
        staffRend.material = charged;
        yield return new WaitForSeconds(0.3f);
        Shoot();
        isShooting = false;
        yield return new WaitForSeconds(0.5f);
        staffRend.material = cooldown;
    }

    private void Shoot()
    {
        lineRenderer.SetPosition(0, firePoint.transform.position);

        RaycastHit hit;
        if (Physics.Raycast(firePoint.transform.position, lastPosition, out hit))
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

    IEnumerator ShootingCooldown()
    {
        yield return new WaitForSeconds(3f);
        isShooting = true;
        StartCoroutine(ShootingCooldown());
    }

    IEnumerator ShootLaster()
    {
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.2f);
        lineRenderer.enabled = false;
    }
}
