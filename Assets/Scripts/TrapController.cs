using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LudumDare46
{
    public class TrapController : MonoBehaviour
    {
        public static TrapController instance;

        Trap trapReference;

        GameObject tooltip;
        Text tooltip_text;

        void Start()
        {
            instance = this;

            tooltip = transform.Find("Tooltip").gameObject;
            tooltip_text = tooltip.transform.Find("Tooltip_Text").GetComponent<Text>();
            HideTooltip();
        }

        void Update()
        {
            if (trapReference)
                trapReference.Execution();
        }

        public void ShowTooltip(Trap trap)
        {
            tooltip.SetActive(true);

            trapReference = trap;
        }

        public void HideTooltip()
        {
            tooltip.SetActive(false);

            trapReference = null;
        }

        public void UpdateTooltip(string s)
        {
            tooltip_text.text = s;
        }

        public void MoveTooltip(Vector3 position)
        {
            tooltip.transform.position = position;
        }
    }
}