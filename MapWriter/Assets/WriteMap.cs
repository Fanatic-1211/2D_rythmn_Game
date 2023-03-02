using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;
public class WriteMap : MonoBehaviour
{

    public enum NOTE_TYPE { U, D, L, R, UL, UR, DL, DR };
    float time = 0;
    // play the song
    // ring segments
    public RingControl ringL;
    public RingControl ringR;
    public RingControl ringU;
    public RingControl ringD;
    // Start is called before the first frame update
    void Start()
    {
        string filePath = Application.dataPath + "/map.txt";
        using (StreamWriter writer = new StreamWriter(filePath, false))
        {
            writer.Write("");
        }

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        tapTracking();
    }


    void tapTracking()
    {
        // flash rings on tap
        if (Input.GetKeyDown("w"))
        {
            ringU.flashRing();
            // Write some text to the file
            WriteToFile("U " + time);
            StartCoroutine(diagonalTracking(0.1f, NOTE_TYPE.U));
        }

        if (Input.GetKeyDown("a"))
        {
            ringL.flashRing();
            WriteToFile("L " + time);
            StartCoroutine(diagonalTracking(0.1f, NOTE_TYPE.L));
        }

        if (Input.GetKeyDown("s"))
        {
            ringD.flashRing();
            WriteToFile("D " + time);
            StartCoroutine(diagonalTracking(0.1f, NOTE_TYPE.D));
        }

        if (Input.GetKeyDown("d"))
        {
            ringR.flashRing();
            WriteToFile("R " + time);
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

                    RemoveLastLine();
                    WriteToFile("UL " + time);
                    break;
                }
                // RIGHT
                if (Input.GetKeyDown("d"))
                {
                    RemoveLastLine();
                    WriteToFile("UR " + time);
                    break; 
                }
            }
            else if (type == NOTE_TYPE.D)
            {
                // check L or R
                // LEFT
                if (Input.GetKeyDown("a"))
                {
                    RemoveLastLine();
                    WriteToFile("DL " + time);
                    break;
                }
                // RIGHT
                if (Input.GetKeyDown("d"))
                {
                    RemoveLastLine();
                    WriteToFile("DR " + time);
                    break;
                }
            }
            else if (type == NOTE_TYPE.L)
            {
                // check U or D
                // UP
                if (Input.GetKeyDown("w"))
                {
                    RemoveLastLine();
                    WriteToFile("UL " + time);
                    break;
                }
                // DOWN
                if (Input.GetKeyDown("s"))
                {
                    RemoveLastLine();
                    WriteToFile("DL " + time);
                    break;
                }
            }
            else if (type == NOTE_TYPE.R)
            {
                // check U or D
                // UP
                if (Input.GetKeyDown("w"))
                {
                    RemoveLastLine();
                    WriteToFile("UR " + time);
                    break;
                }
                // DOWN
                if (Input.GetKeyDown("s"))
                {
                    RemoveLastLine();
                    WriteToFile("DR " + time);
                    break;
                }
            }

            duration -= Time.deltaTime;

            yield return null;
        }
    }
    private void WriteToFile(string text)
    {
        string filePath = Application.dataPath + "/map.txt";
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine(text);
        }
    }
    private void RemoveLastLine()
    {
        string filePath = Application.dataPath + "/map.txt";
        string[] lines = File.ReadAllLines(filePath);
        Array.Resize(ref lines, lines.Length - 1);
        File.WriteAllLines(filePath, lines);
    }

    void OnDestroy()
    {
        string filePath = Application.dataPath + "/map.txt";
        string[] lines = File.ReadAllLines(filePath);
        string[] newArray = lines.Distinct().ToArray();
        File.WriteAllLines(filePath, newArray);
    }
}
