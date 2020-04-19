using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare46
{
    public class TrapKeepPressed : Trap
    {
        [Header("Keep Pressed")]
        [SerializeField] KeyCode[] keysToDisable = default;
        [SerializeField] float timeToKeepPressed = 1;

        int keysIndex;
        bool keepingPressed;
        float lastTimePressed;

        protected override void Update()
        {
            //if key up
            if(keepingPressed && Input.GetKeyUp(keysToDisable[keysIndex]))
            {
                keepingPressed = false;
            }
            
            //if pressed key
            if (Input.GetKeyDown(keysToDisable[keysIndex]))
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
    }
}
