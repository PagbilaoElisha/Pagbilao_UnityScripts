using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mystic : Sorceress
{
    protected override void Awake()
    {
        base.Awake();
        abilities.Add("Focus");
    }

    protected override void OnButtonClick()
    {
        Debug.Log("Mystic picked");
        base.OnButtonClick();
    }
}

