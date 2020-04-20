using UnityEngine;
using UnityEngine.UI;
using LudumDare46.Enumerators;
using Utilities;

namespace LudumDare46
{

    public class Option : MonoBehaviour
    {
        [SerializeField] private Sprite[] sprites = default;
        [SerializeField] private Options whatOption = default;
        [SerializeField] private bool isOptionEnabled; // serialized for debugging purposes only;

        OptionsController optionsController;
        Image imageRenderer;

        private void Awake()
        {
            optionsController = GetComponentInParent<OptionsController>();
            imageRenderer = GetComponent<Image>();
        }

        private void OnEnable()
        {
            isOptionEnabled = optionsController.GetOptionValues(whatOption);
            if(isOptionEnabled)
            {
                imageRenderer.sprite = sprites[0];
            }
            else
            {
                imageRenderer.sprite = sprites[1];
            }
            
        }

        public void Toggle()
        {
            isOptionEnabled = !isOptionEnabled;
            if (isOptionEnabled)
            {
                imageRenderer.sprite = sprites[0];
            }
            else
            {
                imageRenderer.sprite = sprites[1];
            }
            if (isOptionEnabled && whatOption == Options.VIBRATION)
            {
                Handheld.Vibrate();
            }
            SaveOption();
        }

        private void SaveOption()
        {
            Debug.Log(string.Format("setting option {0} at {1} ", Utils.OptionToString(whatOption), isOptionEnabled));
            optionsController.SaveOption(whatOption, isOptionEnabled);
        }

        public bool GetOptionValue()
        {
            return isOptionEnabled;
        }

    }

}