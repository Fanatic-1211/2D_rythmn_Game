using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    Animator myAnimator;
    const string damage = "damage";
    public SpriteRenderer animationSprite;
    // Start is called before the first frame update
    void Start()
    {
        animationSprite = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
        animationSprite.color = new Color(0f, 0f, 0f, 0f);
        myAnimator.SetBool(damage, false);
    }

    public void dmgAnimation()
    {
    StartCoroutine(Animate());
    }
    IEnumerator Animate()
    {
        animationSprite.color = new Color(255f, 255f, 255f, 255f);
        
        myAnimator.SetBool(damage, true);
        yield return new WaitForSeconds(0.666f);
        myAnimator.SetBool(damage, false);
        animationSprite.color = new Color(0f, 0f, 0f, 0f);
        
    }
}
