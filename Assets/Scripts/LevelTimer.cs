using UnityEngine;
using TMPro;
using System;

namespace LudumDare46
{
    public class LevelTimer : MonoBehaviour
    {
        [HideInInspector] public bool endGame;

        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI countdownTimer = default;
        [SerializeField] private TextMeshProUGUI levelTimer = default;
        [SerializeField] private GameObject waitEnterPanel = default;
        [SerializeField] private GameObject preparationTimePanel = default;
        //[Min(0f)]
        //[SerializeField] private float whenToChangeMusicSpeed = default;

        [Header("Debugging Purposes only")]
        [SerializeField] float baseTime = 10f;
        [SerializeField] float preparationTime = 30f;

        private bool triggeredLevelFinish;
        private bool levelStarted;
        private float pTime;

        // cached references

        public float elapsedTime { get; private set; }

        private void OnEnable()
        {
            pTime = preparationTime;
        }

        public void SetTimers(float gameTime, float gamePreparationTime)
        {
            baseTime = gameTime;
            preparationTime = gamePreparationTime;
        }

        // Update is called once per frame
        void Update()
        {
            if (triggeredLevelFinish) { return; }
            if (!levelStarted)
            {
                CountdownToStart();
            }
            else
            {
                UpdateLevelTimer();
            }
        }

        private void UpdateLevelTimer()
        {
            elapsedTime += 1 * Time.deltaTime;
            //slider.value = elapsedTime / baseTime;

            TimeSpan time = TimeSpan.FromSeconds(baseTime - elapsedTime);
            levelTimer.text = string.Format("{0:00}:{1:00}", time.Minutes, time.Seconds);
            //if ((baseTime - elapsedTime) > whenToChangeMusicSpeed)
            //{
            //    gameManager.changeMusicSpeed(1f);
            //}
            //else
            //{
            //    gameManager.changeMusicSpeed(1.2f);
            //}
            bool timerFinished = (elapsedTime >= baseTime);
            if (timerFinished || endGame)
            {
                // A chi comunico che ho finito?
                triggeredLevelFinish = true;
                FindObjectOfType<GameManager>().TriggeredTimerFinish();                
            }
        }

        private void CountdownToStart()
        {
            preparationTime -= 1 * Time.deltaTime;
            countdownTimer.text = preparationTime.ToString("00");
            if (preparationTime <= 0)
            {
                levelStarted = true;
                waitEnterPanel.SetActive(false);
                preparationTimePanel.SetActive(false);
                levelTimer.gameObject.SetActive(true);

                // A chi comunico che ho iniziato?
                FindObjectOfType<GameManager>().TriggeredTimerStart();
            }
        }


        private void OnDisable()
        {
            levelStarted = false;
            levelTimer.gameObject.SetActive(false);
            preparationTimePanel.SetActive(true);
            preparationTime = pTime;
            elapsedTime = 0f;
            triggeredLevelFinish = false;
        }
    }

}