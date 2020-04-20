using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare46
{
    [System.Serializable]
    public enum Keys
    {
        q, w, e, r, t, y, u, i, o, p,
        a, s, d, f, g, h, j, k, l,
        z, x, c, v, b, n, m,
    }

    public class TrapBehaviour : MonoBehaviour
    {
        protected TMPro.TextMeshPro instruction_text;

        protected virtual void UpdateUI()
        {
            //update how to kill
        }
        protected virtual void Die()
        {
            gameObject.SetActive(false);

            GameManager.instance.TrapDisabled();
        }
    }
}