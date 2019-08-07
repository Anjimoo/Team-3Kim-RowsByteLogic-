using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class AbilityMouseoverInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    Text techInfo;
    [SerializeField]
    Text flavorText;
    private bool isOver = false;


    void Start()
    {
        techInfo.enabled = false;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        techInfo.enabled = true;
        flavorText.enabled = true;


        Unit currentUnit = GameObject.Find("TurnsHandler").GetComponent<TurnsHandler>().unitsInQueue.Peek().GetComponent<Unit>();
        int i = 0;
        switch (gameObject.name)
        {
            case "Ability1":
                i = 0;
                break;
            case "Ability2":
                i = 1;
                break;
            case "Ability3":
                i = 2;
                break;
            case "Ability4":
                i = 3;
                break;
        }

        //Handling the Flavor text mouseover.
        flavorText.text = currentUnit.getAbilityList()[i].getFlavorText();

        //Handling the Ability Information mouseover
        techInfo.text = "Cost: " + currentUnit.getAbilityList()[i].getCost().ToString();
        techInfo.text += "\n";
        techInfo.text += "\nRange:" + currentUnit.getAbilityList()[i].getRange();
        techInfo.text += "          Cooldown: " + currentUnit.getAbilitiesCurrentCooldowns(i + 1) + "/" + currentUnit.getAbilityList()[i].getSkillCD().ToString();

        this.isOver = true;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        techInfo.enabled = false;
        flavorText.enabled = false;
        this.isOver = false;
    }
}
