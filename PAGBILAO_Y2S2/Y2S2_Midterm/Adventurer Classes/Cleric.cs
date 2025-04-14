using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleric : Adventurer
{
    protected override void Awake()
    {
        base.Awake();
        abilities.Add("Bless");
    }

    protected override void OnButtonClick()
    {
        Debug.Log("Cleric picked");
        base.OnButtonClick();
    }
}