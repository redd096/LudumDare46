using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare46
{
    public class TrapDigit : Trap
    {
        [Header("Digit")]
        [SerializeField] KeyCode[] keysToDisable = default;

        int keysIndex;

        protected override void Update()
        {
            //if pressed key
            if (Input.GetKeyDown(keysToDisable[keysIndex]))
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
    }
}