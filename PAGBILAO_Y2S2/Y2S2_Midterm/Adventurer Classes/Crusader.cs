using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crusader : Paladin
{
    protected override void Awake()
    {
        base.Awake();
        abilities.Add("Judge");
    }

    protected override void OnButtonClick()
    {
        Debug.Log("Crusader picked");
        base.OnButtonClick();
    }
}
