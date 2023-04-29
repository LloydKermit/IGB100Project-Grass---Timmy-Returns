using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    bool gateClosed;
    public GameObject Gate;
    public GameObject Spawners;

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
        angelSpawner = Spawners.GetComponent<AngelSpawner>();
        
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

                var fenceBorder = GameObject.FindGameObjectsWithTag("FenceBorder");
                for (var i = 0; i < fenceBorder.Length; i++)
                {
                    fenceBorder[i].GetComponent<BoxCollider>().enabled = true; 
                }

                StartCoroutine(angelSpawner.StartSceneWait());

                var triggers = GameObject.FindGameObjectsWithTag("Trigger");
                for (var i = 0; i < triggers.Length; i++)
                {
                    triggers[i].GetComponent<BoxCollider>().enabled = false;
                }
            }
        }
    }

}
