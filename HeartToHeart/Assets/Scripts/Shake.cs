using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public AnimationCurve curve;
    public float duration = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startShaking() {
        StartCoroutine(Shaking());
    }

    IEnumerator Shaking() {
        Vector3 startPos = transform.position;
        float elapsedTime = 0f;

        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime/duration);
            transform.position = startPos + Random.insideUnitSphere * strength;
            yield return null;
        }
        transform.position = startPos;
    }
}
