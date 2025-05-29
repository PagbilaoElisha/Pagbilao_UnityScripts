using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    public Transform player;
    public Transform enemy;
    public static bool loadSavedPositions = false; // Flag for loading saved data

    void Start()
    {
        if (loadSavedPositions) // Check if loading from save
        {
            LoadPositions();
            loadSavedPositions = false; // Reset flag after loading
        }
        else
        {
            Debug.Log("Using default positions.");
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) // Save positions
        {
            SavePositions();
        }

        if (Input.GetKeyDown(KeyCode.L)) // Load positions
        {
            LoadPositions();
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene("Menu");
        }
    }

    public void SavePositions()
    {
        PlayerPrefs.SetFloat("PlayerX", player.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.position.y);
        PlayerPrefs.SetFloat("PlayerZ", player.position.z);

        PlayerPrefs.SetFloat("EnemyX", enemy.position.x);
        PlayerPrefs.SetFloat("EnemyY", enemy.position.y);
        PlayerPrefs.SetFloat("EnemyZ", enemy.position.z);
    }

    public void LoadPositions()
    {
        if (PlayerPrefs.HasKey("PlayerX"))
        {
            player.position = new Vector3(
                PlayerPrefs.GetFloat("PlayerX"),
                PlayerPrefs.GetFloat("PlayerY"),
                PlayerPrefs.GetFloat("PlayerZ")
            );

            enemy.position = new Vector3(
                PlayerPrefs.GetFloat("EnemyX"),
                PlayerPrefs.GetFloat("EnemyY"),
                PlayerPrefs.GetFloat("EnemyZ")
            );
        }
    }
}
