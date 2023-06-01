using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public AudioSource _audiosource;

    // Start is called before the first frame update
    void Start()
    {
        _audiosource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
