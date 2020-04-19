using System.Collections;
using UnityEngine;

namespace LudumDare46
{
    public class Anthill : MonoBehaviour
    {
        [SerializeField] private SpawnerConfig waveConfig;
        [SerializeField] private GameObject targetDestination = default;

        [Header("Testing stuff - Values are controlled by the Level Manager")]
        [SerializeField] private bool looping = false;
        [SerializeField] private bool spawn = false;

        Pooling pool = new Pooling();

        // cached reference
        Coroutine spawnCoroutine;

        IEnumerator Start()
        {
            while (spawn)
            {
                spawnCoroutine = StartCoroutine(SpawnAllAntsInWave());
                yield return spawnCoroutine;
            }
            if(looping)
            {
                StartCoroutine(Start());
            }
        }

        IEnumerator SpawnAllAntsInWave()
        {
            Vector3 startPosition;
            float timeBetweenSpawns;
            float randomFactor;

            startPosition = transform.position;
            timeBetweenSpawns = waveConfig.TimeBetweenSpawns;
            randomFactor = Random.Range(-waveConfig.SpawnRandomFactor, waveConfig.SpawnRandomFactor);
            var antPrefab = waveConfig.AntPrefab;
            var numberOfAnts = waveConfig.NumberOfAnts;
            var antSpeed = waveConfig.AntSpeed;
            var target = targetDestination;
            for (int antCount = 0; antCount < numberOfAnts; antCount++)
            {
                var newAnt = pool.Instantiate(antPrefab, startPosition, Quaternion.identity);
                newAnt.GetComponent<Bug>().SetSpeed(antSpeed);
                newAnt.GetComponent<Bug>().SetTarget(target);
                newAnt.transform.parent = transform;
                yield return new WaitForSeconds(timeBetweenSpawns + randomFactor);
            }
        }

        public void StopSpawning()
        {
            looping = false;
            spawn = false;
            if(spawnCoroutine != null)
            {
                StopCoroutine(spawnCoroutine);
            }
        }

        public void StartSpawning(bool loop)
        {
            looping = loop;
            spawn = true;

            //start spawn
            StartCoroutine(Start());
        }

    }

}