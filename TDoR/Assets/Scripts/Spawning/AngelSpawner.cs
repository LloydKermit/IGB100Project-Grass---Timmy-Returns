using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefab;

    WaveText waveText;

    public int AngelCount;
    public int MaxAngel;

    // Start is called before the first frame update
    void Start()
    {
        MaxAngel = 20;
        WinLose.AngelsLeft = MaxAngel;

        waveText = GameObject.Find("GameController").GetComponent<WaveText>();
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
        yield return new WaitForSeconds(0.7f);
        //AngelCount = GameObject.FindGameObjectsWithTag("Angel").Length;

        if (AngelCount < MaxAngel)
        {
            int randspawnpoint = Random.Range(0, spawnPoints.Length);
            var cloneBat = Instantiate(enemyPrefab[0], spawnPoints[randspawnpoint].position, Quaternion.identity);

            AngelCount += 1;
        }

        Coroutine Angels = StartCoroutine(Angel());

        if (WinLose.AngelsKilled == WinLose.AngelsLeft)
        {
            StopCoroutine(Angels);
            AngelCount = 0;

            waveText.WaveDone();

            MaxAngel = 20 + (10 * (WinLose.WavesCount) / 5);
            WinLose.AngelsLeft = MaxAngel;
            WinLose.AngelsKilled = 0;
        }
    }

}
