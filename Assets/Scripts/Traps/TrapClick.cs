using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare46
{
    public class TrapClick : MonoBehaviour
    {
        [Header("Debug Click")]
        [SerializeField] int clickToRemove = 5;

        protected void OnMouseDown()
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

        protected void UpdateUI()
        {
            Debug.Log(clickToRemove);
        }

        public void Set(int numberClicks)
        {
            clickToRemove = numberClicks;
        }

        protected void Die()
        {
            gameObject.SetActive(false);
        }
    }
}