﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare46
{
    [System.Serializable]
    public enum Keys
    {
        q, w, e, r, t, y, u, i, o, p, 
        a, s, d, f, g, h, j, k, l, 
        z, x, c, v, b, n, m
    }

    public class TrapKeepPressed : MonoBehaviour
    {
        [Header("Debug KeepPressed")]
        [SerializeField] Keys[] keysToDisable = default;
        [SerializeField] float timeToKeepPressed = 1;

        int keysIndex;
        bool keepingPressed;
        float lastTimePressed;

        protected void OnMouseDown()
        {
            UpdateUI();
        }

        protected void Update()
        {
            //if key up
            if(keepingPressed && Input.GetKeyUp(keysToDisable[keysIndex].ToString()))
            {
                keepingPressed = false;
            }
            
            //if pressed key
            if (Input.GetKeyDown(keysToDisable[keysIndex].ToString()))
            {
                Debug.Log("Disattivando....");
                keepingPressed = true;
                lastTimePressed = Time.time + timeToKeepPressed;
            }

            //if keep pressed for the time
            if(keepingPressed && Time.time > lastTimePressed)
            {
                Debug.Log("MORIIIIIIII");
                keepingPressed = false;
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

        protected void UpdateUI()
        {
            Debug.Log(keysToDisable[keysIndex] + " - " + timeToKeepPressed);
        }

        public void Set(int numberLetters, float timeKeepPressed)
        {
            if (numberLetters == 0)
            {
                Debug.LogError("Perché non mi passi quante lettere devo premere? .-.");
                numberLetters = 1;
            }

            keysToDisable = new Keys[numberLetters];
            for(int i = 0; i < numberLetters; i++)
            {
                //random key
                int randomKey = Random.Range(0, System.Enum.GetValues(typeof(Keys)).Length);

                keysToDisable[i] = (Keys)randomKey;
            }

            timeToKeepPressed = timeKeepPressed;
        }

        protected void Die()
        {
            gameObject.SetActive(false);
        }
    }
}
