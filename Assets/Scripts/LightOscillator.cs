using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOscillator : MonoBehaviour
{

    public Light lightToFade;
    public float eachFadeTime = 2f;
    public float fadeWaitTime = 5f;

    void Start()
    {
        StartCoroutine(fadeInAndOutRepeat(lightToFade, eachFadeTime, fadeWaitTime));
    }

    IEnumerator fadeInAndOut(Light lightToFade, bool fadeIn, float duration)
    {
        float minLuminosity = 0; // min intensity
        float maxLuminosity = 1; // max intensity
        float counter = 0f;
        float a, b;

        if (fadeIn)
        {
            a = minLuminosity;
            b = maxLuminosity;
        }
        else
        {
            a = maxLuminosity;
            b = minLuminosity;
        }

        float currentIntensity = lightToFade.intensity;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            lightToFade.intensity = Mathf.Lerp(a, b, counter / duration);
            yield return null;
        }   
    }

    IEnumerator fadeInAndOutRepeat(Light lightToFade, float duration, float waitTime)
    {
        WaitForSeconds waitForXSec = new WaitForSeconds(waitTime);

        while (true)
        {
            yield return fadeInAndOut(lightToFade, false, duration);
            yield return waitForXSec;
            yield return fadeInAndOut(lightToFade, true, duration);
        }

    }



    
}
