using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class OuterTrigger : MonoBehaviour
{
    public static bool gateClosed;
    public GameObject Gate;
    public GameObject Spawners;
    //public GameObject InnerTrig;
    public GameObject GateBorder;

    public AudioSource _SFX;

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
        gateBCollide = GateBorder.GetComponent<BoxCollider>();
        gateMesh = Gate.GetComponent<MeshRenderer>();
        gateCollider = Gate.GetComponent<MeshCollider>();
        angelSpawner = Spawners.GetComponent<AngelSpawner>();
        //InnerTrig = GameObject.FindGameObjectWithTag("InnerTrigger");

        waveText = GameObject.Find("GameController").GetComponent<WaveText>();

    }

    // Update is called once per frame
    void Update()
    {
        if (WinLose.AngelsKilled >= WinLose.AngelsLeft + angelSpawner.maxArch)
        {
            gateClosed = false;
            gateMesh.enabled = false;
            gateCollider.enabled = false;
            gateBCollide.enabled = false;
        }

        if (WinLose.hasInteracted == true)
        {
            var triggers = GameObject.FindGameObjectsWithTag("Trigger");
            for (var i = 0; i < triggers.Length; i++)
            {
                triggers[i].GetComponent<BoxCollider>().enabled = true;
            }
        }
        else if (WinLose.hasInteracted == false)
        {
            var triggers = GameObject.FindGameObjectsWithTag("Trigger");
            for (var i = 0; i < triggers.Length; i++)
            {
                triggers[i].GetComponent<BoxCollider>().enabled = false;
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
                WinLose.hasInteracted = false;

                _SFX.Play();

                //var Beams = GameObject.FindGameObjectsWithTag("Beam");
                //for (var i = 0; i < Beams.Length; i++)
                //{
                //    Beams[i].GetComponent<MeshRenderer>().enabled = true;
                //}

                Coroutine Angels = StartCoroutine(angelSpawner.StartSceneWait());

                Coroutine ArchSpawn = StartCoroutine(angelSpawner.StartSceneWaitArch());

                var triggers = GameObject.FindGameObjectsWithTag("Trigger");
                for (var i = 0; i < triggers.Length; i++)
                {
                    triggers[i].GetComponent<BoxCollider>().enabled = false;
                }

                //InnerTrig.GetComponent<BoxCollider>().enabled = true;

                WinLose.WavesCount += 1;

                waveText.NextWave();
                waveText.AngelsLeft();

            }
        }
    }

}
