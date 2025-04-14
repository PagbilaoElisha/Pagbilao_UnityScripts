using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    protected override void Start()
    {
        base.Start();
        Attack(50);
    }
    protected override void Attack()
    {
        Debug.Log("The knight slashes");
    }
}
