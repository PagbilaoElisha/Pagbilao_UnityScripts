using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PokemonCreator : MonoBehaviour
{
    public List<Pokemon> pokemonList = new List<Pokemon>();

    [Header("List Creator UI")]
    public TMP_InputField nameInput;
    public TMP_InputField levelInput;
    public TMP_Dropdown elementDropdown;
    public TMP_Dropdown genderDropdown;
    public TMP_Text countText;
    public TMP_Text errorText;

    [Header("Scene Objects")]
    public GameObject listCreatorPanel;
    public GameObject listScrollerPanel;
    public GameObject listInfoPanel;
    public GameObject buttonPrefab;
    public Transform buttonContainer;

    [Header("List Info UI")]
    public TMP_Text infoName;
    public TMP_Text infoLevel;
    public TMP_Text infoElement;
    public TMP_Text infoGender;


    public void Start()
    {
        PopulateDropdown(elementDropdown, typeof(Element)); //Replace default options with Element enum
        PopulateDropdown(genderDropdown, typeof(Gender)); //Replace default options with Gender enum

        UpdateCount(); // Counting to ensure no blank names or levels are added
        errorText.gameObject.SetActive(false);
    }

    private void PopulateDropdown(TMP_Dropdown dropdown, System.Type enumType)
    {
        dropdown.ClearOptions();
        List<string> enumNames = new List<string>(System.Enum.GetNames(enumType));
        dropdown.AddOptions(enumNames);
    }

    public void AddToList()
    {
        string name = nameInput.text.Trim(); // .Trim to avoid white spaces
        if (!int.TryParse(levelInput.text, out int level) || string.IsNullOrEmpty(name))
        {
            errorText.gameObject.SetActive(true);
            Debug.Log($"Null name/level detected");
            return; // Avoids creation of button with null name or level by ending function when if condition is true
        }
        Element element = (Element)elementDropdown.value;
        Gender gender = (Gender)genderDropdown.value;

        Pokemon newPokemon = new Pokemon(name, level, element, gender);
        pokemonList.Add(newPokemon);

        UpdateCount();
        ClearInputs();
        errorText.gameObject.SetActive(false); // No null error, hence error is hidden
    }

    private void UpdateCount()
    {
        countText.text = $"{pokemonList.Count} Pokemon added"; // Making sure the number of buttons made is correct
    }

    private void ClearInputs() // Reset inputs for clearer indication of new creation
    {
        nameInput.text = "";
        levelInput.text = "";
        elementDropdown.value = 0;
        genderDropdown.value = 0;
    }

    public void GenerateButtons()
    {
        if (pokemonList.Count == 0)
        {
            Debug.Log($"Empty list generation attempted");
            return;
        }
        foreach (Pokemon p in pokemonList)
        {
            CreateButton(p);
        }

        listCreatorPanel.SetActive(false);
        listScrollerPanel.SetActive(true);
    }

    public void CreateButton(Pokemon pokemon)
    {
        GameObject buttonPokemon = Instantiate(buttonPrefab, buttonContainer);
        PokemonButton pokemonButton = buttonPokemon.GetComponent<PokemonButton>();
        Button button = pokemonButton.GetComponent<Button>();

        TextMeshProUGUI buttonText = buttonPokemon.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText != null )
        {
            pokemonButton.buttonText = buttonText;
        }

        pokemonButton.Initialize(pokemon, listScrollerPanel, listInfoPanel, infoName, infoLevel, infoElement, infoGender);
        button.onClick.AddListener(pokemonButton.OnClick);
        Debug.Log($"{pokemon.name} is created");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToList()
    {
        listInfoPanel.SetActive(false);
        listScrollerPanel.SetActive(true);
    }
}
