using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elementalist : Sorceress
{
    protected override void Awake()
    {
        base.Awake();
        abilities.Add("Conjure");
    }

    protected override void OnButtonClick()
    {
        Debug.Log("Elementalist picked");
        base.OnButtonClick();
    }
}
