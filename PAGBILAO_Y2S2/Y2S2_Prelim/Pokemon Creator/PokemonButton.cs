using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PokemonButton : MonoBehaviour
{
    public TextMeshProUGUI buttonText;

    private GameObject listScrollerPanel;
    private GameObject listInfoPanel;

    private TMP_Text infoName;
    private TMP_Text infoLevel;
    private TMP_Text infoElement;
    private TMP_Text infoGender;

    private Pokemon pokemonData;

    public void Initialize(Pokemon pokemon, GameObject listScroller, GameObject listInfo, TMP_Text nameTMP, TMP_Text levelTMP, TMP_Text elementTMP, TMP_Text genderTMP)
    {
        listScrollerPanel = listScroller;
        listInfoPanel = listInfo;

        infoName = nameTMP;
        infoLevel = levelTMP;
        infoElement = elementTMP;
        infoGender = genderTMP;

        pokemonData = pokemon;
        buttonText.text = pokemon.name;
    }

    public void OnClick()
    {
        // Updating text components
        infoName.text = pokemonData.name;
        infoLevel.text = $"Level {pokemonData.level}";
        infoElement.text = $"{pokemonData.element} Type Pokemon";
        infoGender.text = $"{pokemonData.gender}";

        listScrollerPanel.SetActive(false);
        listInfoPanel.SetActive(true);
    }
}
