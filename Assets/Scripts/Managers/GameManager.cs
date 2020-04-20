using UnityEngine;
using System.Collections;

namespace LudumDare46
{

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private LevelParametersConfig levelParms = default;

        LevelTimer levelTimer;

        void Awake()
        {
            levelTimer = FindObjectOfType<LevelTimer>();

            StartCoroutine(WaitEnterAndStartTimer());
        }

        IEnumerator WaitEnterAndStartTimer()
        {
            levelTimer.gameObject.SetActive(false);

            //wait enter
            while(!Input.GetKeyDown(KeyCode.Return))
            {
                yield return null;
            }

            //set timer
            levelTimer.gameObject.SetActive(true);
            levelTimer.SetTimers(levelParms.LevelTime, levelParms.LevelPreparationTime);
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