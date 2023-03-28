using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    private string name;
    private float txtSpd;
    private Color txtClr;
    public Sprite nametag;

    public Character(string name, float speed, Color color)
    {
        this.name = name;
        txtSpd = speed;
        txtClr = color;
    }

    public string getName()
    {
        return name;
    }
}
public class CharacterManager : MonoBehaviour
{
    public List<Character> characters = new List<Character>();
    public List<Sprite> nametag = new List<Sprite>();
    public List<Sprite> aExpr = new List<Sprite>();
    public List<Sprite> aPose = new List<Sprite>();
    public List<Sprite> wExpr = new List<Sprite>();
    public List<Sprite> wPose = new List<Sprite>();
    public List<Sprite> cExpr = new List<Sprite>();
    public List<Sprite> cPose = new List<Sprite>();

    public List<Sprite> misc = new List<Sprite>();

    public Character activeCharacter;

    // Start is called before the first frame update
    void Start()
    {
        Character a = new Character("Aria", 0.1f, new Color(0.264f, 0.078f, 0.078f, 1));
        Character w = new Character("Wesley", 0.08f, new Color(0.176f, 0.095f, 0.302f, 1));
        Character c = new Character("Carrie", 0.12f, new Color(0.179f, 0.253f, 0.255f, 1));
        Character x = new Character("Scaramouche", 0.1f, Color.black); // (because it's a little silhouetto of a man)

        activeCharacter = a;

        characters.Add(a);
        characters.Add(w);
        characters.Add(c);
        characters.Add(x);
    }

    // Update is called once per frame
    void Update()
    {
        
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

    //string stringBeforeChar = authors.Substring(0, authors.IndexOf(","));
    //string stringAfterChar = authors.Substring(authors.IndexOf(",") + 2);

    // if Contains [Character]
    // substring until delimiter of [ or \n
    // if Contains [Expr]
}
