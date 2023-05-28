using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    Animation SpawnBeam;
    private bool animationPlayed;

    // Start is called before the first frame update
    void Start()
    {
        SpawnBeam = GetComponent<Animation>();
        animationPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (OuterTrigger.gateClosed == true && this.tag == "NormalAngelBeam")
        {
            Summon();
        }
        else if (OuterTrigger.gateClosed == false)
        {
            WaveEnd();
        }
    }

    public void Summon()
    {
        this.GetComponent<MeshRenderer>().enabled = true;
        if (animationPlayed == false)
        {
            SpawnBeam.Play("SpawnBeamAni");
            animationPlayed = true;
        }
    }

    public IEnumerator SummonArch()
    {
        Debug.Log("Parent");
        this.GetComponent<MeshRenderer>().enabled = true;
        if (animationPlayed == false)
        {
            SpawnBeam.Play("SpawnBeamAni");
            yield return new WaitForSeconds(1f);
            animationPlayed = true;
            this.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void WaveEnd()
    {
        this.GetComponent<MeshRenderer>().enabled = false;
        animationPlayed = false;
    }
}
