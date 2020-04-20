using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace LudumDare46
{

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public int stars { get; private set; }
        public float score { get; private set; }

        [SerializeField] private LevelParametersConfig levelParms = default;

        LevelTimer levelTimer;

        private int currentAntsSpawned = 0;
        private int currentAntsKilled = 0;
        private int currentTrapsDisabled = 0;

        #region start

        void Awake()
        {
            CheckInstance();
        }

        void CheckInstance()
        {
            if (instance == null)
            {
                //if no instance, this is the instance and set default values
                instance = this;
                SetDefaults();
            }
            else
            {
                //if there is an instance, set its default values and destroy this one
                instance.SetDefaults();
                Destroy(this.gameObject);
            }
        }

        void SetDefaults()
        {
            levelTimer = FindObjectOfType<LevelTimer>();

            StartCoroutine(WaitEnterAndStartTimer());

            //reset
            stars = 0;
            score = 0;
            currentAntsSpawned = 0;
            currentAntsKilled = 0;
            currentTrapsDisabled = 0;
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

        #endregion

        #region spawner

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

        #endregion

        private void CheckGameOver()
        {
            float potentiallyAlive = levelParms.AntsToSpawn - currentAntsKilled;
            Debug.Log("Potentially Alive: " + potentiallyAlive);
            Debug.Log("Percentage: " + potentiallyAlive / levelParms.AntsToSpawn);
            if (potentiallyAlive / levelParms.AntsToSpawn < levelParms.AntsToSave)
            {
                GameOver(false);
            }
        }

        void GameOver(bool win)
        {
            int remainedAnts = currentAntsSpawned - currentAntsKilled;
            int currentStars = 0;

            if (win)
            {
                currentStars = 1;

                if(remainedAnts >= levelParms.AntsForSecondStar)
                {
                    currentStars = 2;

                    if(currentTrapsDisabled >= levelParms.MinimumTrapsForThirdStar)
                    {
                        currentStars = 3;
                    }
                }
            }

            score = levelTimer.elapsedTime * remainedAnts * currentTrapsDisabled;
            stars = currentStars;

            LoadEndScene();
        }

        void LoadEndScene()
        {
            SceneManager.LoadScene("Ending Scene");
        }

        #region public API

        public void TriggeredTimerFinish()
        {
            StopSpawners();

            GameOver(true);
        }

        public void TriggeredTimerStart()
        {
            StartSpawners();
        }



        public void AntKilled()
        {
            currentAntsKilled++;
            Debug.Log(string.Format("Ant Killed - {0} ants still alive, {1} to spawn", currentAntsSpawned - currentAntsKilled, levelParms.AntsToSpawn - currentAntsSpawned));
            
            CheckGameOver();
        }

        public void AntSpawned()
        {
            currentAntsSpawned++;
            Debug.Log(string.Format("Ant Spawned - {0} ants alive, {1} to spawn", currentAntsSpawned, levelParms.AntsToSpawn - currentAntsSpawned));

            if (currentAntsSpawned >= levelParms.AntsToSpawn)
            {
                StopAnthills();
            }
        }

        public void TrapDisabled()
        {
            currentTrapsDisabled++;
            Debug.Log("Trap Disabled!");
        }

        #endregion
    }

}