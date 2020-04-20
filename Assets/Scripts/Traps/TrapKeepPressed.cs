using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace LudumDare46
{
    public class TrapKeepPressed : TrapBehaviour
    {
        [Header("Debug KeepPressed")]
        [SerializeField] Keys[] keysToDisable = default;
        [SerializeField] float timeToKeepPressed = 1;

        int keysIndex;
        bool keepingPressed;
        float lastTimePressed;

        SpriteRenderer instruction_image;

        protected void Update()
        {
            //if key up
            if(keepingPressed && Input.GetKeyUp(keysToDisable[keysIndex].ToString()))
            {
                keepingPressed = false;
                UpdatePressedUI(true);
            }
            
            //if pressed key
            if (Input.GetKeyDown(keysToDisable[keysIndex].ToString()))
            {
                keepingPressed = true;
                lastTimePressed = Time.time + timeToKeepPressed;
            }

            if (keepingPressed)
                UpdatePressedUI();

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

                UpdatePressedUI(true);
            }
        }

        void UpdatePressedUI(bool reset = false)
        {
            instruction_text.text = keysToDisable[keysIndex].ToString().ToUpper();

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
            UpdatePressedUI(true);
        }

        protected override void Die()
        {
            base.Die();

            keysIndex = 0;
        }
    }
}
