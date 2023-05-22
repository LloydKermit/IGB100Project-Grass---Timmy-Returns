using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archangel : MonoBehaviour
{
    public int health;
    public int damage;
    private bool hasBeenHit = false;

    private Vector3 lookDir;
    private Vector3 lastPosition;
    private GameObject target;
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
        yield return new WaitForSeconds(0.2f);
        Shoot();
        isShooting = false;
    }

    private void Shoot()
    {
        lineRenderer.SetPosition(0, transform.position);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, lastPosition, out hit))
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
