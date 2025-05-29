using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Priest : Cleric
{
    protected override void Awake()
    {
        base.Awake();
        abilities.Add("Heal");
    }

    protected override void OnButtonClick()
    {
        Debug.Log("Priest picked");
        base.OnButtonClick();
    }
}
