using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OOPSample : MonoBehaviour
{
    public List<Character> characterList = new List<Character>();
    public GameObject buttonPrefab;
    public Transform buttonContainer;

    public Sprite actionBr; // Assign in the Inspector
    public Sprite flagBr;   // Assign in the Inspector
    public Sprite actionCn; // Assign in the Inspector
    public Sprite flagCn;   // Assign in the Inspector
    public Sprite actionFr; // Assign in the Inspector
    public Sprite flagFr;   // Assign in the Inspector
    public Sprite actionJp; // Assign in the Inspector
    public Sprite flagJp;   // Assign in the Inspector
    public Sprite actionKr; // Assign in the Inspector
    public Sprite flagKr;   // Assign in the Inspector
    public Sprite actionPh; // Assign in the Inspector
    public Sprite flagPh;   // Assign in the Inspector
    public Sprite actionRs; // Assign in the Inspector
    public Sprite flagRs;   // Assign in the Inspector
    public Sprite actionTh; // Assign in the Inspector
    public Sprite flagTh;   // Assign in the Inspector
    public Sprite actionUk; // Assign in the Inspector
    public Sprite flagUk;   // Assign in the Inspector
    public Sprite actionUs; // Assign in the Inspector
    public Sprite flagUs;   // Assign in the Inspector

    public GameObject characterDetailsPanel;
    public TextMeshProUGUI selectedCharacterName;
    public TextMeshProUGUI selectedOrigin;
    public TextMeshProUGUI selectedDescription;
    public TextMeshProUGUI selectedFocus;
    public Image selectedActionImage;
    public Image selectedFlagImage;
    private Character currentSelectedCharacter = null;

    public void Start()
    {
        characterDetailsPanel.SetActive(false);
        Character br = new Character("Capoeira", "Brazil",
            "    Capoeira is a martial art that focuses on flowing movements through kicking, evasions, acrobatics and dance. It was created by Afro-Brazilians to defend from Portuguese slavers before it was outlawed in 1890 and reformed in the 20th century to bypass the law. The dance aspect is said to be a disguise, but also maintains spirituality and culture.",
            Focus.Striking, actionBr, flagBr);
        characterList.Add(br);
        Character cn = new Character("Shaolinquan", "China",
            "    Shaolinquan, or Shaolin Kung Fu, is the largest style of kung fu, developed in the Shaolin Temple within its 1500-year history. It features not only combat training but also intense body conditioning and mental and spiritual practices. It has multiple styles of kung fu under it such as Changquan (Long Fist), Baihequan (Crane Fist), and Zuiquan (Drunken Fist).",
            Focus.Hybrid, actionCn, flagCn);
        characterList.Add(cn);
        Character fr = new Character("Savate", "France",
            "    Savate, also known as French Boxing or Foot Fighting, is a full-contact combat sport focused on kicking. The martial art combines the principles of Boxing with kicks that exclusively uses the feet and does not include the knees or shins. It originated from the 17th century practiced by sailors, and is reformed into sport during the 19th century.", 
           Focus.Striking, actionFr, flagFr);
        characterList.Add(fr);
        Character jp = new Character("Karate", "Japan",
            "    Karate is a martial art developed starting from the 1300s in the Ryukyu Kingdom under the influence of Chinese martial arts. The modern sport of karate is striking-focused, but traditional karate also involves throws and joint locks. There are four main styles of karate developed in Japan, especially in Okinawa: Shotokan, Shito-ryu, Wado-ryu, and Goju-ryu.",
            Focus.Hybrid, actionJp, flagJp);
        characterList.Add(jp);
        Character kr = new Character("Taekwondo", "Korea",
            "    Taekwondo is a Korean martial art and combat sport that is best known for its flashy kicking techniques. The name's meaning is divided into its syllables: tae (kick), kwon (punch), and do (way). Its three main components are poomsae (kicking, punching and blocking), kyorugi (Olympic-like sparring), and gyeopka (breaking boards).",
            Focus.Striking, actionKr, flagKr);
        characterList.Add(kr);
        Character ph = new Character("Eskrima", "Philippines",
            "    Eskrima is the national martial art of the Philippines, mainly focusing on weaponry like sticks and blades, but also features unarmed techniques. It is also known as Arnis or Kali, with the names being interchanged with general Filipino martial arts. A main focus that most systems teach is learning the angles of strikes than the strikes themselves.",
            Focus.Hybrid, actionPh, flagPh);
        characterList.Add(ph);
        Character rs = new Character("Sambo", "Russia",
            "    Sambo is a combat sport and a recognized style of amateur wrestling of Soviet origins. Its name is an acronym of \"samozashchita bez oruzhiya (self-defense without weapons)\", with the intent to merge techniques of other martial arts like Judo, Wrestling and Boxing. There are different styles of sambo for different purposes, like Sport and Combat.",
            Focus.Grappling, actionRs, flagRs);
        characterList.Add(rs);
        Character th = new Character("Muay Thai", "Thailand",
            "    Muay Thai is a martial art and full-contact combat sport from Thailand. It is also known as the Art of Eight Limbs, with the limbs referring to the fighter's fists, elbows, knees and shins as the main techniques in striking. Despite its extreme body conditioning and risk of injury, kids from Thailand that are under 15 would also train and compete professionally.",
            Focus.Striking, actionTh, flagTh);
        characterList.Add(th);
        Character uk = new Character("Boxing", "United Kingdom",
            "    Boxing is a combat sport that was invented probably since the prehistoric times, then modernized in UK in the mid-1860s. Globally, it refers to striking-focused martial arts but is commonly attributed to fists-only boxing. There are categories of boxers like in-fighter (close-range aggressor), out-boxer (long-range fighter), and slugger (brutal power-puncher).",
            Focus.Striking, actionUk, flagUk);
        characterList.Add(uk);
        Character us = new Character("Jeet Kune Do", "United States",
            "    Jeet Kune Do (Way of the Intercepting Fist) is the martial art created by martial artist, filmmaker and philosopher Lee Jun-fan, better known as Bruce Lee. It is built upon the belief that martial systems should be flexible \"like water\". Some martial arts that influence this art include Wing Chun, Boxing, Savate, Taekwondo, Eskrima, Judo, Jujutsu and Wrestling.",
            Focus.Hybrid, actionUs, flagUs);
        characterList.Add(us);

        foreach (Character character in characterList)
        {
            CreateCharacterButton(character);
        }
    }

    public void CreateCharacterButton(Character character)
    {
        GameObject characterButton = Instantiate(buttonPrefab, buttonContainer);
        ButtonCharacter buttonCharacter = characterButton.GetComponent<ButtonCharacter>();
        Button button = characterButton.GetComponent<Button>();
        Image buttonImage = characterButton.GetComponent<Image>();
        if (buttonImage != null && character.flag != null)
        {
            buttonImage.sprite = character.flag; // Set button's background to flag image
        }
        button.onClick.AddListener(() => OnClickCharacterButton(character));
        buttonCharacter.SetData(character);
    }

    public void OnClickCharacterButton(Character character)
    {
        if (currentSelectedCharacter == character)
            return; // Prevent redundant clicks

        // Hide previous information
        characterDetailsPanel.SetActive(false);

        // Update new selection
        currentSelectedCharacter = character;

        // Show updated info
        characterDetailsPanel.SetActive(true);
        selectedCharacterName.text = character.name;
        selectedOrigin.text = character.origin;
        selectedDescription.text = character.description;
        selectedFocus.text = $"{character.focus}";

        selectedActionImage.sprite = character.action ?? null;
        selectedFlagImage.sprite = character.flag ?? null;
    }
}