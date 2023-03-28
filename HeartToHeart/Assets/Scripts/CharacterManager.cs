using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    private string name;
    private float txtSpd;
    private Color txtClr;
    public Texture nametag;

    public Character(string name, float speed, Color color)
    {
        this.name = name;
        txtSpd = speed;
        txtClr = color;
    }
    public string charName()
    {
        return name;
    }
    public float txtSpeed()
    {
        return txtSpd;
    }
    public Color txtCol()
    {
        return txtClr;
    }
}
public class CharacterManager : MonoBehaviour
{
    // list of expressions for each character
    public List<Sprite> nametag;
    public List<Sprite> aExpr;
    public List<Sprite> aPose;
    public List<Sprite> wExpr;
    public List<Sprite> wPose;
    public List<Sprite> cExpr;
    public List<Sprite> cPose;

    // misc sprites for minor characters and events
    public List<Sprite> misc;

    // currently active character
    public Character activeCharacter;

    // list of character objects
    Character a;
    Character w;
    Character c;
    Character x;

    // Start is called before the first frame update
    void Start()
    {
        // define characters, type speed, and color type
        a = new Character("Aria", 0.1f, new Color(0.264f, 0.078f, 0.078f, 1));
        w = new Character("Wesley", 0.08f, new Color(0.176f, 0.095f, 0.302f, 1));
        c = new Character("Carrie", 0.12f, new Color(0.179f, 0.253f, 0.255f, 1));
        x = new Character("Scaramouche", 0.1f, Color.black); // (because it's a little silhouetto of a man)

        // set default character to aria
        activeCharacter = a;

        characters.Add(a);
        characters.Add(w);
        characters.Add(c);
        characters.Add(x);
    }

    // Update is called once per frame
    void Update() {}

    void ResetActive()
    {
        // changes all current sprites to their mouth closed components

        // sets active char to null
        activeCharacter = null;
    }

    public void ParseCharacter (string cmd)
    {
        // set the character to be active
        string input = cmd.Substring(1, cmd.IndexOf(" "));
        cmd = cmd.Substring(cmd.IndexOf(" ") + 1);
        foreach (Character letter in characters)
        {
            if (letter.getName() == input)
            {
                activeCharacter = letter;
            }
        }
        // LILY FUNCTION <3 open mouth?

        if (cmd.Contains("[EXPR]"))
        {
            input = cmd.Substring(0, cmd.IndexOf(" "));
            cmd = cmd.Substring(cmd.IndexOf(" ") + 1);
        }
        // LILY FUNCTION <3 set expression to input
        
        if (cmd.Contains("[POSE]"))
        {
            input = cmd.Substring(0, cmd.IndexOf(" "));
        }
        // LILY FUNCTION <3 set pose to input
    }
}
