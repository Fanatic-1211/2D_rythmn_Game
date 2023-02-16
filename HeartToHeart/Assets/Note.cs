using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NOTE_TYPE { U, D, L, R, UL, UR, DL, DR };

public class Note : MonoBehaviour
{
    float time;
    public NOTE_TYPE type;

    // Start is called before the first frame update
    void Start()
    {
        // assign time
        time = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // reduce note time
        time -= Time.deltaTime;
    }
}
