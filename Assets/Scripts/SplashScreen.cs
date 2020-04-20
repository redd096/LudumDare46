using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] Image image = default;
    [Min(0)]
    [SerializeField] float waitBeforeStartFadeIn = 1;
    [Min(0)]
    [SerializeField] float timeToFadeIn = 1;
    [Min(0)]
    [SerializeField] float waitBeforeStartFadeOut = 2;
    [Min(0)]
    [SerializeField] float timeToFadeOut = 1;
    [SerializeField] string nextSceneName = "Main Scene";

    void Start()
    {
        StartCoroutine(FadeInAndOut());
    }

    IEnumerator FadeInAndOut()
    {
        //start alpha to 0
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);

        //wait before start fade in
        yield return new WaitForSeconds(waitBeforeStartFadeIn);

        //fade in
        float delta = 0;
        while (delta < 1)
        {
            delta = Fade(0, 1, delta, timeToFadeIn);

            yield return null;
        }

        //final alpha to 1
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);

        //wait before start fade out
        yield return new WaitForSeconds(waitBeforeStartFadeOut);

        //fade out
        delta = 0;
        while (delta < 1)
        {
            delta = Fade(1, 0, delta, timeToFadeOut);

            yield return null;
        }

        //final alpha to 0
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);

        //load new scene
        SceneManager.LoadScene(nextSceneName);
    }

    float Fade(float from, float to, float delta, float duration)
    {
        //speed based to duration
        delta += Time.deltaTime / duration;

        //set alpha from to
        float alpha = Mathf.Lerp(from, to, delta);
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);

        return delta;
    }
}
