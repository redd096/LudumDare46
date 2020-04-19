using System.Collections;
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

    public class TrapKeepPressed : Trap
    {
        [Header("Debug KeepPressed")]
        [SerializeField] Keys[] keysToDisable = default;
        [SerializeField] float timeToKeepPressed = 1;

        int keysIndex;
        bool keepingPressed;
        float lastTimePressed;

        protected override void Update()
        {
            //if key up
            if(keepingPressed && Input.GetKeyUp(keysToDisable[keysIndex].ToString()))
            {
                keepingPressed = false;
            }
            
            //if pressed key
            if (Input.GetKeyDown(keysToDisable[keysIndex].ToString()))
            {
                keepingPressed = true;
                lastTimePressed = Time.time + timeToKeepPressed;
            }

            //if keep pressed for the time
            if(keepingPressed && Time.time > lastTimePressed)
            {
                keepingPressed = false;
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
            Debug.Log(keysToDisable[keysIndex] + " - " + timeToKeepPressed);
        }

        public void Set(int numberLetters, float timeKeepPressed)
        {
            keysToDisable = new Keys[numberLetters];
            for(int i = 0; i < numberLetters; i++)
            {
                //random key
                int randomKey = Random.Range(0, System.Enum.GetValues(typeof(Keys)).Length);

                keysToDisable[i] = (Keys)randomKey;
            }

            timeToKeepPressed = timeKeepPressed;
        }
    }
}
