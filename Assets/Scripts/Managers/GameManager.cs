using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LudumDare46
{

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance { get; private set; }
        public static bool pause { get; private set; }

        public int Stars { get; private set; }
        public float Score { get; private set; }

        [SerializeField] private LevelParametersConfig levelParms = default;

        [SerializeField] Sprite volumeOn = default;
        [SerializeField] Sprite volumeOff = default;

        LevelTimer levelTimer;
        Slider antSlider;
        Image volumeButton;

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
                DontDestroyOnLoad(this);
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

            GameObject antSlider_object = GameObject.Find("Total Ants Slider");
            antSlider = antSlider_object.GetComponent<Slider>();
            //antSlider_object.SetActive(false);

            GameObject statsPanel_object = GameObject.Find("Stats Panel");
            volumeButton = statsPanel_object.transform.Find("Options").Find("Audio Button").GetComponent<Image>();
            statsPanel_object.SetActive(false);

            StartCoroutine(WaitEnterAndStartTimer());

            //reset
            Stars = 0;
            Score = 0;
            currentAntsSpawned = 0;
            currentAntsKilled = 0;
            currentTrapsDisabled = 0;

            Resume();

            //set volume
            AudioListener.volume = PlayerPrefs.GetFloat("Volume", 1);
            SetVolumeImage();
        }

        IEnumerator WaitEnterAndStartTimer()
        {
            if (levelTimer != null)
            {
                levelTimer.gameObject.SetActive(false);
            }

            //wait enter
            while (!Input.GetKeyDown(KeyCode.Return))
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
            for (int i = 0; i < trapSpawners.Length; i++)
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

        private void UpdateAntSlider(float percentage)
        {
            antSlider.value = percentage;
        }

        private void CheckGameOver()
        {
            float potentiallyAlive = levelParms.AntsToSpawn - currentAntsKilled;
            Debug.Log("Potentially Alive: " + potentiallyAlive);
            Debug.Log("Percentage: " + potentiallyAlive / levelParms.AntsToSpawn);

            UpdateAntSlider(potentiallyAlive / levelParms.AntsToSpawn);

            if (potentiallyAlive / levelParms.AntsToSpawn < levelParms.AntsToSave)
            {
                Debug.Log("Game Over");
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

                if (remainedAnts / levelParms.AntsToSpawn >= levelParms.AntsForSecondStar)
                {
                    currentStars = 2;

                    if (currentTrapsDisabled >= levelParms.MinimumTrapsForThirdStar)
                    {
                        currentStars = 3;
                    }
                }
            }

            Score = levelTimer.elapsedTime * remainedAnts * currentTrapsDisabled;
            Stars = currentStars;

            LoadEndScene();
        }

        void LoadEndScene()
        {
            SceneManager.LoadScene("Ending Scene");
        }

        #region public API

        #region scene management

        public void RestartGame()
        {
            SceneManager.LoadScene("Main Scene");
        }

        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void Pause()
        {
            pause = true;

            Time.timeScale = 0;
        }

        public void Resume()
        {
            pause = false;

            Time.timeScale = 1;
        }

        public void Volume()
        {
            AudioListener.volume = AudioListener.volume == 1 ? 0 : 1;

            PlayerPrefs.SetFloat("Volume", AudioListener.volume);

            SetVolumeImage();
        }

        void SetVolumeImage()
        {
            if(AudioListener.volume == 1)
            {
                instance.volumeButton.sprite = volumeOn;
            }
            else
            {
                instance.volumeButton.sprite = volumeOff;
            }
        }

        #endregion

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