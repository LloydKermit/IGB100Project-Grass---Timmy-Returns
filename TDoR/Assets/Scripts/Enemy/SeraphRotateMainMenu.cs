using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeraphRotateMainMenu : MonoBehaviour
{
    private Vector3 lookDir;
    private GameObject target;
    public GameObject[] eyes;

    // Start is called before the first frame update
    void Start()
    {
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
        for(int i = 0; i < eyes.Length; i++)
            {
            // Rotate Timmy to face cursor
            lookDir = target.transform.position - eyes[i].transform.position;
            Quaternion rotation = Quaternion.LookRotation(-lookDir);
            eyes[i].transform.rotation = rotation;
        }
    }
}
