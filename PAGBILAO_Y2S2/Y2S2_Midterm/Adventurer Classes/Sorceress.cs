using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorceress : Adventurer
{
    protected override void Awake()
    {
        base.Awake();
        abilities.Add("Channel");
    }

    protected override void OnButtonClick()
    {
        Debug.Log("Sorceress picked");
        base.OnButtonClick();
    }
}