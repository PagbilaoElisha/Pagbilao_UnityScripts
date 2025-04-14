using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    public List<DialogueGroup> dialogueGroups = new List<DialogueGroup>();
    public float talkRange = 3f;
    public float rotationSpeed = 50f;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI talkPrompt; // For currently unresolved issues, each NPC's talk prompt must be different in the Hierarchy
    private Transform player;
    private PlayerController playerController;
    private Quaternion originalRotation;
    private Quaternion targetRotation;
    private int dialogueIndex = 0;
    private DialogueGroup currentGroup;
    private bool inConversation = false;
    private bool hasInteracted = false;
    private bool canAdvance = false;
    private bool rotateToPlayer = false;
    private bool rotateToOriginal = false;
    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        playerController = player.GetComponent<PlayerController>();
        originalRotation = transform.rotation;
        dialogueText.gameObject.SetActive(false);
        talkPrompt.gameObject.SetActive(false);
        GameManager.Instance.hasWeapon = false;
        GameManager.Instance.hasMoney = false;
        GameManager.Instance.hasBag = false;
        GameManager.Instance.hasQuest = false;
        hasInteracted = false;
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        bool lookingAtNPC = IsLookingAtThisNPC();
        if (!inConversation && distanceToPlayer <= talkRange && lookingAtNPC)
        {
            talkPrompt.text = "Talk";
            talkPrompt.gameObject.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {
                StartConversation();
            }
        }
        else if (!inConversation)
        {
            talkPrompt.gameObject.SetActive(false);
        }
        if (inConversation && Input.GetMouseButtonDown(0) && canAdvance)
        {
            AdvanceDialogue();
        }

        if (rotateToPlayer)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.5f)
            {
                rotateToPlayer = false;
            }
        }
        if (rotateToOriginal)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, originalRotation, rotationSpeed * Time.deltaTime);
            if (Quaternion.Angle(transform.rotation, originalRotation) < 0.5f)
            {
                rotateToOriginal = false;
            }
        }
    }

    void StartConversation()
    {
        SFXManager.Instance.PlaySFX(SFXManager.Instance.talk);
        inConversation = true;
        // Find appropriate dialogue based on conditions
        dialogueIndex = 0;
        currentGroup = GetBestDialogueGroup();
        // Lock player movement
        playerController.LockControl(true);
        // Start NPC interaction
        talkPrompt.gameObject.SetActive(false);
        FacePlayer();
        dialogueText.gameObject.SetActive(true);
        dialogueText.text = currentGroup.lines[dialogueIndex];
        hasInteracted = true;
        // Ensuring line 0 in a dialogue group shows, since Update unintentionally skips it without
        canAdvance = false;
        StartCoroutine(EnableAdvanceAfterClick());
    }

    IEnumerator EnableAdvanceAfterClick()
    {
        yield return new WaitForEndOfFrame();
        canAdvance = true;
    }

    void AdvanceDialogue()
    {
        dialogueIndex++;
        if (dialogueIndex < currentGroup.lines.Length)
        {
            dialogueText.text = currentGroup.lines[dialogueIndex];
        }
        else // If current dialogue is last line, dialogueIndex is currentGroup.lines.Length during the if-else
        {
            EndConversation();
        }
    }

    void EndConversation()
    {
        // Condition-based effects
        if (currentGroup.giveWeapon) GameManager.Instance.hasWeapon = true;
        if (currentGroup.giveMoney) GameManager.Instance.hasMoney = true;
        if (currentGroup.giveBag) GameManager.Instance.hasBag = true;
        if (currentGroup.giveQuest) GameManager.Instance.hasQuest = true;
        // End dialogue
        dialogueText.gameObject.SetActive(false);
        inConversation = false;
        currentGroup = null;
        // Return NPC to original orientation
        rotateToOriginal = true;
        rotateToPlayer = false;
        // Activate player control
        playerController.LockControl(false);
    }

    void FacePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;
        if (direction != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(direction);
            rotateToPlayer = true;
            rotateToOriginal = false;
        }
    }

    bool IsLookingAtThisNPC()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, talkRange))
        {
            return hit.collider != null && hit.collider.gameObject == gameObject;
        }
        return false;
    }

    DialogueGroup GetBestDialogueGroup()
    {
        DialogueGroup bestGroup = null;
        foreach (var group in dialogueGroups)
        {
            if (ConditionMet(group.conditionName))
            {
                if (bestGroup == null || group.priority < bestGroup.priority)
                    bestGroup = group;
            }
        }
        return bestGroup;
    }

    bool ConditionMet(string condition)
    {
        if (string.IsNullOrWhiteSpace(condition))
            return true;

        string[] parts = condition.Split('&'); // Using & to combine Has and FirstTime type cases
        foreach (string part in parts)
        {
            switch (part.Trim())
            {
                case "HasWeapon": if (!GameManager.Instance.hasWeapon) return false; break;
                case "HasMoney": if (!GameManager.Instance.hasMoney) return false; break;
                case "HasBag": if (!GameManager.Instance.hasBag) return false; break;
                case "HasQuest": if (!GameManager.Instance.hasQuest) return false; break;
                case "FirstTime": if (hasInteracted) return false; break;
                case "NotFirstTime": if (!hasInteracted) return false; break;
                default: Debug.LogWarning($"Unknown condition part: '{part}'"); break;
            }
        }
        return true;
    }
}
