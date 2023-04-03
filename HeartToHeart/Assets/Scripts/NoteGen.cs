using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class NoteGen : MonoBehaviour
{
    // list of sprites to feed to prefab notes
    public List<Sprite> noteSprites = new List<Sprite>();

    // prefab note in question
    public GameObject prefab;
    public GameObject prefab2;
    Vector3 scaleChange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Note> genRandNotes(float totTime)
    {
        List<Note> noteList = new List<Note>();

        float tcounter = 0;

        while (tcounter < totTime)
        {
            int noteInd = Random.Range(0, 10);
            float noteTime = tcounter + 2f;
            
            // create new instance of a note
            GameObject newNote = Instantiate(prefab);

            // edit note to be based on the note type
            switch (noteInd)
            {
                case 0:
                    newNote.GetComponent<Note>().initializeNote(NOTE_TYPE.L, noteTime, noteSprites[0]);
                    break;
                case 1:
                    newNote.GetComponent<Note>().initializeNote(NOTE_TYPE.R, noteTime, noteSprites[1]);
                    break;
                case 2:
                    newNote.GetComponent<Note>().initializeNote(NOTE_TYPE.U, noteTime, noteSprites[2]);
                    break;
                case 3:
                    newNote.GetComponent<Note>().initializeNote(NOTE_TYPE.D, noteTime, noteSprites[3]);
                    break;
                case 4:
                    newNote.GetComponent<Note>().initializeNote(NOTE_TYPE.UL, noteTime, noteSprites[4]);
                    break;
                case 5:
                    newNote.GetComponent<Note>().initializeNote(NOTE_TYPE.UR, noteTime, noteSprites[5]);
                    break;
                case 6:
                    newNote.GetComponent<Note>().initializeNote(NOTE_TYPE.DL, noteTime, noteSprites[6]);
                    break;
                case 7:
                    newNote.GetComponent<Note>().initializeNote(NOTE_TYPE.DR, noteTime, noteSprites[7]);
                    break;
                case 8:
                    // make other pair of duo notes
                    GameObject _newDuoNote = Instantiate(prefab);

                    newNote.GetComponent<Note>().initializeNote(NOTE_TYPE.L, noteTime, noteSprites[8]);
                    _newDuoNote.GetComponent<Note>().initializeNote(NOTE_TYPE.R, noteTime, noteSprites[9]);

                    // add extra note to list
                    noteList.Add(_newDuoNote.GetComponent<Note>());

                    break;
                case 9:
                    // make other pair of duo notes
                    GameObject newDuoNote_ = Instantiate(prefab);

                    newNote.GetComponent<Note>().initializeNote(NOTE_TYPE.U, noteTime, noteSprites[10]);
                    newDuoNote_.GetComponent<Note>().initializeNote(NOTE_TYPE.D, noteTime, noteSprites[11]);

                    // add extra note to list
                    noteList.Add(newDuoNote_.GetComponent<Note>());

                    break;
            }

            // add to the list
            noteList.Add(newNote.GetComponent<Note>());

            tcounter += Random.Range(0.3f, 1.0f);
        }

        return noteList;
    }

    public List<HoldNote> genRandHoldNotes(float totTime)
    {
        List<HoldNote> holdNoteList = new List<HoldNote>();
        float tcounter = 0;
        // float offset = .016f;
        while (tcounter < totTime)
        {
            int noteInd = Random.Range(0, 4);
            float noteTime = tcounter + 2f;
            // create new instance of a note
            // this is the middle sprite
            GameObject newNote = Instantiate(prefab2);
            GameObject newNote2 = Instantiate(prefab);
            GameObject newNote3 = Instantiate(prefab);
            float offset = .10f;
            float variable = 5.0f;
            // edit note to be based on the note type
            switch (noteInd)
            {
                case 0:
                    //  newNote.GetComponent<HoldNote>().initializeNote(HOLD_NOTE_TYPE.HL, NOTE_TYPE.HL, noteTime, noteSprites[12], noteSprites[0]);
                    scaleChange = new Vector3(1, 0, 0);
                    Resize(5.0f, scaleChange, newNote, variable);
                    offset *= variable;
                    newNote.GetComponent<HoldNote>().initializeNote(HOLD_NOTE_TYPE.HL, NOTE_TYPE.HL, noteTime, noteSprites[12], noteSprites[0], newNote2, newNote3, offset);
                    break;
                case 1:
                    //  newNote.GetComponent<HoldNote>().initializeNote(HOLD_NOTE_TYPE.HR, NOTE_TYPE.HR, noteTime, noteSprites[12], noteSprites[1]);
                    scaleChange = new Vector3(1, 0, 0);
                    Resize(5.0f, scaleChange, newNote, variable);
                    offset *= variable;
                    newNote.GetComponent<HoldNote>().initializeNote(HOLD_NOTE_TYPE.HR, NOTE_TYPE.HR, noteTime, noteSprites[12], noteSprites[1], newNote2, newNote3, offset);
                    break;
                case 2:
                    // newNote.GetComponent<HoldNote>().initializeNote(HOLD_NOTE_TYPE.HU, NOTE_TYPE.HU, noteTime, noteSprites[13], noteSprites[2]);
                    scaleChange = new Vector3(0, 1, 0);
                    Resize(5.0f, scaleChange, newNote, variable);
                    offset *= variable;
                    newNote.GetComponent<HoldNote>().initializeNote(HOLD_NOTE_TYPE.HU, NOTE_TYPE.HU, noteTime, noteSprites[13], noteSprites[2], newNote2, newNote3, offset);
                    break;
                case 3:
                    // newNote.GetComponent<HoldNote>().initializeNote(HOLD_NOTE_TYPE.HD, NOTE_TYPE.HD, noteTime, noteSprites[13], noteSprites[3]);
                    scaleChange = new Vector3(0, 1, 0);
                    Resize(5.0f, scaleChange, newNote, variable);
                    offset *= variable;
                    newNote.GetComponent<HoldNote>().initializeNote(HOLD_NOTE_TYPE.HD, NOTE_TYPE.HD, noteTime, noteSprites[13], noteSprites[3], newNote2, newNote3, offset);
                    break;
            }
            // add to the list
            holdNoteList.Add(newNote.GetComponent<HoldNote>());
            tcounter += Random.Range(0.3f, 1.0f);
        }
        return holdNoteList;
    }
    public void Resize(float amount, Vector3 direction, GameObject note, float variable)
    {
        amount *= variable;
        amount += -1;
        note.transform.localScale += amount * direction;
    }

    public List<Note> getNotes(string path)
    {
        List<Note> noteList = new List<Note>();
        List<string> fileLines = File.ReadAllLines(path).ToList();
        for (int i = 0; i < fileLines.Count; i++)
        {
            string[] data = fileLines[i].Split(" ");
            string[] nextData=null;
            if (i!= fileLines.Count-1)
            {
                nextData = fileLines[i+1].Split(" ");
            }
            GameObject newNote = Instantiate(prefab);

            if (nextData!=null && float.Parse(nextData[1]) - float.Parse(data[1]) < 0.05 && checkOpp(data[0], nextData[0]))
            {
                    GameObject newNote2 = Instantiate(prefab);
                    switch (data[0])
                {
                    case "L":
                        newNote.GetComponent<Note>().initializeNote(NOTE_TYPE.L, float.Parse(data[1]), noteSprites[14]);
                        newNote2.GetComponent<Note>().initializeNote(NOTE_TYPE.R, float.Parse(nextData[1]), noteSprites[15]);
                        break;
                    case "R":
                        newNote.GetComponent<Note>().initializeNote(NOTE_TYPE.R, float.Parse(data[1]), noteSprites[15]);
                        newNote2.GetComponent<Note>().initializeNote(NOTE_TYPE.L, float.Parse(nextData[1]), noteSprites[14]);
                        break;
                    case "U":
                        newNote.GetComponent<Note>().initializeNote(NOTE_TYPE.U, float.Parse(data[1]), noteSprites[16]);       
                        newNote2.GetComponent<Note>().initializeNote(NOTE_TYPE.D, float.Parse(nextData[1]), noteSprites[17]);
                        break;
                    case "D":
                        newNote.GetComponent<Note>().initializeNote(NOTE_TYPE.D, float.Parse(data[1]), noteSprites[17]);
                        newNote2.GetComponent<Note>().initializeNote(NOTE_TYPE.U, float.Parse(nextData[1]), noteSprites[16]);
                        break;
                }
                    i++;
                    noteList.Add(newNote.GetComponent<Note>());
                    noteList.Add(newNote2.GetComponent<Note>());
                
                nextData = null;
            }
            else
            {
                switch (data[0])
                {
                    case "L":
                        newNote.GetComponent<Note>().initializeNote(NOTE_TYPE.L, float.Parse(data[1]), noteSprites[0]);
                        break;
                    case "R":
                        newNote.GetComponent<Note>().initializeNote(NOTE_TYPE.R, float.Parse(data[1]), noteSprites[1]);
                        break;
                    case "U":
                        newNote.GetComponent<Note>().initializeNote(NOTE_TYPE.U, float.Parse(data[1]), noteSprites[2]);
                        break;
                    case "D":
                        newNote.GetComponent<Note>().initializeNote(NOTE_TYPE.D, float.Parse(data[1]), noteSprites[3]);
                        break;
                    case "UL":
                        newNote.GetComponent<Note>().initializeNote(NOTE_TYPE.UL, float.Parse(data[1]), noteSprites[4]);
                        break;
                    case "UR":
                        newNote.GetComponent<Note>().initializeNote(NOTE_TYPE.UR, float.Parse(data[1]), noteSprites[5]);
                        break;
                    case "DL":
                        newNote.GetComponent<Note>().initializeNote(NOTE_TYPE.DL, float.Parse(data[1]), noteSprites[6]);
                        break;
                    case "DR":
                        newNote.GetComponent<Note>().initializeNote(NOTE_TYPE.DR, float.Parse(data[1]), noteSprites[7]);
                        break;
                }
                // add to the list
                noteList.Add(newNote.GetComponent<Note>());
            }
            

        }
        return noteList;
    }
    public List<HoldNote> getHoldNotes(string path)
    {
        List<HoldNote> holdNoteList = new List<HoldNote>();
        List<string> fileLines = File.ReadAllLines(path).ToList();
        foreach (string line in fileLines)
        {
            string[] data = line.Split(" ");
            GameObject newNote = Instantiate(prefab2);
            GameObject newNote2 = Instantiate(prefab);
            GameObject newNote3 = Instantiate(prefab);
            float variable = float.Parse(data[2]);
            switch (data[0])
            {
                case "HL":
                    newNote.GetComponent<HoldNote>().initializeNote(HOLD_NOTE_TYPE.HL, NOTE_TYPE.HL, float.Parse(data[1]), noteSprites[12], noteSprites[0], newNote2, newNote3, variable);
                    break;
                case "HR":
                    newNote.GetComponent<HoldNote>().initializeNote(HOLD_NOTE_TYPE.HR, NOTE_TYPE.HR, float.Parse(data[1]), noteSprites[12], noteSprites[1], newNote2, newNote3, variable);
                    break;
                case "HU":
                    newNote.GetComponent<HoldNote>().initializeNote(HOLD_NOTE_TYPE.HU, NOTE_TYPE.HU, float.Parse(data[1]), noteSprites[13], noteSprites[2], newNote2, newNote3, variable);
                    break;
                case "HD":
                    newNote.GetComponent<HoldNote>().initializeNote(HOLD_NOTE_TYPE.HD, NOTE_TYPE.HD, float.Parse(data[1]), noteSprites[13], noteSprites[3], newNote2, newNote3, variable);
                    break;
            }
            // add to the list
            holdNoteList.Add(newNote.GetComponent<HoldNote>());
        }
        return holdNoteList;
    }

    private bool checkOpp(string first, string second)
    {
        if (first == "L" && second == "R")
        {
            return true;
        }
        else if (first == "R" && second == "L")
        {
            return true;
        }
        else if (first == "U" && second == "D")
        {
            return true;

        }
        else if (first == "D" && second == "U")
        {
            return true;

        }
        else 
        {
            return false;
        }

    }
}
