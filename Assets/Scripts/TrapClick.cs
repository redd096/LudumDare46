using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare46
{
    public class TrapClick : Trap
    {
        [SerializeField] float clickToRemove = 5;

        public override void UpdateUI()
        {
            TrapController.instance.UpdateTooltip( clickToRemove.ToString() );
        }

        public override void Execution()
        {
            base.Execution();

            //if pressed left click
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                clickToRemove--;

                //check if dead
                if(clickToRemove <= 0)
                {
                    Die();
                    return;
                }

                UpdateUI();
            }
        }
    }
}