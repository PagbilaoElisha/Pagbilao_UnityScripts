using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypingTestV2 : MonoBehaviour
{
    // ------------------------- VARIABLES -------------------------
    [Header("Reference")]
    public TriggerPuzzleV2 triggerPuzzleV2; // Reference to the TriggerPuzzle.cs script
    public GameObject floatingPanel; // Reference to the panel
    public List<TextMeshProUGUI> modifiedTexts = new List<TextMeshProUGUI>(); // Reference to the TextMeshProUGUI components
    public List<string> appendedTexts = new List<string>(); // Reference to the already appended texts
    public Player player; // Reference to the player script

    [Header("Regulator")]
    public int unlockedDoorChecker = 0; // Checker for the unlocked state

    // ------------------------- FUNCTIONS -------------------------
    void Update()
    {
        // Check if all doors are unlocked
        if (appendedTexts.Count == modifiedTexts.Count)
        {
            UnlockSomething(); // Call the unlock function
            DeactivatePanel(); // Deactivate the panel
            EnablePlayerMovement(); // Re-enable player movement
        }
    }

    public void CheckInput(TMP_InputField input) // Check the user input
    {
        string tempInput = input.text; // Store the input text
        tempInput.Trim(); // Remove any leading or trailing whitespace

        // Check if input matches the required text
        for (var i = 0; i < modifiedTexts.Count; i++)
        {
            if (modifiedTexts[i].text == tempInput && !appendedTexts.Contains(tempInput)) // Check if the input matches the first text
            {
                Debug.Log($"Case {i}");
                modifiedTexts[i].color = Color.red; // Change color to red
                appendedTexts.Add(tempInput); // Add the text to the appended list
                unlockedDoorChecker++; // Increment the unlocked door checker
            }
        }
    }

    public void ActivatePanel() // Opens the prompt
    {
        floatingPanel.SetActive(true); // Activate the panel
        DisablePlayerMovement(); // Disable player movement
    }

    public void DeactivatePanel() // Closes the prompt
    {
        floatingPanel.SetActive(false); // Deactivate the panel
    }

    public void UnlockSomething() // Unlocks the door
    {
        triggerPuzzleV2.UnlockPath(); // Call the unlock function
        Debug.Log("Unlocking something");
    }

    // Disable player movement
    private void DisablePlayerMovement()
    {
        if (player != null)
        {
            player.canMove = false; // Disable player movement
        }
    }

    // Re-enable player movement
    private void EnablePlayerMovement()
    {
        if (player != null)
        {
            player.canMove = true; // Re-enable player movement
        }
    }
}
