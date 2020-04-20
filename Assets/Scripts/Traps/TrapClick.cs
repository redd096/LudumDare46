using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LudumDare46
{
    public class TrapClick : MonoBehaviour
    {
        [Header("Debug Click")]
        [SerializeField] int clickToRemove = 5;

        TextMeshPro instruction_text;

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
            instruction_text.text = clickToRemove.ToString();
        }

        public void Set(int numberClicks)
        {
            clickToRemove = numberClicks;

            instruction_text = GetComponentInChildren<TextMeshPro>();
            UpdateUI();
        }

        protected void Die()
        {
            gameObject.SetActive(false);
        }
    }
}