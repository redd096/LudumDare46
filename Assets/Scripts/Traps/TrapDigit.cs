using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace LudumDare46
{
    public class TrapDigit : TrapBehaviour
    {
        [Header("Debug Digit")]
        [SerializeField] Keys[] keysToDisable = default;

        int keysIndex;

        void Update()
        {
            if (GameManager.pause) return;

            if (keysIndex >= keysToDisable.Length)
                Debug.LogError("Come ci siamo arrivati a " + keysIndex + " numeri?");

            //if pressed key
            if (Input.GetKeyDown(keysToDisable[keysIndex].ToString()))
            {
                keysIndex++;

                //check if dead
                if (keysIndex >= keysToDisable.Length)
                {
                    Die();
                    return;
                }

                UpdateUI();
            }

        }

        protected override void UpdateUI()
        {
            instruction_text.text = keysToDisable[keysIndex].ToString().ToUpper();
        }

        public void Set(int numberLetters)
        {
            if (numberLetters == 0)
            {
                Debug.LogError("Perché non mi passi quante lettere devo premere? .-.");
                numberLetters = 1;
            }

            keysToDisable = new Keys[numberLetters];
            for (int i = 0; i < numberLetters; i++)
            {
                //random key
                int randomKey = Random.Range(0, System.Enum.GetValues(typeof(Keys)).Length);

                keysToDisable[i] = (Keys)randomKey;
            }
            instruction_text = GetComponentInChildren<TextMeshPro>();
            UpdateUI();
        }

        protected override void Die()
        {
            base.Die();

            keysIndex = 0;
        }

    }
}