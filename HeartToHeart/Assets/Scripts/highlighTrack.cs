using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class highlighTrack : MonoBehaviour
{
    // list of tracks: 
    // 0 - U
    // 1 - D
    // 2 - L
    // 3 - R
    // 4 - UL
    // 5 - UR
    // 6 - DL
    // 7 - DR
    public List<SpriteRenderer> tracks;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void flashTrack(NOTE_TYPE type)
    {
        int index = 0;

        switch (type)
        {
            case NOTE_TYPE.U:
                index = 0;
                break;
            case NOTE_TYPE.D:
                index = 1;
                break;
            case NOTE_TYPE.L:
                index = 2;
                break;
            case NOTE_TYPE.R:
                index = 3;
                break;
            case NOTE_TYPE.UL:
                index = 4;
                break;
            case NOTE_TYPE.UR:
                index = 5;
                break;
            case NOTE_TYPE.DL:
                index = 6;
                break;
            case NOTE_TYPE.DR:
                index = 7;
                break;
            case NOTE_TYPE.HU:
                index = 0;
                break;
            case NOTE_TYPE.HD:
                index = 1;
                break;
            case NOTE_TYPE.HL:
                index = 2;
                break;
            case NOTE_TYPE.HR:
                index = 3;
                break;
        }

        float duration = 0.3f;

        // end previous coroutine
        StopCoroutine("TrackFade");

        // set color to max opacity
        tracks[index].color = new Color(1, 1, 1, 1);

        // call coroutine
        StartCoroutine(TrackFade(duration, index));
    }
    IEnumerator TrackFade(float duration, int index)
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            // change alpha channel to decrease back to half opacity
            float alpha = Mathf.Lerp(1f, 0.0f, elapsed / duration);
            tracks[index].color = new Color(1, 1, 1, alpha);

            elapsed += Time.deltaTime;

            yield return null;
        }

        tracks[index].color = new Color(1, 1, 1, 0.0f);
    }
}
