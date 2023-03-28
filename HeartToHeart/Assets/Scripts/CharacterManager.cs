using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
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
}
public class CharacterManager : MonoBehaviour
{
    // public List<Character> characters = new List<Character>();
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // if Contains [Character]
    // substring until delimiter of [ or \n
    // if Contains [Expr]
}
