using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefab;

    public int BatCount;
    public int MaxBat;

    // Start is called before the first frame update
    void Start()
    {
        MaxBat = 100;
        StartCoroutine(StartSceneWait());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator StartSceneWait()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(Bat());
    }

    IEnumerator Bat()
    {
        yield return new WaitForSeconds(0.1f);
        BatCount = GameObject.FindGameObjectsWithTag("LDem").Length;

        if (BatCount < MaxBat)
        {
            int randspawnpoint = Random.Range(0, spawnPoints.Length);
            var cloneBat = Instantiate(enemyPrefab[0], spawnPoints[randspawnpoint].position, Quaternion.identity);
        }
        StartCoroutine(Bat());
    }

}
