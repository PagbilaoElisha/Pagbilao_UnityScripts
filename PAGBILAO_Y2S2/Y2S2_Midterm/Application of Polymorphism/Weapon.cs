using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected virtual void Start()
    {
        Attack();
    }
    protected virtual void Attack() { }

    public void Attack(int dmg)
    {
        Debug.Log($"He dealt {dmg} damage");
    }
}