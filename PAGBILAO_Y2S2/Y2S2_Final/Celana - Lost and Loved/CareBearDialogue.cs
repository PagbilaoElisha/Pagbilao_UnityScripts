using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CareBearDialogue : MonoBehaviour
{
    [Header("UI Components")]
    public CanvasGroup dialogueCanvasGroup;       // UI fade control
    public TextMeshProUGUI dialogueText;          // Text component for dialogue

    [Header("Dialogue Settings")]
    [TextArea(2, 5)]
    public string[] dialogueLines;                // Array of dialogue lines
    public float fadeDuration = 0.5f;             // Fade time

    private int currentLineIndex = 0;             // Track current dialogue line
    private bool playerInRange = false;           // Is player near NPC
    private bool dialogueActive = false;          // Is dialogue box currently visible
    private Coroutine fadeCoroutine;              // To prevent overlapping fades

    private void Update()
    {
        if (playerInRange && dialogueActive && Input.GetKeyDown(KeyCode.E))
        {
            DisplayNextLine();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            currentLineIndex = 0;
            dialogueText.text = dialogueLines[currentLineIndex];
            StartFade(true);
            dialogueActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            dialogueActive = false;
            StartFade(false);
        }
    }

    private void DisplayNextLine()
    {
        currentLineIndex++;

        if (currentLineIndex < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLineIndex];
        }
        else
        {
            // All lines done, hide dialogue
            dialogueActive = false;
            StartFade(false);
        }
    }

    private void StartFade(bool fadeIn)
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeCanvasGroup(dialogueCanvasGroup, fadeIn ? 0f : 1f, fadeIn ? 1f : 0f));
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = endAlpha;

        canvasGroup.interactable = endAlpha == 1f;
        canvasGroup.blocksRaycasts = endAlpha == 1f;
    }
}
