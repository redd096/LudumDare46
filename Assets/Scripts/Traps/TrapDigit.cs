using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare46
{
    public class TrapDigit : Trap
    {
        [Header("Debug Digit")]
        [SerializeField] Keys[] keysToDisable = default;

        int keysIndex;

        protected override void Update()
        {
            //if pressed key
            if (Input.GetKeyDown(keysToDisable[keysIndex].ToString()))
            {
                keysIndex++;

                //check if dead
                if (keysIndex >= keysToDisable.Length)
                {
                    Destroy(gameObject);
                    return;
                }

                UpdateUI();
            }
        }

        public void UpdateUI()
        {
            Debug.Log(keysToDisable[keysIndex]);
        }

        public void Set(int numberLetters, float speed)
        {
            keysToDisable = new Keys[numberLetters];
            for (int i = 0; i < numberLetters; i++)
            {
                //random key
                int randomKey = Random.Range(0, System.Enum.GetValues(typeof(Keys)).Length);

                keysToDisable[i] = (Keys)randomKey;
            }

            this.speed = speed;

            //TODO deve prendere 2 punti random da usare come patrolMovements
        }
    }
}