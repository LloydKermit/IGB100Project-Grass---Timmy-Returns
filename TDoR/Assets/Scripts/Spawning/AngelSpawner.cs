using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefab;

    public int AngelCount;
    public int MaxAngel;

    // Start is called before the first frame update
    void Start()
    {
        MaxAngel = 100;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator StartSceneWait()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(Angel());
    }

    public IEnumerator Angel()
    {
        yield return new WaitForSeconds(1f);
        AngelCount = GameObject.FindGameObjectsWithTag("Angel").Length;

        if (AngelCount < MaxAngel)
        {
            int randspawnpoint = Random.Range(0, spawnPoints.Length);
            var cloneBat = Instantiate(enemyPrefab[0], spawnPoints[randspawnpoint].position, Quaternion.identity);
        }
        StartCoroutine(Angel());
    }

}
