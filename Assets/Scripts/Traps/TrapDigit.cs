using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare46
{
    public class TrapDigit : MonoBehaviour
    {
        [Header("Debug Digit")]
        [SerializeField] Keys[] keysToDisable = default;

        int keysIndex;

        protected void OnMouseDown()
        {
            Debug.Log(keysToDisable[keysIndex]); 
        }

        void Update()
        {
            //base.Update();

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
                Debug.Log(keysToDisable[keysIndex]);
            }

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

        }

        protected void Die()
        {
            gameObject.SetActive(false);
            keysIndex = 0;
        }

    }
}