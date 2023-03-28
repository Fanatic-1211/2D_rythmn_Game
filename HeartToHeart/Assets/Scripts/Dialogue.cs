using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    // object to fade between scenes
    public Image fader;

    // 0 - APARTMENT
    // 1 - SUBWAY
    // 2 - OFFICE
    public List<Texture> backgrounds;
    public GameObject currBg;

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

        // read the next line!
        if (VNScript[index].Contains("[Line]"))
        {
            // clear current text
            txt.text = "";

            string line = VNScript[index].Substring(7);
            StartCoroutine(Type(line));
        }
        else if (VNScript[index].Contains("[Background]"))
        {
            // do we need to fade?
            if (VNScript[index].Contains("[F]"))
            {
                // change bg!
                string bgName = VNScript[index].Substring(17);
                ChangeBG(bgName, true);
            }
            else
            {
                // change bg!
                string bgName = VNScript[index].Substring(13);
                ChangeBG(bgName, false);
            }
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
    void ChangeBG(string bg, bool fade)
    {
        float length = 0f;

        if (fade)
            length = 0.5f;

        switch(bg)
        {
            case "Apartment":
                StartCoroutine(ChangeBG(backgrounds[0], length));
                break;
            case "Subway":
                StartCoroutine(ChangeBG(backgrounds[1], length));
                break;
            case "Office":
                StartCoroutine(ChangeBG(backgrounds[2], length));
                break;
        }
    }

    IEnumerator ChangeBG(Texture bgChange, float duration)
    {
        float time = 0;

        // fade to black
        while (time < duration)
        {
            fader.color = new Color(0, 0, 0, (time / duration));
            time += Time.deltaTime;

            yield return null;
        }
        fader.color = new Color(0, 0, 0, 1);

        // change bg texture
        currBg.GetComponent<RawImage>().texture = bgChange;

        // fade to transparent
        time = 0;
        while (time < duration)
        {
            fader.color = new Color(0, 0, 0, 1f - (time / duration));
            time += Time.deltaTime;

            yield return null;
        }
        fader.color = new Color(0, 0, 0, 0);
    }
}
