using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardian : Paladin
{
    protected override void Awake()
    {
        base.Awake();
        abilities.Add("Shield");
    }

    protected override void OnButtonClick()
    {
        Debug.Log("Guardian picked");
        base.OnButtonClick();
    }
}
