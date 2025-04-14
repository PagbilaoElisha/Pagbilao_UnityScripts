using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosMage : Mystic
{
    protected override void Awake()
    {
        base.Awake();
        abilities.Add("Disrupt");
    }

    protected override void OnButtonClick()
    {
        Debug.Log("Chaos mage picked");
        base.OnButtonClick();
    }
}
