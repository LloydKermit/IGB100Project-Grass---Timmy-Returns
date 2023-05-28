using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelSpawner : MonoBehaviour
{
    public Transform bossSpawn;
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefab;

    WaveText waveText;

    public bool ArchSpawning = false;
    private int archCount = 0;
    private int maxArch;
    public int AngelCount;
    public int MaxAngel;
    private int bossCount = 0;
    List<int> RanSpawn = new List<int>();

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
        maxArch = WinLose.WavesCount - 2;

        if (WinLose.WavesCount == 7 && bossCount == 0)
        {
            var boss = Instantiate(enemyPrefab[1], bossSpawn.position, Quaternion.Euler(new Vector3(180, 0, 0)));
            bossCount++;
        }
    }

    public IEnumerator SpawnArch()
    {
        Debug.Log("Hello");
        yield return new WaitForSeconds(1.5f);

        if (archCount < maxArch)
        {
            int randomIndex = Random.Range(9, 14);
            var Beam = spawnPoints[randomIndex].GetComponentInParent<Beam>();

            if (!RanSpawn.Contains(randomIndex))
            {
                AngelCount++;
                archCount++;
                Debug.Log(archCount);

                RanSpawn.Add(randomIndex);

                Beam.StartCoroutine(Beam.SummonArch());
                yield return new WaitForSeconds(1);
                var cloneArch = Instantiate(enemyPrefab[2], spawnPoints[randomIndex].position, Quaternion.Euler(new Vector3(180, 0, 0)));
            }
        }

        Coroutine ArchSpawn = StartCoroutine(SpawnArch());

        if (archCount == maxArch)
        {
            StopCoroutine(ArchSpawn);
            RanSpawn.Clear();
            archCount = 0;
        }
    }

    //private IEnumerator SpawnArch()
    //{
    //    while (RanSpawn.Count < WinLose.WavesCount - 2 && WinLose.WavesCount >= 3)
    //    {
    //        int randomIndex = Random.Range(9, 14);

    //        if (!RanSpawn.Contains(randomIndex))
    //        {
    //            RanSpawn.Add(randomIndex);
    //        }
    //    }

    //    if (archCount < maxArch)
    //    {
    //        archCount++;
    //        AngelCount += 1;

    //        Debug.Log(RanSpawn[archCount]);
    //        var Beam = spawnPoints[RanSpawn[archCount]].GetComponentInParent<Beam>();
    //        Beam.StartCoroutine(Beam.SummonArch());
    //        var cloneArch = Instantiate(enemyPrefab[2], spawnPoints[RanSpawn[archCount]].position, Quaternion.Euler(new Vector3(180, 0, 0)));
    //        yield return new WaitForSeconds(1);
    //    }

    //    ArchSpawned = true;

    //}

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
            int randspawnpoint = Random.Range(0, 8);
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
                archCount = 0;
                RanSpawn.Clear();

                waveText.WaveDone();

                MaxAngel = 10 + (5 * WinLose.WavesCount);
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
