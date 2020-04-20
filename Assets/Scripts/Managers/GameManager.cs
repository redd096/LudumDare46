using System;
using UnityEngine;

namespace LudumDare46
{

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private LevelParametersConfig levelParms = default;

        public Action OnAntSpawned;
        public Action OnAntKilled;

        private int currentAntsSpawned = 0;
        private int currentAntsKilled = 0;

        private void AntKilled()
        {
            currentAntsKilled++;
            CheckGameOver();
        }

        private void CheckGameOver()
        {
            //if(levelParms.AntsToSpawn - currentAntsSpawned  levelParms.AntsToSave ;
        }

        private void AntSpawned()
        {
            currentAntsSpawned++;
        }

        void Awake()
        {
            FindObjectOfType<LevelTimer>().SetTimers(levelParms.LevelTime, levelParms.LevelPreparationTime);
            OnAntSpawned = AntSpawned;
            OnAntKilled = AntKilled;
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