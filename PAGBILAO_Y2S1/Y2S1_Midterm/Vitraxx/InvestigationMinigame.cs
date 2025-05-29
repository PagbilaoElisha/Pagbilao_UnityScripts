using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InvestigationMinigame : MonoBehaviour
{
    public List<Button> hiddenButtons;         // List of buttons representing hidden objects
    public List<TMP_Text> cardTexts;           // List of TMP texts to display object names
    private int foundCount = 0;                // Counter to track found items
    public GameObject mysteryGame;
    public GameObject nextScene;

    private void Start()
    {
        SetupButtons();
    }

    // Sets up each button to trigger ObjectFound on click
    void SetupButtons()
    {
        if (hiddenButtons.Count != cardTexts.Count)
        {
            Debug.LogError("Button and text lists must be the same length.");
            return;
        }

        for (int i = 0; i < hiddenButtons.Count; i++)
        {
            int index = i; // Capture index for lambda
            hiddenButtons[i].onClick.AddListener(() => ObjectFound(index));
        }
    }

    // Called when a hidden object is found
    void ObjectFound(int index)
    {
        // Deactivate the clicked button and corresponding text
        hiddenButtons[index].gameObject.SetActive(false);
        cardTexts[index].gameObject.SetActive(false);

        foundCount++;

        // Check if all objects are found, then move to the next scene
        if (foundCount >= hiddenButtons.Count)
        {
            mysteryGame.SetActive(false);
            nextScene.SetActive(true);
        }
    }
}