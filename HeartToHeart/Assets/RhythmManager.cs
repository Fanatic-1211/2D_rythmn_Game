using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmManager : MonoBehaviour
{
    // values to track health, score, and combo
    int health;
    int combo;
    int score;

    // song to play
    AudioSource song;

    // track for notes
    // array of notes

    // buffer for diag taps
    float buff;

    // Start is called before the first frame update
    void Start()
    {
        // init ints
        health = 6; 
        combo = 0;
        score = 0;

        song = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w")) { print("w pressed");}
        if (Input.GetKeyDown("a")) { print("a pressed");}
        if (Input.GetKeyDown("s")) { print("s pressed");}
        if (Input.GetKeyDown("d")) { print("d pressed");}

        Input.GetButtonDown("s");

    }
}
