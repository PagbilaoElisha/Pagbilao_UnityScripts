using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inquisitor : Priest
{
    protected override void Awake()
    {
        base.Awake();
        abilities.Add("Purge");
    }

    protected override void OnButtonClick()
    {
        Debug.Log("Inquisitor picked");
        base.OnButtonClick();
    }
}