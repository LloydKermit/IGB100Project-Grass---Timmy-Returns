using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GDemSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefab;

    public int GDemCount;
    public int MaxGDem;

    // Start is called before the first frame update
    void Start()
    {
        MaxGDem = 10;
        StartCoroutine(LDemSpawn());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator LDemSpawn()
    {
        yield return new WaitForSeconds(2.5f);
        GDemCount = GameObject.FindGameObjectsWithTag("GDem").Length;

        if (GDemCount < MaxGDem)
        {
            int randspawnpoint = Random.Range(0, spawnPoints.Length);
            Instantiate(enemyPrefab[0], spawnPoints[randspawnpoint].position, Quaternion.identity);
        }
        StartCoroutine(LDemSpawn());
    }

}
