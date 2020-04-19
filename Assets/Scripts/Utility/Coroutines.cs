using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace LudumDare46.Utilities
{
    public static class GameCoroutines
    {
        public static IEnumerator FillUp(Image image, float fillValue, float step)
        {
            //if(Mathf.Abs(image.fillAmount - fillValue) < Mathf.Epsilon) { yield return null; }
            if (image.fillAmount < fillValue)
            {
                //while (image.fillAmount < fillValue)
                while ((fillValue - image.fillAmount) > Mathf.Epsilon)
                {
                    yield return null;
                    image.fillAmount += step;
                }
            }
            else
            {
                //while (image.fillAmount > fillValue)
                while ((image.fillAmount - fillValue) > Mathf.Epsilon)
                {
                    yield return null;
                    image.fillAmount -= step;
                }
            }
        }

        public static IEnumerator ScaleUp(float scaleValue, Transform transform)
        {
            float scaled = transform.localScale.x;
            float increment = .1f;

            while (scaled < scaleValue)
            {
                transform.localScale += new Vector3(increment, increment, 0f);
                scaled += increment;
                yield return new WaitForSeconds(Time.deltaTime);
                //Debug.Log(scaled + " ---------- " + scaleValue);
            }
        }

    }

}
