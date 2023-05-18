using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelSpawner : MonoBehaviour
{
    public Transform bossSpawn;
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefab;

    WaveText waveText;

    public int AngelCount;
    public int MaxAngel;

    // Start is called before the first frame update
    void Start()
    {
        MaxAngel = 10;
        WinLose.AngelsLeft = MaxAngel;

        waveText = GameObject.Find("GameController").GetComponent<WaveText>();
    }

    // Update is called once per frame
    void Update()
    {
        if (WinLose.WavesCount == 7)
        {
            var boss = Instantiate(enemyPrefab[1], bossSpawn.position, Quaternion.Euler(new Vector3(180, 0, 0)));
        }
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
            var cloneAngel = Instantiate(enemyPrefab[0], spawnPoints[randspawnpoint].position, Quaternion.Euler(new Vector3(180, 0, 0 )));

            AngelCount += 1;
        }

        Coroutine Angels = StartCoroutine(Angel());

        if (WinLose.WavesCount < 7)
        {
            if (WinLose.AngelsKilled == WinLose.AngelsLeft)
            {
                StopCoroutine(Angels);
                AngelCount = 0;

                waveText.WaveDone();

                MaxAngel = 10 + (10 * (WinLose.WavesCount) / 5);
                WinLose.AngelsLeft = MaxAngel;
                WinLose.AngelsKilled = 0;
            }
        }
        else if (WinLose.WavesCount == 7)
        {
            if (WinLose.BossDead == true)
            {
                waveText.WaveDone();
            }
        }
    }
}
