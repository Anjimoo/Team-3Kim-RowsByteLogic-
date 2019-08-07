using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class AttackMouseoverInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
        int i=0;
        switch (gameObject.name)
        {
            case "Attack1":
                i = 0;
                break;
            case "Attack2":
                i = 1;
                break;
            case "Attack3":
                i = 2;
                break;
            case "Attack4":
                i = 3;
                break;
        }

        //Handling the Flavor text mouseover.
        flavorText.text = currentUnit.getAttackList()[i].getFlavorText();

        //Handling the Attack Information mouseover
        techInfo.text = "Cost: " + currentUnit.getAttackList()[i].getCost().ToString();
        techInfo.text += "\nDamage:" + (-currentUnit.getAttackList()[i].getHPmod()).ToString() + "+(" + ((int)(currentUnit.getAttackPower() * currentUnit.getAttackList()[i].getDamageScaling()) + ")");
        techInfo.text += "\nRange:" + currentUnit.getAttackList()[i].getRange();
        techInfo.text += "          Cooldown: "+currentUnit.getAttacksCurrentCooldowns(i+1)+"/" + currentUnit.getAttackList()[i].getSkillCD().ToString();

        this.isOver = true;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        techInfo.enabled = false;
        flavorText.enabled = false;
        this.isOver = false;
    }
}
