using UnityEngine;
using Utilities;

namespace LudumDare46
{
    public class PlayerPrefsController
    {
        public const string MASTER_VOLUME_KEY = "volume on/off";
        public const string VIBRATION_KEY = "vibration";

        private static bool defaultValue = true;

        public static bool GetOption(string key)
        {
            bool toRet;
            if (!PlayerPrefs.HasKey(key))
            {
                toRet = defaultValue;
            }
            else
            {
                toRet = Utils.IntToBool(PlayerPrefs.GetInt(key));
            }
            return toRet;
        }

        public static void SetOption(string key, bool value)
        {
            PlayerPrefs.SetInt(key, Utils.BoolToInt(value));
        }


    }

}