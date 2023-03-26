using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HOLD_NOTE_TYPE {HU, HD, HR, HL};

public class HoldNote : MonoBehaviour
{
    public float time;
    public float secondNoteTime;
    public float firstNoteTime;
    public HOLD_NOTE_TYPE type;
    float size;
    public Note firstNote;
    public Note secondNote;
    GameObject newNote1;
    GameObject newNote2;
    float offset;
    public bool held = false;
    bool created = false;

    public float trackSpeed;

    public RhythmManager parent;

    // Start is called before the first frame update
    void Start()
    {
        // assign time (currently set to def value)
        // time = 2.0f;

        // get reference to RhythmManager
        parent = GameObject.Find("Field").GetComponent<RhythmManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (created == true)
        {
            Vector3 centerPos = new Vector3(newNote1.transform.position.x + newNote2.transform.position.x, newNote1.transform.position.y + newNote2.transform.position.y) / 2;
            if (secondNoteTime >= 0.02225)
            {
                if (type == HOLD_NOTE_TYPE.HU || type == HOLD_NOTE_TYPE.HD)
                {
                    float scaleY = Mathf.Abs(newNote1.transform.position.y - newNote2.transform.position.y);
                    transform.localScale = new Vector3(1, scaleY * 9.46f, 1);
                }
                else
                {
                    float scaleX = Mathf.Abs(newNote1.transform.position.x - newNote2.transform.position.x);
                    transform.localScale = new Vector3(scaleX * 9.46f, 1, 1);
                }
            }
            else
            {
                centerPos = new Vector3(100, 100);
            }
            transform.position = centerPos;
        }
        if (held == true)
        {
            Debug.Log("Test");
            if (Input.GetKey(KeyCode.A) && firstNote.type == NOTE_TYPE.HL)
            {
                firstNoteTime = 0.02225f;
                checkTime();
            }
            else if (Input.GetKeyUp(KeyCode.A) && firstNote.type == NOTE_TYPE.HL)
            {
                Debug.Log("TestA");
                checkHitSecond();
                checkTime();
                held = false;
            }
            if (Input.GetKey(KeyCode.D) && firstNote.type == NOTE_TYPE.HR)
            {
                firstNoteTime = 0.02225f;
                checkTime();
            }
            else if (Input.GetKeyUp(KeyCode.D) && firstNote.type == NOTE_TYPE.HR)
            {
                Debug.Log("TestD");
                checkHitSecond();
                checkTime();
                held = false;
            }
            if (Input.GetKey(KeyCode.W) && firstNote.type == NOTE_TYPE.HU)
            {
                firstNoteTime = 0.02225f;
                checkTime();
            }
            else if (Input.GetKeyUp(KeyCode.W) && firstNote.type == NOTE_TYPE.HU)
            {
                Debug.Log("TestW");
                checkHitSecond();
                checkTime();
                held = false;
            }
            if (Input.GetKey(KeyCode.S) && firstNote.type == NOTE_TYPE.HD)
            {
                firstNoteTime = 0.02225f;
                checkTime();
            }
            else if (Input.GetKeyUp(KeyCode.S) && firstNote.type == NOTE_TYPE.HD)
            {
                Debug.Log("TestS");
                checkHitSecond();
                checkTime();
                held = false;
            }
        }
    }
    public void checkTime()
    {
        if (parent.returnSecondNote() == null)
            return;
        if (parent.returnSecondNote().getTime() <= -0.25f)
        {
            // combo
            parent.destroyGameObject();
        }
    }
    public void checkHitSecond()
    {
        if (parent.returnSecondNote().checkHit() != 4)
        {
            parent.destroyGameObject();
        }
    }
    public void incrementPosition()
    {
        // reduce note time
        if (held == true)
        {
            secondNoteTime -= Time.deltaTime;
        }
        else
        {
            
            firstNoteTime -= Time.deltaTime;
            secondNoteTime -= Time.deltaTime;
        }
        // draw note
        drawNote();

        // if position hits 0, take damage, delete note
        if (firstNoteTime <= -0.2864)
            takeDamage();

    }

    // creates general note given the type and time associated with it
    public void initializeNote(HOLD_NOTE_TYPE _type, NOTE_TYPE _type2, float _time, Sprite sprite, Sprite noteSprite, GameObject noteInfo, GameObject noteInfo2, float offset)
    {
        newNote1 = noteInfo;
        newNote2 = noteInfo2;
        this.offset = offset;
        time = _time + offset;
        firstNoteTime = _time;
        type = _type;
        // Change sprite
        GetComponent<SpriteRenderer>().sprite = sprite;
        newNote1.GetComponent<Note>().initializeNote(_type2, _time, noteSprite);
        if (_type2 == NOTE_TYPE.HD || _type2 == NOTE_TYPE.HL)
        {
            secondNoteTime = _time + (offset) ;
        }
        else
        {
            secondNoteTime = _time + (offset);
        }
        newNote2.GetComponent<Note>().initializeNote(_type2, _time + offset, noteSprite);
        firstNote = newNote1.GetComponent<Note>();
        secondNote = newNote2.GetComponent<Note>();
        created = true;
    }

    public void drawNote()
    {
        switch (type)
        {
            case HOLD_NOTE_TYPE.HL:
                // transform.position = new Vector3(-(0.716f + (time * trackSpeed)), 0, 0);
                firstNote.transform.position = new Vector3(-(0.716f + (firstNoteTime * trackSpeed)), 0, 0);
                secondNote.transform.position = new Vector3(-(0.716f + (secondNoteTime * trackSpeed)), 0, 0);
                firstNote.setTime(firstNoteTime);
                secondNote.setTime(secondNoteTime);
                break;
            case HOLD_NOTE_TYPE.HR:
                // transform.position = new Vector3(0.716f + (time * trackSpeed), 0, 0);
                firstNote.transform.position = new Vector3(0.716f + (firstNoteTime * trackSpeed), 0, 0);
                secondNote.transform.position = new Vector3(0.716f + (secondNoteTime * trackSpeed), 0, 0);
                firstNote.setTime(firstNoteTime);
                secondNote.setTime(secondNoteTime);
                break;
            case HOLD_NOTE_TYPE.HU:
                // transform.position = new Vector3(0, 0.716f + (time * trackSpeed), 0);
                firstNote.transform.position = new Vector3(0, 0.716f + (firstNoteTime * trackSpeed), 0);
                secondNote.transform.position = new Vector3(0, 0.716f + (secondNoteTime * trackSpeed), 0);
                firstNote.setTime(firstNoteTime);
                secondNote.setTime(secondNoteTime);
                break;
            case HOLD_NOTE_TYPE.HD:
                // transform.position = new Vector3(0, -(0.716f + (time * trackSpeed)), 0);
                firstNote.transform.position = new Vector3(0, -(0.716f + (firstNoteTime * trackSpeed)), 0);
                secondNote.transform.position = new Vector3(0, -(0.716f + (secondNoteTime * trackSpeed)), 0);
                firstNote.setTime(firstNoteTime);
                secondNote.setTime(secondNoteTime);
                break;
        }
    }

    public int checkHit()
    {
        // if the time is between the window, count as hit
        //good
        if (Mathf.Abs(time) < 0.15f)
        {
            return 1;
        }
        //great
        else if (Mathf.Abs(time) < 0.10f)
        {
            return 2;
        }
        //perfect
        else if (Mathf.Abs(time) < 0.5f)
        {
            return 3;
        }
        else
        {
            return 4;
        }
    }

    void takeDamage()
    {
        // talk to parent class about damage
        parent.takeDamage();

        // destroy object
        Destroy(newNote1);
        Destroy(newNote2);
        Destroy(gameObject);
    }

    public float getTime()
    {
        return time;
    }
}
