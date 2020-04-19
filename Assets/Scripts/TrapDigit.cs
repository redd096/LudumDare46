using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare46
{
    public class TrapDigit : Trap
    {
        [SerializeField] KeyCode[] keysToDisable = default;

        int keysIndex;

        public override void UpdateUI()
        {
            TrapController.instance.UpdateTooltip( keysToDisable[keysIndex].ToString() );
        }

        public override void Execution()
        {
            base.Execution();

            //if pressed any key
            if(Input.anyKeyDown)
            {
                string inputsPressed = Input.inputString;

                //if same key
                if(inputsPressed == keysToDisable[keysIndex].ToString())
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
        }
    }
}