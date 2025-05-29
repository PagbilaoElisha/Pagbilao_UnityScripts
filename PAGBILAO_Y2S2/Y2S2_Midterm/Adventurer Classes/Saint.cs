using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saint : Priest
{
    protected override void Awake()
    {
        base.Awake();
        abilities.Add("Sanctify");
    }

    protected override void OnButtonClick()
    {
        Debug.Log("Saint picked");
        base.OnButtonClick();
    }
}