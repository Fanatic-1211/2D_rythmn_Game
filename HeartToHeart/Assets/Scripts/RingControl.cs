using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingControl : MonoBehaviour
{
    SpriteRenderer r;
    AudioSource s;

    // Start is called before the first frame update
    void Start() 
    {
        r = GetComponent<SpriteRenderer>();
        s = GetComponent<AudioSource>();
    }

    public void toggleRing(bool on)
    {
        if (on)
        {
            // turn ring on
            // set color to max opacity
            r.color = new Color(1, 1, 1, 1);
        }
        else
        {
            // turn ring off
            // remove opacity
            // set color to max opacity
            r.color = new Color(1, 1, 1, 0.5f);
        }
    }

    public void flashRing()
    {
        float duration = 0.3f;

        // end previous coroutine
        StopCoroutine("TapFade");

        // set color to max opacity
        r.color = new Color(1, 1, 1, 1);

        // play sounds
        s.Play();

        // call coroutine
        StartCoroutine(TapFade(duration));
    }
    IEnumerator TapFade(float duration)
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            // change alpha channel to decrease back to half opacity
            float alpha = Mathf.Lerp(1f, 0.5f, elapsed / duration);
            r.color = new Color(1, 1, 1, alpha);

            elapsed += Time.deltaTime;

            yield return null;
        }

        r.color = new Color(1, 1, 1, 0.5f);
    }
}
