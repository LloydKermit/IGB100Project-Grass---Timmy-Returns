using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    bool gateClosed;
    public GameObject Gate;

    MeshRenderer gateMesh;
    MeshCollider gateCollider;

    AngelSpawner angelSpawner;

    private void Awake()
    {
        gateClosed = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        gateMesh = Gate.GetComponent<MeshRenderer>();
        gateCollider = Gate.GetComponent<MeshCollider>();
        angelSpawner = Gate.GetComponent<AngelSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (gateClosed == false)
        {
            if (collision.tag == "Player")
            {
                gateClosed = true;
                gateMesh.enabled = true;
                gateCollider.enabled = true;                
            }
        }
    }

}
