using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int enemyCount = 10; // Number of enemies in scene
    public bool hasWeapon = false;
    public bool hasMoney = false;
    public bool hasBag = false;
    public bool hasQuest = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void EnemyDefeated()
    {
        enemyCount--;
        if (enemyCount <= 0)
        {
            EndGame(true);
        }
    }

    public void EndGame(bool win)
    {
        // Change based on how End scene is entered
        PlayerPrefs.SetString("EndResult", win ? "YOU WIN!!!" : "YOU DIED");
        PlayerPrefs.SetString("EndSound", win ? "y" : "n");
        SceneManager.LoadScene("End");
    }
}
