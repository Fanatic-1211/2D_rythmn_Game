using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartControl : MonoBehaviour
{
    public RhythmManager parent;
    public AnimationControl animation;
    private SpriteRenderer heartSprite;
    float flashLength;
    // Start is called before the first frame update

    void Start()
    {
        parent = GameObject.Find("Field").GetComponent<RhythmManager>();
        animation = GameObject.Find("HeartAnimator").GetComponent<AnimationControl>();
        heartSprite = GetComponent<SpriteRenderer>();
       


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void dmgFlash()
    {
        flashLength = 0.2f; // Change to a ratio of the BPM??
        StartCoroutine(Flash());
    }
    public void dmgAnimation()
    {
        animation.dmgAnimation();
        

    }
    
    IEnumerator Flash()
    {
        heartSprite.color = new Color(1f, 1f, 1f, 0.2f); // flash transparent
        yield return new WaitForSeconds(flashLength);
        heartSprite.color = new Color(1f, 1f, 1f, 1f); // return to og color
        yield return new WaitForSeconds(flashLength);
        if (parent.inv) // if still invincible, continue flashing!
        {
            flashLength /= 1.17f;
            StartCoroutine(Flash());
        }
    }
}
