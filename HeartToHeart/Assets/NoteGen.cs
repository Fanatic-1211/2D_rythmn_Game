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

    public List<Note> getNotes(string path)
    {
        List<Note> noteList = new List<Note>();
        List<string> fileLines = File.ReadAllLines(path).ToList();
        foreach (string line in fileLines)
        {
            string[] data = line.Split(" ");
            GameObject newNote = Instantiate(prefab);
            switch(data[0]){
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
        return noteList;
    }
}
