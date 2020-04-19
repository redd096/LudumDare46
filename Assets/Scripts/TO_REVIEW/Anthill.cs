using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare46
{
    public class Anthill : MonoBehaviour
    {
        [SerializeField] private SpawnerConfig waveConfig;
        //[SerializeField] private Target targetDestination = default;
        //[SerializeField] private float pauseBetweenTargetHops;

        /// <summary>
        /// Time in seconds after which the spawning starts
        /// </summary>
        [SerializeField] private float delayToStart = 0f;

        [Header("Testing stuff - Values are controlled by the Level Manager")]
        [SerializeField] private bool looping = false;
        [SerializeField] private bool spawn = false;

        //List<Bug> antList = new List<Bug>();

        Pooling pool = new Pooling();

        // cached reference
        Coroutine spawnCoroutine;
        //Coroutine targetCoroutine;


        IEnumerator Start()
        {
            while (spawn)
            {
                yield return new WaitForSeconds(delayToStart);
                spawnCoroutine = StartCoroutine(SpawnAllAntsInWave());
                //targetCoroutine = StartCoroutine(ChangeDestination());
                while (spawnCoroutine != null)
                    yield return null;
            }
            if (looping)
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
            //var target = targetDestination;
            for (int antCount = 0; antCount < numberOfAnts; antCount++)
            {
                //var newAnt = pool.Instantiate(antPrefab, startPosition, Quaternion.identity);
                var newAnt = Instantiate(antPrefab, startPosition, Quaternion.identity);
                var bugComponent = newAnt.GetComponent<Bug>();
                //antList.Add(bugComponent);
                bugComponent.SetSpeed(antSpeed);
                //bugComponent.SetTarget(target.transform);
                newAnt.transform.parent = transform;
                yield return new WaitForSeconds(timeBetweenSpawns + randomFactor);
            }
            spawnCoroutine = null;
        }


        public void StopSpawning()
        {
            looping = false;
            spawn = false;
            if (spawnCoroutine != null)
            {
                StopCoroutine(spawnCoroutine);
            }
            //if (targetCoroutine != null)
            //{
            //    StopCoroutine(targetCoroutine);
            //}
        }


        //private void OnEnable()
        //{
        //    targetDestination.OnTeleport += OnTargetTeleport;
        //}

        public void StartSpawning(bool loop)
        {
            looping = loop;
            spawn = true;

            //start spawn
            StartCoroutine(Start());
        }

        //public void OnTargetTeleport(Target newTarget)
        //{
        //    targetDestination = newTarget;
        //    foreach (var bug in antList)
        //    {
        //        bug.SetTarget(targetDestination.transform);
        //    }
        //}

        //private void OnDisable()
        //{
        //    targetDestination.OnTeleport -= OnTargetTeleport;
        //}

    }

}