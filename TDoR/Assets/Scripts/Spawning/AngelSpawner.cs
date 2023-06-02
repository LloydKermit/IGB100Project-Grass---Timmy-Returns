using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class AngelSpawner : MonoBehaviour
{
    public Transform bossSpawn;
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefab;

    WaveText waveText;

    public bool ArchSpawning = false;
    private int archCount = 0;
    public int maxArch;
    public int AngelCount;
    public int MaxAngel;
    private int bossCount = 0;
    List<int> RanSpawn = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        MaxAngel = 5;
        WinLose.AngelsLeft = MaxAngel;
        maxArch = 0;

        waveText = GameObject.Find("GameController").GetComponent<WaveText>();
    }

    // Update is called once per frame
    void Update()
    {
        if (WinLose.WavesCount == 7 && bossCount == 0)
        {
            bossCount++;
            StartCoroutine(SpawnSeraph());
        }
    }

    public IEnumerator SpawnSeraph()
    {
        var Beam = bossSpawn.GetComponentInParent<Beam>();
        Beam.StartCoroutine(Beam.SummonSeraph());
        yield return new WaitForSeconds(1);

        var boss = Instantiate(enemyPrefab[1], bossSpawn.position, Quaternion.Euler(new Vector3(0, 180, 0)));
    }

    public IEnumerator SpawnArch()
    {
        while (archCount < maxArch)
        {
            int randomIndex = Random.Range(9, 14);
            var Beam = spawnPoints[randomIndex].GetComponentInParent<Beam>();

            if (!RanSpawn.Contains(randomIndex))
            {
                archCount += 1;
                Debug.Log(archCount);

                RanSpawn.Add(randomIndex);

                Beam.StartCoroutine(Beam.SummonArch());
                yield return new WaitForSeconds(1);
                Debug.Log("Beam");

                var cloneArch = Instantiate(enemyPrefab[2], spawnPoints[randomIndex].position, Quaternion.Euler(new Vector3(180, 0, 0)));
                Debug.Log("Spawned");

                yield return new WaitForSeconds(1);

                if (archCount >= maxArch || WinLose.AngelsKilled >= WinLose.AngelsLeft + maxArch)
                    break;
            }
        }

        if (archCount >= maxArch || WinLose.AngelsKilled >= WinLose.AngelsLeft + maxArch)
        {
            archCount = 0;
        }
    }
        public IEnumerator StartSceneWaitArch()
    {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(SpawnArch());
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
            int randspawnpoint = Random.Range(0, 8);
            var cloneAngel = Instantiate(enemyPrefab[0], spawnPoints[randspawnpoint].position, Quaternion.Euler(new Vector3(180, 0, 0 )));

            AngelCount += 1;
        }

        Coroutine Angels = StartCoroutine(Angel());

        if (WinLose.WavesCount < 7)
        {
            if (WinLose.AngelsKilled >= WinLose.AngelsLeft + maxArch)
            {
                StopCoroutine(Angels);
                AngelCount = 0;
                archCount = 0;
                RanSpawn.Clear();

                waveText.WaveDone();

                MaxAngel = 5 + (4 * WinLose.WavesCount);
                WinLose.AngelsLeft = MaxAngel;
                WinLose.AngelsKilled = 0;
                maxArch = WinLose.WavesCount - 1;
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
