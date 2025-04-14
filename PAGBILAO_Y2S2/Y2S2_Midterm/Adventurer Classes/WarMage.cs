using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarMage : Mystic
{
    protected override void Awake()
    {
        base.Awake();
        abilities.Add("Overload");
    }

    protected override void OnButtonClick()
    {
        Debug.Log("War mage picked");
        base.OnButtonClick();
    }
}
