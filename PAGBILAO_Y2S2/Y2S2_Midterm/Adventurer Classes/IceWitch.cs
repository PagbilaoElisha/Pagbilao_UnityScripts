using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWitch : Elementalist
{
    protected override void Awake()
    {
        base.Awake();
        abilities.Add("Freeze");
    }

    protected override void OnButtonClick()
    {
        Debug.Log("Ice witch picked");
        base.OnButtonClick();
    }
}
