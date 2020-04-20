using System;
using UnityEngine;

namespace LudumDare46
{

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private LevelParametersConfig levelParms = default;

        public Action OnAntSpawned;
        public Action OnAntKilled;
        public Action OnTrapDisabled;

        private int currentAntsSpawned = 0;
        private int currentAntsKilled = 0;
        private int currentTrapsDisabled = 0;

        private void AntKilled()
        {
            currentAntsKilled++;
            Debug.Log(string.Format("Ant Killed - {0} ants still alive, {1} to spawn", currentAntsSpawned - currentAntsKilled, levelParms.AntsToSpawn - currentAntsSpawned));
            CheckGameOver();
        }

        private void CheckGameOver()
        {
            float potentiallyAlive = levelParms.AntsToSpawn - currentAntsKilled;
            Debug.Log("Potentially Alive: " + potentiallyAlive);
            Debug.Log("Percentage: " + potentiallyAlive / levelParms.AntsToSpawn);
            if (potentiallyAlive / levelParms.AntsToSpawn < levelParms.AntsToSave)
            {
                Debug.Log("Mori");
            }
        }

        private void AntSpawned()
        {
            currentAntsSpawned++;
            Debug.Log(string.Format("Ant Spawned - {0} ants alive, {1} to spawn", currentAntsSpawned, levelParms.AntsToSpawn - currentAntsSpawned));
            if (currentAntsSpawned >= levelParms.AntsToSpawn)
            {
                StopAnthills();
            }
        }

        private void TrapDisabled()
        {
            currentTrapsDisabled++;
            Debug.Log("Trap Disabled!");
        }


        void Awake()
        {
            FindObjectOfType<LevelTimer>().SetTimers(levelParms.LevelTime, levelParms.LevelPreparationTime);
            OnAntSpawned += AntSpawned;
            OnAntKilled += AntKilled;
            OnTrapDisabled += TrapDisabled;
        }

        private void StartSpawners()
        {
            var anthillsArray = FindObjectsOfType<Anthill>();
            if (anthillsArray.Length > 0)
            {
                foreach (Anthill anthill in anthillsArray)
                {
                    anthill.StartSpawning(levelParms.Loop);
                }
            }

            var trapSpawners = FindObjectsOfType<TrapSpawner>();
            for(int i = 0; i < trapSpawners.Length; i++)
            {
                trapSpawners[i].StartSpawning();
            }
        }

        private void StopSpawners()
        {
            var anthHillsArray = FindObjectsOfType<Anthill>();
            foreach (Anthill anthill in anthHillsArray)
            {
                anthill.StopSpawning();
            }

            var trapSpawners = FindObjectsOfType<TrapSpawner>();
            for (int i = 0; i < trapSpawners.Length; i++)
            {
                trapSpawners[i].StopSpawning();
            }
        }

        private void StopAnthills()
        {
            var anthHillsArray = FindObjectsOfType<Anthill>();
            foreach (Anthill anthill in anthHillsArray)
            {
                anthill.StopSpawning();
            }
        }

        public void TriggeredTimerFinish()
        {
            StopSpawners();

        }

        public void TriggeredTimerStart()
        {
            StartSpawners();
        }
    }

}