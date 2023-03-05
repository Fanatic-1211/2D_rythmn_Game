using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NOTE_TYPE { U, D, L, R, UL, UR, DL, DR};

public class Note : MonoBehaviour
{
    float time;
    public NOTE_TYPE type;

    // useful math for diagonals
    float mult = Mathf.Sin(Mathf.PI / 4.0f);
    public float trackSpeed = 3.0f;

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
        
    }

    public void incrementPosition()
    {
        // reduce note time
        time -= Time.deltaTime;

        // draw note
        drawNote();

        // if position hits 0, take damage, delete note
        if (time <= -0.2864)
            takeDamage();
    }

    // creates general note given the type and time associated with it
    public void initializeNote(NOTE_TYPE _type, float _time, Sprite sprite)
    {
        time = _time;
        type = _type;
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    void drawNote()
    {
        switch (type)
        {
            case NOTE_TYPE.L:
                transform.position = new Vector3(-(0.716f + (time * trackSpeed)), 0, 0);
                break;
            case NOTE_TYPE.R:
                transform.position = new Vector3(0.716f + (time * trackSpeed), 0, 0);
                break;
            case NOTE_TYPE.U:
                transform.position = new Vector3(0, 0.716f + (time * trackSpeed), 0);
                break;
            case NOTE_TYPE.D:
                transform.position = new Vector3(0, -(0.716f + (time * trackSpeed)), 0);
                break;

            case NOTE_TYPE.UR:
                transform.position = new Vector3(0.545f + (time * trackSpeed * mult), 0.545f + (time * trackSpeed * mult), 0);
                break;
            case NOTE_TYPE.UL:
                transform.position = new Vector3(-(0.545f + (time * trackSpeed * mult)), 0.545f + (time * trackSpeed * mult), 0);
                break;
            case NOTE_TYPE.DL:
                transform.position = new Vector3(-(0.545f + (time * trackSpeed * mult)), -(0.545f + (time * trackSpeed * mult)), 0);
                break;
            case NOTE_TYPE.DR:
                transform.position = new Vector3(0.545f + (time * trackSpeed * mult), -(0.545f + (time * trackSpeed * mult)), 0);
                break;
        }
    }

    public int checkHit()
    {
        // if the time is between the window, count as hit
        if (Mathf.Abs(time) < 0.15f) {
            return 1;
        }
        else if (Mathf.Abs(time) < 0.10f) { 
            return 2;
            }
        else if(Mathf.Abs(time) < 0.5f){
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
        Destroy(gameObject);
    }

    public float getTime()
    {
        return time;
    }
}
