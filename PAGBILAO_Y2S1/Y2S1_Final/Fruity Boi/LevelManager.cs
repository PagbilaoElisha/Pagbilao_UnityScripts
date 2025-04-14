using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject menu; // Reference to the menu object.
    public GameObject player; // Reference to the player object.
    public Transform playerStartPosition; // Player's starting position.
    public GameObject collectible; // Reference to the collectible item.
    public GameObject nextLevel;

    private MenuManager menuManager;

    void Start()
    {
        menuManager = menu.GetComponent<MenuManager>();
        ResetLevel();
    }

    void Update()
    {
        
    }

    public void ResetLevel()
    {
        player.transform.position = playerStartPosition.position;
        collectible.SetActive(true);
    }

    public void OnWin()
    {
        menuManager.UnlockNextLevel();
        nextLevel.SetActive(true);
        gameObject.SetActive(false); // Deactivate this level.
        collectible.SetActive(false);
    }

    public void ReturnToMenu()
    {
        menu.SetActive(true);
        gameObject.SetActive(false);
    }
}
