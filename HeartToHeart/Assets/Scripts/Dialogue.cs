using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System.IO;

public class Dialogue : MonoBehaviour
{
    // information for the textbox & visuals
    public TextMeshProUGUI txt;
    public float txtSpd;
    public GameObject textbox;
    bool crawling = false;

    // information for the data being read from the file
    private List<string> VNScript;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        // txtSpd = 1; // based on speed

        ReadScript(1);

        index = -1;
        ReadNextLine();
    }

    void ReadScript(int sceneNumber)
    {
        // read lines in from the file, store in a data type
        VNScript = File.ReadAllLines("Assets/VN_Files/scene" + sceneNumber + ".vn").ToList();
    }

    // Update is called once per frame
    void Update()
    {
        // check to see if button was pressed
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            // if true set text to be finished immediately
            if (crawling)
                crawling = false;
            else
                ReadNextLine();
        }
    }

    void ReadNextLine()
    {
        index++;

        if (index > VNScript.Count - 1)
        {
            textbox.SetActive(false);
            return;
        }

        if (VNScript[index].Contains("[Line]"))
        {
            // clear current text
            txt.text = "";

            string line = VNScript[index].Substring(7);
            StartCoroutine(Type(line));
        }
        
    }

    IEnumerator Type(string line)
    {
        crawling = true;

        foreach (char c in line.ToCharArray())
        {
            // at any point, if 'crawling' is turned off, stop early
            if (!crawling)
            {
                txt.text = line;
                break;
            }

            // if not, remain crawlin!
            txt.text += c;
            yield return new WaitForSeconds(txtSpd);
        }

        crawling = false;
    }

    IEnumerator ChangeBG()
    {
        yield return null;
    }
}