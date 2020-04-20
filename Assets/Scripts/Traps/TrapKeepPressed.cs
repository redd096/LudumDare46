using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LudumDare46
{
    [System.Serializable]
    public enum Keys
    {
        q, w, e, r, t, y, u, i, o, p, 
        a, s, d, f, g, h, j, k, l, 
        z, x, c, v, b, n, m
    }

    public class TrapKeepPressed : MonoBehaviour
    {
        [Header("Debug KeepPressed")]
        [SerializeField] Keys[] keysToDisable = default;
        [SerializeField] float timeToKeepPressed = 1;

        int keysIndex;
        bool keepingPressed;
        float lastTimePressed;

        SpriteRenderer instruction_image;
        TextMeshPro instruction_text;

        protected void Update()
        {
            //if key up
            if(keepingPressed && Input.GetKeyUp(keysToDisable[keysIndex].ToString()))
            {
                keepingPressed = false;
                UpdateUI(true);
            }
            
            //if pressed key
            if (Input.GetKeyDown(keysToDisable[keysIndex].ToString()))
            {
                keepingPressed = true;
                lastTimePressed = Time.time + timeToKeepPressed;
            }

            if (keepingPressed)
                UpdateUI();

            //if keep pressed for the time
            if(keepingPressed && Time.time > lastTimePressed)
            {
                keepingPressed = false;
                keysIndex++;

                //check if dead
                if (keysIndex >= keysToDisable.Length)
                {
                    Die();
                    return;
                }

                UpdateUI(true);
            }
        }

        protected void UpdateUI(bool reset = false)
        {
            instruction_text.text = keysToDisable[keysIndex].ToString();

            //set delta for alpha (if reset, delta == 1)
            float delta = reset ? 1 : 1 / (timeToKeepPressed / (lastTimePressed - Time.time));

            //set image color
            Color imageColor = instruction_image.color;
            imageColor.a = delta;

            instruction_image.color = imageColor;

            //set text color
            Color textColor = instruction_text.color;
            textColor.a = delta;

            instruction_text.color = textColor;
        }

        public void Set(int numberLetters, float timeKeepPressed)
        {
            if (numberLetters == 0)
            {
                Debug.LogError("Perché non mi passi quante lettere devo premere? .-.");
                numberLetters = 1;
            }

            keysToDisable = new Keys[numberLetters];
            for(int i = 0; i < numberLetters; i++)
            {
                //random key
                int randomKey = Random.Range(0, System.Enum.GetValues(typeof(Keys)).Length);

                keysToDisable[i] = (Keys)randomKey;
            }

            timeToKeepPressed = timeKeepPressed;

            instruction_image = transform.Find("Instruction_Image").GetComponent<SpriteRenderer>();
            instruction_text = GetComponentInChildren<TextMeshPro>();
            UpdateUI(true);
        }

        protected void Die()
        {
            gameObject.SetActive(false);
            keysIndex = 0;
        }
    }
}
