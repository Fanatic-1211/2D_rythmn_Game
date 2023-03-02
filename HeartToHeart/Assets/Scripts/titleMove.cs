using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class titleMove : MonoBehaviour
{
    public RawImage img;

    // Update is called once per frame
    void Update()
    {
        img.uvRect = new Rect(img.uvRect.position + new Vector2(0.01f, 0.01f) * Time.deltaTime, img.uvRect.size);
    }
}
