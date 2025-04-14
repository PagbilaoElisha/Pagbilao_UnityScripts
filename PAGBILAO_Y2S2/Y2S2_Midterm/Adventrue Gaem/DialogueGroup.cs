using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueGroup
{
    public string conditionName;
    public int priority; // Lower index = higher priority
    public string[] lines;
    // Setting booleans to true
    public bool giveWeapon;
    public bool giveMoney;
    public bool giveBag;
    public bool giveQuest;
}
