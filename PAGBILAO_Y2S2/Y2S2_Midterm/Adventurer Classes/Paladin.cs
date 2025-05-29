using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paladin : Cleric
{
    protected override void Awake()
    {
        base.Awake();
        abilities.Add("Smite");
    }

    protected override void OnButtonClick()
    {
        Debug.Log("Paladin picked");
        base.OnButtonClick();
    }
}

