using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace LudumDare46
{
    public class TrapClick : TrapBehaviour
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

        protected override void UpdateUI()
        {
            instruction_text.text = clickToRemove.ToString();
        }

        public void Set(int numberClicks)
        {
            clickToRemove = numberClicks;

            instruction_text = GetComponentInChildren<TextMeshPro>();
            UpdateUI();
        }

        protected override void Die()
        {
            base.Die();
        }
    }
}