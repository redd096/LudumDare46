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
        [SerializeField] protected AudioClip spawnSound = default;
        [SerializeField] protected AudioClip killSound = default;

        protected virtual void UpdateUI()
        {
            //update how to kill
        }

        private void OnEnable()
        {
            AudioSource.PlayClipAtPoint(spawnSound, Camera.main.transform.position);
        }

        protected virtual void Die()
        {
            gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(killSound, Camera.main.transform.position);

            GameManager.instance.TrapDisabled();
        }
    }
}