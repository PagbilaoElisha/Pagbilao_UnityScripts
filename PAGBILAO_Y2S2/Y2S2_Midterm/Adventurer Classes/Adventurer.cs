using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Adventurer : MonoBehaviour
{
    public Button button;
    public Adventurer parent;
    public List<Adventurer> children = new List<Adventurer>();
    protected List<string> abilities;

    private static List<Adventurer> allButtons = new List<Adventurer>();
    private static Adventurer selectedButton = null;
    private Color originalColor;

    [SerializeField] private TextMeshProUGUI abilityTextUI;
    private static TextMeshProUGUI abilityDisplay;

    protected virtual void Awake()
    {
        if (button == null)
        {
            button = GetComponent<Button>();
        }

        if (abilityDisplay == null && abilityTextUI != null)
        {
            abilityDisplay = abilityTextUI;
        }

        abilities = new List<string>();
        abilities.Add("Strike");

        originalColor = button.image.color;
        allButtons.Add(this);
        Invoke(nameof(SetupHierarchy), 0.1f);
    }

    private void SetupHierarchy()
    {
        Transform parentTransform = transform.parent;
        while (parentTransform != null)
        {
            Adventurer potentialParent = parentTransform.GetComponent<Adventurer>();
            if (potentialParent != null && parent == null)
            {
                parent = potentialParent;
                parent.children.Add(this);
                break;
            }
            parentTransform = parentTransform.parent;
        }

        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    protected virtual void OnButtonClick()
    {
        // Assigning color of selected button; saving original color for when it's deselected
        if (selectedButton != null)
        {
            selectedButton.button.image.color = selectedButton.originalColor;
        }
        button.image.color = GetClassColor();
        selectedButton = this;

        EnableAllowedButtons();
        UpdateAbilityDisplay();
    }

    private Color GetClassColor()
    {
        if (this is Sorceress) return Color.blue;
        if (this is Cleric) return Color.yellow;
        return Color.red;
    }

    private void EnableAllowedButtons()
    {
        foreach (var btn in allButtons) // Disable all buttons and change all line colors to black
        {
            btn.button.interactable = false;

            LineConnector connector = btn.GetComponent<LineConnector>();
            if (connector != null)
            {
                connector.SetLineColor(Color.black);
            }
        }

        // Enable self
        button.interactable = true;
        // Enable children
        foreach (var child in children)
        {
            child.button.interactable = true;

            LineConnector connector = child.GetComponent<LineConnector>();
            if (connector != null)
            {
                connector.SetLineColor(Color.white);
            }
        }
        // Enable ancestors
        Adventurer current = this;
        while (current.parent != null)
        {
            current.parent.button.interactable = true;
            LineConnector connector = current.GetComponent<LineConnector>();
            if (connector != null)
            {
                connector.SetLineColor(Color.white);
            }
            current = current.parent;
        }
        // Make sure line colors are updated
        foreach (var btn in allButtons)
        {
            LineConnector connector = btn.GetComponent<LineConnector>();
            if (connector != null)
            {
                connector.UpdateLine();
            }
        }
    }

    private void UpdateAbilityDisplay()
    {
        if (abilityDisplay != null)
        {
            List<string> allAbilities = GetAllAbilities();
            abilityDisplay.text = string.Join(", ", allAbilities);
        }
    }

    private List<string> GetAllAbilities()
    {
        List<string> collected = new List<string>();
        HashSet<string> uniqueAbilities = new HashSet<string>(); // Avoid duplicate abilities
        Adventurer current = this;

        while (current != null)
        {
            foreach (string ability in current.abilities)
            {
                if (uniqueAbilities.Add(ability))
                {
                    collected.Insert(0, ability);
                }
            }
            current = current.parent;
        }
        collected.Reverse(); // Abilities placed bottom-up (i.e. ability of clicked to Strike); reversing order for neatness
        return collected;
    }
}
