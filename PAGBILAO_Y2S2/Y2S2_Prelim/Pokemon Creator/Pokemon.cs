using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pokemon
{
    public string name;
    public int level;
    public Element element;
    public Gender gender;

    public Pokemon(string name, int level, Element element, Gender gender)
    {
        this.name = name;
        this.level = level;
        this.element = element;
        this.gender = gender;
    }
}

public enum Element {Normal, Fighting, Flying, Poison, Ground, Rock, Bug, Ghost, Steel,
Fire, Water, Grass, Electric, Psychic, Ice, Dragon, Dark, Fairy}
public enum Gender { Male, Female}