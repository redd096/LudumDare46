using UnityEngine;

namespace LudumDare46
{

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private LevelParametersConfig levelParms = default;

        void Awake()
        {
            FindObjectOfType<LevelTimer>().SetTimers(levelParms.LevelTime, levelParms.LevelPreparationTime);
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
        }

        private void StopSpawners()
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