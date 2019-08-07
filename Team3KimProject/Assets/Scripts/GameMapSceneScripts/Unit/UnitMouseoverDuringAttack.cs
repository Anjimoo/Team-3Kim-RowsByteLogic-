using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class UnitMouseoverDuringAttack : MonoBehaviour 
{
    //[SerializeField]
    Text floatingCrit, floatingHit;
    float crit, hit, dodgeifdodging,dodge;
    Unit currentUnit;
    bool useOnce;

    private void Awake()
    {
        floatingCrit = gameObject.transform.Find("Canvas").transform.Find("CritText").GetComponent<Text>();
        floatingHit = gameObject.transform.Find("Canvas").transform.Find("HitText").GetComponent<Text>();
        

    }

    void Update()
    {
        if (GameObject.Find("TurnsHandler").GetComponent<TurnsHandler>().unitsInQueue.Peek().GetComponent<AttackAHandler>().selectedToAttack && !useOnce)
        { ShowText(); }

        if (!GameObject.Find("TurnsHandler").GetComponent<TurnsHandler>().unitsInQueue.Peek().GetComponent<AttackAHandler>().selectedToAttack)
        {
            useOnce = false;
            HideText();
        }
    }

    public void ShowText()
    {
        currentUnit = GameObject.Find("TurnsHandler").GetComponent<TurnsHandler>().unitsInQueue.Peek().GetComponent<Unit>();

        if (currentUnit.isThisPlayer1()!=this.gameObject.GetComponent<Unit>().isThisPlayer1())
         {
            crit = GameObject.Find("InvokeAttack").GetComponent<AttackInvoker>().getCrit(currentUnit, this.gameObject.GetComponent<Unit>(), currentUnit.gameObject.GetComponent<AttackAHandler>().getCurrentAttack());
            hit = GameObject.Find("InvokeAttack").GetComponent<AttackInvoker>().getHit(currentUnit, this.gameObject.GetComponent<Unit>(), currentUnit.gameObject.GetComponent<AttackAHandler>().getCurrentAttack());
            dodge = 100 - hit;
            floatingCrit.text =  "Crit rate: "+crit.ToString()+"%";
            floatingHit.text =  "Chance to hit: "+hit.ToString()+"%";    
            floatingCrit.enabled = true;
            floatingHit.enabled = true;
            useOnce = true;
        }



    }

    public void HideText()
    {
        floatingCrit.enabled = false;
        floatingHit.enabled = false;
    }
}
