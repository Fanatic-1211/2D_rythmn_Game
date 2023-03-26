using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI txt;
    public string[] lines;
    public float txtSpd;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        // txtSpd = 1; // based on speed
        txt.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (txt.text == lines[index])
            {
                NextLine();
            } 
            else
            {
                StopAllCoroutines();
                txt.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(Type());
    }

    IEnumerator Type()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            txt.text += c;
            yield return new WaitForSeconds(txtSpd);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            txt.text = string.Empty;
            StartCoroutine(Type());
        } 
        else
        {
            gameObject.SetActive(false);
        }
    }
}
