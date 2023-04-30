using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class OuterTrigger : MonoBehaviour
{
    public static bool gateClosed;
    public GameObject Gate;
    public GameObject GateBorder;
    public GameObject Spawners;
    public GameObject InnerTrig;

    BoxCollider gateBCollide;
    MeshRenderer gateMesh;
    MeshCollider gateCollider;

    WaveText waveText;
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
        gateBCollide = GateBorder.GetComponent<BoxCollider>();
        InnerTrig = GameObject.FindGameObjectWithTag("InnerTrigger");

        waveText = GameObject.Find("GameController").GetComponent<WaveText>();

    }

    // Update is called once per frame
    void Update()
    {
        if (WinLose.AngelsKilled == WinLose.AngelsLeft)
        {
            gateClosed = false;
            gateMesh.enabled = false;
            gateCollider.enabled = false;
            gateBCollide.enabled = false;

        }

        if (InnerTrig.GetComponent<BoxCollider>().enabled == false)
        {
            var triggers = GameObject.FindGameObjectsWithTag("Trigger");
            for (var i = 0; i < triggers.Length; i++)
            {
                triggers[i].GetComponent<BoxCollider>().enabled = true;
            }
        }
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
                gateBCollide.enabled = true;

                //var fenceBorder = GameObject.FindGameObjectsWithTag("FenceBorder");
                //for (var i = 0; i < fenceBorder.Length; i++)
                //{
                //    fenceBorder[i].GetComponent<BoxCollider>().enabled = true; 
                //}

                Coroutine Angels = StartCoroutine(angelSpawner.StartSceneWait());

                var triggers = GameObject.FindGameObjectsWithTag("Trigger");
                for (var i = 0; i < triggers.Length; i++)
                {
                    triggers[i].GetComponent<BoxCollider>().enabled = false;
                }

                InnerTrig.GetComponent<BoxCollider>().enabled = true;

                WinLose.WavesCount += 1;

                waveText.NextWave();
                waveText.AngelsLeft();

            }
        }
    }

}
