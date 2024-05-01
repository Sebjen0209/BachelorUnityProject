using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
    public Image characterDisplay;
    public Image characterIcon;
    public TextMeshProUGUI characterDescription;
    public TextMeshProUGUI characterRole;

    public Sprite warriorImage;
    public Sprite warriorIcon;
    private string warriorDescription; 
    
    public Sprite sorcererImage;
    public Sprite sorcererIcon;
    private string sorcererDescription; 

    public Sprite archerImage; 
    public Sprite archerIcon;
    private string archerDescription; 

    public Sprite assassinImage;
    public Sprite assassinIcon;
    private string assassinDescription; 

    public void SetCharacter(string characterType)
    {
        switch (characterType)
        {
            case "Warrior":
                characterDisplay.sprite = warriorImage;
                characterIcon.sprite = warriorIcon;
                characterDescription.text = "Warrior Description. Meele and Hard Cock";
                characterRole.text = "Warrior";
                break;
            case "Sorcerer":
                characterDisplay.sprite = sorcererImage;
                characterIcon.sprite = sorcererIcon;
                characterDescription.text = "Wizard Description. Meele and Hard Cock";
                characterRole.text = "Sorcerer";
                break;
            case "Archer":
                characterDisplay.sprite = archerImage;
                characterIcon.sprite = archerIcon;
                characterDescription.text = "Archer Description. Bow and faggot looking ass cloth";
                characterRole.text = "Archer";
                break;
            case "Assassin":
                characterDisplay.sprite = assassinImage;
                characterIcon.sprite = assassinIcon;
                characterDescription.text = "Assassin Description. Sneak Sneak";
                characterRole.text = "Assassin";
                break;
        }
    }
}
