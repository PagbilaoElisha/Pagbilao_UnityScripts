using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private bool checking = false;

    void Start()
    {
        // Do not disable game object so that Update can work; renderer and collider can be individually disabled without interrupting the ability of the goal to Update
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    void Update()
    {
        if (!checking) // Avoid multiple StartCoroutines
        {
            checking = true;
            StartCoroutine(CheckGoal()); // Using Coroutine so that it doesn't continuously check for enemies once renderer and collider are active
        }
    }

    IEnumerator CheckGoal()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            int enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

            if (enemies == 0)
            {
                GetComponent<SpriteRenderer>().enabled = true;
                GetComponent<Collider2D>().enabled = true;
                yield break;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            int buildIndex = SceneManager.GetActiveScene().buildIndex;
            int levelIndex = buildIndex - 1;
            
            // Unlock next level
            if (levelIndex + 1 < GameManager.Instance.levelUnlocked.Length)
            {
                GameManager.Instance.UnlockLevel(levelIndex + 1);
            }

            if (buildIndex >= SceneManager.sceneCountInBuildSettings - 1) // If this is the last level, return to menu
            {
                GameManager.Instance.BackToMenu();
            }
            else // Else, load next level
            {
                SceneManager.LoadScene(buildIndex + 1);
            }
        }
    }
}
