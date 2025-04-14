using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character
{
    public string name;
    public string origin;
    public string description;
    public Focus focus;
    public Sprite action;
    public Sprite flag;

    public Character(string name, string origin, string description, Focus focus, Sprite action, Sprite flag)
    {
        this.name = name;
        this.origin = origin;
        this.description = description;
        this.focus = focus;
        this.action = action;
        this.flag = flag;
    }

    public Character()
    {
        name = "UNKNOWN";
        origin = "UNKNOWN";
        description = "UNKNOWN";
        focus = Focus.Hybrid;
        action = null;
        flag = null;
    }
}

public enum Focus { Striking, Grappling, Hybrid }
