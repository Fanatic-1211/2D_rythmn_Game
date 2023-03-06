using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmManager : MonoBehaviour
{
    // values to track health, score, and combo
    public int health;     // VARIABLE
    int combo;
    int score;

    // heart related stuff (might put this in the heart-connected script if too much)
    public HeartControl heart;
    public bool inv; // true if invincible. We can have this on true for the tutorial stage!


    // invincibility frames
    static float invTime = 2.0f;
    float curInv; // don't think I'll use this

    // bool for advancing notes
    bool notePause = false;

    // song to play
    public AudioSource audSource;

    // track for notes
    List<Note> notes;

    // ring segments
    public RingControl ringL;
    public RingControl ringR;
    public RingControl ringU;
    public RingControl ringD;

    // track hilighter
    public highlighTrack track;

    // note generator
    public NoteGen noteGen;

    // Start is called before the first frame update
    void Start()
    {
        // init ints
        health = 6; // change this based on difficulty
        combo = 0;
        score = 0;
        inv = false;

        heart = GameObject.Find("Heart").GetComponent<HeartControl>();

        print("generating notes...");
        // notes = noteGen.genRandNotes(40f);
        notes = noteGen.getNotes("Assets/Map/TestMap.txt");
    }

    // Update is called once per frame
    void Update()
    {
        if (!notePause)
            advanceNotes();

        tapTracking();

        if (Input.GetKeyDown("p") || Input.GetKeyDown(KeyCode.Escape))
            notePause = !notePause;
    }

    void advanceNotes()
    {
        for (int i = 0; i < notes.Count; i++)
            if (notes[i] == null)
                notes.Remove(notes[i]);
            else
                notes[i].incrementPosition();
    }

    // function that determines what note is tapped
    void tapTracking()
    {
        // flash rings on tap
        if (Input.GetKeyDown("w"))
        {
            ringU.flashRing();

            // check if note was hit
            // if not, make diagonal check
            if (!checkNoteTap(NOTE_TYPE.U))
                StartCoroutine(diagonalTracking(0.1f, NOTE_TYPE.U));
        }

        if (Input.GetKeyDown("a"))
        {
            ringL.flashRing();

            // check if note was hit
            // if not, make diagonal check
            if (!checkNoteTap(NOTE_TYPE.L))
                StartCoroutine(diagonalTracking(0.1f, NOTE_TYPE.L));
        }

        if (Input.GetKeyDown("s"))
        {
            ringD.flashRing();

            // check if note was hit
            // if not, make diagonal check
            if (!checkNoteTap(NOTE_TYPE.D))
                StartCoroutine(diagonalTracking(0.1f, NOTE_TYPE.D));
        }

        if (Input.GetKeyDown("d"))
        {
            ringR.flashRing();

            // check if note was hit
            // if not, make diagonal check
            if (!checkNoteTap(NOTE_TYPE.R))
                StartCoroutine(diagonalTracking(0.1f, NOTE_TYPE.R));
        }
    }

    // helper IEnumerator that determines in a note was a diagonal tap
    IEnumerator diagonalTracking(float duration, NOTE_TYPE type)
    {
        // duration = buffer between note taps
        // NOTE_TYPE to determine the first input note and notes to check

        while (duration > 0)
        {
            // each input note has its own possible diagonals
            // check if note was hit, then flash track
            if (type == NOTE_TYPE.U)
            { 
                // check L or R
                // LEFT
                if (Input.GetKeyDown("a"))
                {
                    if (checkNoteTap(NOTE_TYPE.UL))
                        track.flashTrack(NOTE_TYPE.UL);
                    break;
                }
                // RIGHT
                else if (Input.GetKeyDown("d"))
                {
                    if (checkNoteTap(NOTE_TYPE.UR))
                        track.flashTrack(NOTE_TYPE.UR);
                    break;
                }
            }
            else if (type == NOTE_TYPE.D)
            {
                // check L or R
                // LEFT
                if (Input.GetKeyDown("a"))
                {
                    if (checkNoteTap(NOTE_TYPE.DL))
                        track.flashTrack(NOTE_TYPE.DL);
                    break;
                }
                // RIGHT
                else if (Input.GetKeyDown("d"))
                {
                    if (checkNoteTap(NOTE_TYPE.DR))
                        track.flashTrack(NOTE_TYPE.DR);
                    break;
                }
            }
            else if (type == NOTE_TYPE.L)
            {
                // check U or D
                // UP
                if (Input.GetKeyDown("w"))
                {
                    if (checkNoteTap(NOTE_TYPE.UL))
                        track.flashTrack(NOTE_TYPE.UL);
                    break;
                }
                // DOWN
                else if (Input.GetKeyDown("s"))
                {
                    if (checkNoteTap(NOTE_TYPE.DL))
                        track.flashTrack(NOTE_TYPE.DL);
                    break;
                }
            }
            else if (type == NOTE_TYPE.R)
            {
                // check U or D
                // UP
                if (Input.GetKeyDown("w"))
                {
                    if (checkNoteTap(NOTE_TYPE.UR))
                        track.flashTrack(NOTE_TYPE.UR);
                    break;
                }
                // DOWN
                else if (Input.GetKeyDown("s"))
                {
                    if (checkNoteTap(NOTE_TYPE.DR))
                        track.flashTrack(NOTE_TYPE.DR);
                    break;
                }
            }

            duration -= Time.deltaTime;

            yield return null;
        }
    }

    // function that detemines in a note in the list has been tapped
    bool checkNoteTap(NOTE_TYPE direction)
    {
        // check the most recent entries in the 'notes' list to determine if a hit was secured in the range
        // use helper function in note class

        for (int i = 0; i < notes.Count; i++)
        {
            if (notes[i].checkHit())
                if (notes[i].type == direction)
                {
                    // note was in hiting range AND is the correct type, we can remove it from the list and delete the game ovject
                    // flash the track, increment combo, add to score
                    track.flashTrack(direction);
                    GameObject toDelete = notes[i].gameObject;

                    notes.Remove(notes[i]);
                    Destroy(toDelete);

                    //INCREMENT COMBO, ADD TO SCORE

                    return true;
                }
        }

        return false;
    }




    // ------------ DAMAGE ------------ //
    public void takeDamage()
    {
        if (!inv)
        {
            health -= 1;
            StartCoroutine(invincibility(invTime));
            heart.dmgFlash();
        }
    }

    IEnumerator invincibility(float duration)
    {
        inv = true;
        yield return new WaitForSeconds(duration);
        inv = false;
    }
}
