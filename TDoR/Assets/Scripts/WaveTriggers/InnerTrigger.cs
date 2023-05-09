using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerTrigger : MonoBehaviour
{
    public GameObject Inner;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (OuterTrigger.gateClosed == false)
        {
            if (collision.tag == "Player")
            {
                Inner.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}
