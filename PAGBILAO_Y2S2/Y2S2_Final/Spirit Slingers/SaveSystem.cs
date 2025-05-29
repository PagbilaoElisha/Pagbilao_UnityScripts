using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem
{
    public static void MarkLevelCompleted(int levelIndex)
    {
        int unlocked = PlayerPrefs.GetInt("UnlockedLevel", 1);
        if (levelIndex + 1 > unlocked)
        {
            PlayerPrefs.SetInt("UnlockedLevel", levelIndex + 1);
            PlayerPrefs.Save();
        }
    }
}