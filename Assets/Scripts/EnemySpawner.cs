using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfig;
    [SerializeField] float timeBetweenWaves = 1f;
    [SerializeField] float timeReducer = 0.5f;
    [SerializeField] int acceleratorScore = 0;
    [SerializeField] int bossScore= 0;
    [SerializeField] bool isLooping;

    bool canFast = true;

    WaveConfigSO currentWave;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    void Update()
    {
        MakeItFaster();

        if (timeBetweenWaves == 0 && scoreKeeper.GetScore() % bossScore==0)
        {

        }
    }

    void MakeItFaster()
    {
        if (scoreKeeper.GetScore() % acceleratorScore == 0 && canFast)
        {
            timeBetweenWaves -= timeReducer;
            canFast = false;
        }

        else if (scoreKeeper.GetScore() % acceleratorScore != 0)
        {
            canFast = true;
        }

        if (timeBetweenWaves < 0)
        {
            timeBetweenWaves = 0;
        }
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }

    IEnumerator SpawnEnemies()
    {
        do
        {
            foreach (WaveConfigSO wave in waveConfig)
            {
                currentWave = wave;

                for (int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    Instantiate(currentWave.GetEnemyPrefab(i),
                                currentWave.GetStartingWaypoint().position,
                                Quaternion.Euler(0, 0, 180),
                                transform);

                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }

                if (scoreKeeper.GetScore() % acceleratorScore == 0)
                {
                    timeBetweenWaves -= timeReducer;
                }

                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }
        while (isLooping);
    }
}
