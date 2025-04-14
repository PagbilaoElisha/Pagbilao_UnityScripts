using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GDStudent : EMCStudent // Game Development students; under EMC
{
    public void MakeGame()
    {
        Debug.Log($"{name} creates a game");
    }
}
