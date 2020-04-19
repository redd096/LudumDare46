using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare46
{
    public class TrapClick : Trap
    {
        [Header("Click")]
        [SerializeField] float clickToRemove = 5;

        void OnMouseDown()
        {
            clickToRemove--;

            //check if dead
            if (clickToRemove <= 0)
            {
                Destroy(gameObject);
                return;
            }

            UpdateUI();
        }

        public void UpdateUI()
        {
            Debug.Log(clickToRemove);
        }
    }
}