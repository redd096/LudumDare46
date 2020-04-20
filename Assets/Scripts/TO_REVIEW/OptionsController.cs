using UnityEngine;
using LudumDare46.Enumerators;
using Utilities;

namespace LudumDare46
{

    public class OptionsController : MonoBehaviour
    {
        public void SaveOption(Options whatOption, bool value)
        {
            switch (whatOption)
            {
                case Options.SOUND:
                {
                    Debug.Log("saving sound at.. " + value);
                    PlayerPrefsController.SetOption(PlayerPrefsController.MASTER_VOLUME_KEY, value);
                    //FindObjectOfType<MusicPlayer>()?.SetVolume();
                    //MusicPlayer.Singleton?.SetVolume();
                    break;
                }
                case Options.VIBRATION:
                {
                    Debug.Log("saving vibration at.. " + value);
                    PlayerPrefsController.SetOption(PlayerPrefsController.VIBRATION_KEY, value); ;
                    break;
                }
                default:
                {
                    Debug.LogError("Unknown option requested!");
                    throw new System.Exception("Unknown option requested!");
                }
            }
        }

        public bool GetOptionValues(Options whatOption)
        {
            bool toRet;
            switch (whatOption)
            {
                case Options.SOUND:
                {
                    toRet = PlayerPrefsController.GetOption(PlayerPrefsController.MASTER_VOLUME_KEY);
                    Debug.Log("Sound was setted as: " + toRet);
                    break;
                }
                case Options.VIBRATION:
                {
                    toRet = PlayerPrefsController.GetOption(PlayerPrefsController.VIBRATION_KEY);
                    Debug.Log("Vibration was setted as: " + toRet);
                    break;
                }
                default:
                {
                    Debug.LogError("Unknown option requested!");
                    throw new System.Exception("Unknown option requested!");
                }
            }
            return toRet;
        }

    }

}