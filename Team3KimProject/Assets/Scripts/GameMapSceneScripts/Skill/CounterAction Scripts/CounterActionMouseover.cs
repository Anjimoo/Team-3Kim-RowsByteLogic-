using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class CounterActionMouseover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    Text techInfo;
    [SerializeField]
    Text flavorText;
    private bool isOver = false;
    float dodge, dodgeNormal;


    void Start()
    {
        techInfo.enabled = false;

    }

    public void OnPointerEnter(PointerEventData eventData)

    {
        Unit currentUnit = GameObject.Find("TurnsHandler").GetComponent<TurnsHandler>().unitsInQueue.Peek().GetComponent<Unit>();
        Unit attackedUnit = GameObject.Find("InvokeAttack").GetComponent<AttackInvoker>().unit2;

        //Debug.Log(currentUnit.gameObject.name);
        //Debug.Log(attackedUnit.gameObject.name);

        dodge= GameObject.Find("InvokeAttack").GetComponent<AttackInvoker>().getDodge(currentUnit, attackedUnit, currentUnit.gameObject.GetComponent<AttackAHandler>().getCurrentAttack());
        dodgeNormal= GameObject.Find("InvokeAttack").GetComponent<AttackInvoker>().getDodgeNormal(currentUnit, attackedUnit, currentUnit.gameObject.GetComponent<AttackAHandler>().getCurrentAttack());
        techInfo.enabled = true;
        flavorText.enabled = true;



        switch (gameObject.name)
        {
            case "Action1": //Dodge
                {
                    techInfo.text = "Cost: 25% of your maximum MP.";
                    flavorText.text = "Attempt to dodge an upcoming attack. Will take you 25% of maximum MP.";
                    flavorText.text += "\nChance to dodge: "+dodge.ToString()+"%";
                    break;
                }
            case "Action2": //Guard
                {
                    techInfo.text = "Cost: 50% of incoming damage as MP.";
                    flavorText.text = "Cuts incoming damage by 50%, remaining 50% will be reduced from MP.\nCannot dodge.";
                        break;
                }
            case "Action3": //Do Nothing
                {
                    techInfo.text = "No reaction by your unit.";
                    flavorText.text = "Go on with the encounter.";
                    flavorText.text += "\nChance to dodge: " + dodgeNormal.ToString() + "%";
                    break;
                }

        }
                       
        

        this.isOver = true;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        techInfo.enabled = false;
        flavorText.enabled = false;
        this.isOver = false;
    }
}
