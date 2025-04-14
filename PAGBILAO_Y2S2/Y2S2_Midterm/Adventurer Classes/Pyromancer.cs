using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pyromancer : Elementalist
{
    protected override void Awake()
    {
        base.Awake();
        abilities.Add("Ignite");
    }

    protected override void OnButtonClick()
    {
        Debug.Log("Pyromancer picked");
        base.OnButtonClick();
    }
}