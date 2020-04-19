using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare46
{
    public class TrapClick : Trap
    {
        [Header("Debug Click")]
        [SerializeField] int clickToRemove = 5;

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

        public void Set(int numberClicks, float speed)
        {
            clickToRemove = numberClicks;
            this.speed = speed;

            //TODO deve prendere 2 punti random da usare come patrolMovements
        }
    }
}