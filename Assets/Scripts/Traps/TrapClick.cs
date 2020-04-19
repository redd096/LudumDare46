using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare46
{
    public class TrapClick : Trap
    {
        [Header("Debug Click")]
        [SerializeField] int clickToRemove = 5;

        protected override void OnMouseDown()
        {
            clickToRemove--;

            //check if dead
            if (clickToRemove <= 0)
            {
                Die();
                return;
            }

            UpdateUI();
        }

        protected override void UpdateUI()
        {
            Debug.Log(clickToRemove);
        }

        public void Set(int numberClicks, float speed)
        {
            clickToRemove = numberClicks;
            this.speed = speed;

            //two random points to move
            patrolMovements = new Vector3[2];
            patrolMovements[0] = Utils.GetRandomWalkableNode();
            patrolMovements[1] = Utils.GetRandomWalkableNode();
        }
    }
}