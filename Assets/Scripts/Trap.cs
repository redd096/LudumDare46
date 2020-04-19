using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare46
{
    public class Trap : MonoBehaviour
    {
        [SerializeField] Vector3 offsetTooltip = default;

        private void OnMouseDown()
        {
            ShowUI();
        }

        private void OnMouseOver()
        {
            
        }

        public virtual void ShowUI()
        {
            TrapController.instance.ShowTooltip(this);

            UpdateUI();
        }

        public virtual void HideUI()
        {
            TrapController.instance.HideTooltip();
        }

        public virtual void UpdateUI()
        {
            //(╯°□°)╯︵ ┻━┻
        }

        public virtual void Execution()
        {
            TrapController.instance.MoveTooltip(transform.position + offsetTooltip);
        }

        protected virtual void Die()
        {
            HideUI();

            Destroy(gameObject);
        }
    }
}