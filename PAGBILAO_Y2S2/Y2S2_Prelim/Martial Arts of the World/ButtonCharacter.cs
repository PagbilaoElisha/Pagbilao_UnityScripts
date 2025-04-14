using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonCharacter : MonoBehaviour
{
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI origin;
    public TextMeshProUGUI description;
    public TextMeshProUGUI focus;
    public Image action;
    public Image flag;

    public void SetData(Character character)
    {
        characterName.text = character.name;
        origin.text = character.origin;
        description.text = character.description;
        focus.text = $"{character.focus}";

        if (character.action != null)
            action.sprite = character.action;

        if (character.flag != null)
            flag.sprite = character.flag;
    }
}