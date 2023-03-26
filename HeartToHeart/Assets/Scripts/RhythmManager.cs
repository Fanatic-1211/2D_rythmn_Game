using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmManager : MonoBehaviour
{
    // values to track health, score, and combo
    public int health;     // VARIABLE
    int combo;
    int score;
    int index;

    // heart related stuff (might put this in the heart-connected script if too much)
    public HeartControl heart;
    public bool inv; // true if invincible. We can have this on true for the tutorial stage!


    // invincibility frames
    static float invTime = 2.0f;
    float curInv; // don't think I'll use this

    // bool for advancing notes
    bool notePause = false;
    bool held = false;
    // song to play
    public AudioSource audSource;

    // track for notes
    List<Note> notes;
    List<HoldNote> holdNotes;

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
        holdNotes = noteGen.getHoldNotes("Assets/Map/TestHoldMap.txt");

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

        for (int i = 0; i < holdNotes.Count; i++)
            if (holdNotes[i] == null)
                holdNotes.Remove(holdNotes[i]);
            else
                holdNotes[i].incrementPosition();
    }

    // function that determines what note is tapped
    void tapTracking()
    {
        // flash rings on tap
        if (Input.GetKeyDown("w"))
        {
            ringU.flashRing();
            if (!checkHoldNoteTap(NOTE_TYPE.HU))
            {
                
            }
            // check if note was hit
            // if not, make diagonal check
            if (!checkNoteTap(NOTE_TYPE.U) && !checkNoteTap(NOTE_TYPE.HU))
                StartCoroutine(diagonalTracking(0.1f, NOTE_TYPE.U));
        }
        if (Input.GetKeyDown("a"))
        {
            ringL.flashRing();
            if (!checkHoldNoteTap(NOTE_TYPE.HL))
            {
               
            }
            // check if note was hit
            // if not, make diagonal check
            if (!checkNoteTap(NOTE_TYPE.L) && !checkNoteTap(NOTE_TYPE.HL))
                StartCoroutine(diagonalTracking(0.1f, NOTE_TYPE.L));
        }
        if (Input.GetKeyDown("s"))
        {
            ringD.flashRing();
            if (!checkHoldNoteTap(NOTE_TYPE.HD))
            {
              
            }
            // check if note was hit
            // if not, make diagonal check
            if (!checkNoteTap(NOTE_TYPE.D) && !checkNoteTap(NOTE_TYPE.HD))
                StartCoroutine(diagonalTracking(0.1f, NOTE_TYPE.D));
        }
        if (Input.GetKeyDown("d"))
        {
            ringR.flashRing();
            if (!checkHoldNoteTap(NOTE_TYPE.HR))
            {
              
            }
            // check if note was hit
            // if not, make diagonal check
            if (!checkNoteTap(NOTE_TYPE.R) && !checkNoteTap(NOTE_TYPE.HR))
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
            if (notes[i].checkHit()!=4)
                if (notes[i].type == direction)
                {
                    // note was in hiting range AND is the correct type, we can remove it from the list and delete the game ovject
                    // flash the track, increment combo, add to score
                    track.flashTrack(direction);
                    GameObject toDelete = notes[i].gameObject;
                    if (notes[i].checkHit() == 1)
                    {
                        combo++;
                    }
                    else if (notes[i].checkHit() == 2)
                    {
                        combo++;
                    }
                    else if (notes[i].checkHit() == 3){
                        combo = 0;
                    }


                    notes.Remove(notes[i]);
                    Destroy(toDelete);

                    //INCREMENT COMBO, ADD TO SCORE
                    //Adjust poor great and perfect here as well using notes[i].checkHit's vaule

                    return true;
                }
        }

        return false;
    }



    bool checkHoldNoteTap(NOTE_TYPE direction)
    {
        // check the most recent entries in the 'notes' list to determine if a hit was secured in the range
        // use helper function in note class
        for (int i = 0; i < holdNotes.Count; i++)
        {
            if (holdNotes[i].firstNote.checkHit() != 4)
            {
                if (holdNotes[i].firstNote.type == direction)
                {
                    // note was in hiting range AND is the correct type, we can remove it from the list and delete the game ovject
                    // flash the track, increment combo, add to score
                    // Slowly remove fill until done
                    holdNotes[i].firstNoteTime -= holdNotes[i].firstNoteTime;
                    holdNotes[i].secondNoteTime -= holdNotes[i].firstNoteTime;
                    track.flashTrack(direction);
                    Debug.Log(direction);
                    index = i;
                    holdNotes[i].held = true;
                    //INCREMENT COMBO, ADD TO SCORE
                    //Adjust poor great and perfect here as well using notes[i].checkHit's vaule
                    return true;
                }
            }
        }
        return false;
    }
    public Note returnSecondNote()
    {
        if (index >= 0 && index < holdNotes.Count)
            return holdNotes[index].secondNote;
        else
            return null;
    }
    public void destroyGameObject()
    {
        GameObject toDelete1 = holdNotes[index].gameObject;
        GameObject toDelete2 = holdNotes[index].firstNote.gameObject;
        GameObject toDelete3 = holdNotes[index].secondNote.gameObject;
        track.flashTrack(holdNotes[index].secondNote.type);
        holdNotes.Remove(holdNotes[index]);
        Destroy(toDelete1);
        Destroy(toDelete2);
        Destroy(toDelete3);
    }
    // ------------ DAMAGE ------------ //
    public void takeDamage()
    {
        if (!inv)
        {
            health -= 1;
            heart.dmgAnimation();
            StartCoroutine(invincibility(invTime));
            
            heart.dmgFlash();
            print(health);
        }
    }

    IEnumerator invincibility(float duration)
    {
        inv = true;
        yield return new WaitForSeconds(duration);
        inv = false;
    }
    public void setCombo(int num)
    {
        combo = num;
    }
}
