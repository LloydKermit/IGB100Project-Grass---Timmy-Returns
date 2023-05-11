using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTrigger1 : MonoBehaviour
{
    public GameObject trigger1;
    public GameObject trigger2;

    // Start is called before the first frame update
    void Start()
    {
        trigger1.SetActive(false);
        trigger2.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            trigger1.SetActive(true);
            trigger2.SetActive(true);

        }
    }
}
