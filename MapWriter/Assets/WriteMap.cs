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
    public AudioSource source;
    public AudioClip clip;
    // ring segments
    public RingControl ringL;
    public RingControl ringR;
    public RingControl ringU;
    public RingControl ringD;
    float heldTime = 0f;
    float heldTime2 = 0f;
    float heldTime3 = 0f;
    float heldTime4 = 0f;
    float maxHeldTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        string filePath = Application.dataPath + "/map.txt";
        using (StreamWriter writer = new StreamWriter(filePath, false))
        {
            writer.Write("");
        }
        filePath = Application.dataPath + "/holdMap.txt";
        using (StreamWriter writer = new StreamWriter(filePath, false))
        {
            writer.Write("");
        }
        source.Play();
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
            heldTime = Time.time;
            StartCoroutine(diagonalTracking(0.1f, NOTE_TYPE.U));
        }
        
        else if (Input.GetKeyUp("w"))
        {
           
            heldTime = Time.time-heldTime;
            float temp = time - heldTime;
            WriteToHoldFile("HU " + temp + " " + heldTime);
            if (heldTime < maxHeldTime)
            {
                RemoveLastHoldLine();

            }
            else
            {
                RemoveLastLine();
            }

        }

        if (Input.GetKeyDown("a"))
        {
            ringL.flashRing();
            WriteToFile("L " + time);
            heldTime2 = Time.time;
            StartCoroutine(diagonalTracking(0.1f, NOTE_TYPE.L));
        }
        else if (Input.GetKeyUp("a"))
        {
            heldTime2 = Time.time - heldTime2;
            float temp = time - heldTime2;
            WriteToHoldFile("HL " + temp + " " + heldTime2);
            if (heldTime2 < maxHeldTime)
            {
                RemoveLastHoldLine();

            }
            else
            {
                RemoveLastLine();
            }
        }

        if (Input.GetKeyDown("s"))
        {
            ringD.flashRing();
            WriteToFile("D " + time);
            heldTime3 = Time.time;
            StartCoroutine(diagonalTracking(0.1f, NOTE_TYPE.D));
        }

        else if (Input.GetKeyUp("s"))
        {
            heldTime3 = Time.time - heldTime3;
            float temp = time - heldTime3;
            WriteToHoldFile("HD " + temp + " " + heldTime3);
            if (heldTime3 < maxHeldTime)
            {
                RemoveLastHoldLine();
         
            }
            else
            {
                RemoveLastLine();
            }
        }

        if (Input.GetKeyDown("d"))
        {
            ringR.flashRing();
            WriteToFile("R " + time);
            heldTime4 = Time.time;
            StartCoroutine(diagonalTracking(0.1f, NOTE_TYPE.R));
        }
        else if (Input.GetKeyUp("d"))
        {
            heldTime4 = Time.time - heldTime4;
            float temp = time - heldTime4;
            WriteToHoldFile("HR " + temp + " " + heldTime4);
            if (heldTime4 < maxHeldTime)
            {
                RemoveLastHoldLine();

            }
            else
            {
                RemoveLastLine();
            }
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
    private void WriteToHoldFile(string text)
    {
        string filePath = Application.dataPath + "/holdMap.txt";
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
    private void RemoveLastHoldLine()
    {
        string filePath = Application.dataPath + "/holdMap.txt";
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
