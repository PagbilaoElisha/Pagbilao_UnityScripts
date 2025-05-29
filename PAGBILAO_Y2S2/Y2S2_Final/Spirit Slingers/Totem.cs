using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{
    private void OnDestroy()
    {
        LevelManager manager = FindObjectOfType<LevelManager>();
        manager?.RegisterTotemDestroyed();
    }
}