using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmManager : MonoBehaviour
{
    // values to track health, score, and combo
    int health;
    int combo;
    int score;

    // invincibility frames
    static float invTime = 2.0f;
    float curInv;


    // song to play
    AudioSource song;

    // track for notes
    // array of notes

    // buffer for diag taps
    float buff;

    // ring segments
    public RingControl ringL;
    public RingControl ringR;
    public RingControl ringU;
    public RingControl ringD;

    // track hilighter
    public highlighTrack track;

    // Start is called before the first frame update
    void Start()
    {
        // init ints
        health = 6; 
        combo = 0;
        score = 0;
        curInv = 0;

        song = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        tapTracking();
    }

    void tapTracking()
    {
        // flash rings on tap
        if (Input.GetKeyDown("w"))
        {
            ringU.flashRing();
            //track.flashTrack("U");

            // check note tap
            //if (CONDITION)
            //{
            //    track.flashTrack("W");
            //    Destroy(note.gameObject);
            //}

            // if not, make diagonal check
            // else
            StartCoroutine(diagonalCheck(0.1f, NOTE_TYPE.U));
        }

        if (Input.GetKeyDown("a"))
        {
            ringL.flashRing();
            //track.flashTrack("L");

            // check note tap
            //if (CONDITION)
            //{
            //    track.flashTrack("A");
            //    Destroy(note.gameObject);
            //}

            // if not, make diagonal check
            // else
            StartCoroutine(diagonalCheck(0.1f, NOTE_TYPE.L));
        }

        if (Input.GetKeyDown("s"))
        {
            ringD.flashRing();
            //track.flashTrack("D");

            // check note tap
            //if (CONDITION)
            //{
            //    track.flashTrack("S");
            //    Destroy(note.gameObject);
            //}

            // if not, make diagonal check\
            // else
            StartCoroutine(diagonalCheck(0.1f, NOTE_TYPE.D));
        }

        if (Input.GetKeyDown("d"))
        {
            ringR.flashRing();

            // check note tap
            //if (CONDITION)
            //{
            //    track.flashTrack("R");
            //    Destroy(note.gameObject);
            //}

            // if not, make diagonal check
            // else
            StartCoroutine(diagonalCheck(0.1f, NOTE_TYPE.R));
        }
    }

    IEnumerator diagonalCheck(float duration, NOTE_TYPE type)
    {
        // duration = buffer between note taps
        // NOTE_TYPE to determine the first input note and notes to check

        while (duration > 0)
        {
            // each input note has its own possible diagonals
            if (type == NOTE_TYPE.U)
            { 
                // check L or R
                // LEFT
                if (Input.GetKeyDown("a"))
                {
                    //track.flashTrack(NOTE_TYPE.UL);
                    break;
                }
                // RIGHT
                if (Input.GetKeyDown("d"))
                {
                    //track.flashTrack(NOTE_TYPE.UR);
                    break;
                }
            }
            else if (type == NOTE_TYPE.D)
            {
                // check L or R
                // LEFT
                if (Input.GetKeyDown("a"))
                {
                    //track.flashTrack(NOTE_TYPE.DL);
                    break;
                }
                // RIGHT
                if (Input.GetKeyDown("d"))
                {
                    //track.flashTrack(NOTE_TYPE.DR);
                    break;
                }
            }
            else if (type == NOTE_TYPE.L)
            {
                // check U or D
                // UP
                if (Input.GetKeyDown("w"))
                {
                    //track.flashTrack(NOTE_TYPE.UL);
                    break;
                }
                // DOWN
                if (Input.GetKeyDown("s"))
                {
                    //track.flashTrack(NOTE_TYPE.DL);
                    break;
                }
            }
            else if (type == NOTE_TYPE.R)
            {
                // check U or D
                // UP
                if (Input.GetKeyDown("w"))
                {
                    //track.flashTrack(NOTE_TYPE.UR);
                    break;
                }
                // DOWN
                if (Input.GetKeyDown("s"))
                {
                    //track.flashTrack(NOTE_TYPE.DR);
                    break;
                }
            }

            duration -= Time.deltaTime;

            yield return null;
        }
    }
}
