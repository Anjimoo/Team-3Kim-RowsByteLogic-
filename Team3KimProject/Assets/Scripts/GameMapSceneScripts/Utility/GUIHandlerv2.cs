using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GUIHandlerv2 : MonoBehaviour
{
    [SerializeField]
    private GameObject UImenu; //CANVAS for UI
    [SerializeField]
    private GameObject UnitStats; //UNITSTATS UI
    [SerializeField]
    private GameObject UnitAttacks; //new UNITATTACK UI see in Unity
    [SerializeField]
    private GameObject UnitAbility; //UNITABILITY UI
    [SerializeField]
    private GameObject UnitOptions; //UNITOPTIONS UI MOVE/ATTACK/STATS...
    [SerializeField]
    public GameObject UnitCounterActions;
    [SerializeField]
    private Image[] HealthBar;
    [SerializeField]
    private Image[] MPBar;
    private GameObject peekObject;
    private int layer;
    public bool moved = false;
    [SerializeField]
    public bool busy = false;
    public bool attacked=false;
    bool iClicked=false; //Check where Exit Button is accessed via menu or enemy stat click 
    bool toggleChoise=false;
    int chosenIndex = 0;
    public float mcFoVtemp;




    // Start is called before the first frame update
    void Start()
    {
        layer = 1 << 9;
        
    }
    
    // Update is called once per frame
    private void Update()
    {

        DisableButtons();
        CancelWithRightMouse();



    }


    public void OnMoveClick()
    {
        SelectedTrueChange();       
        UImenu.SetActive(false);        
        peekObject.GetComponent<UnitMove>().clickAble = true;
        busy = true;
     
    }
    public void OnWaitClick()
    {
        peekObject.GetComponent<Unit>().circulateCooldowns();
        GameObject.Find("TurnsHandler").GetComponent<TurnsHandler>().Enquing(peekObject);
            if (UImenu.activeSelf==true)
            { GameObject.Find("MoveButton").GetComponent<Button>().interactable = true; 
 
             GameObject.Find("AttacksButton").GetComponent<Button>().interactable = true; }
        

        SelectedFalseChange();
        UImenu.SetActive(false);
        peekObject.GetComponent<UnitMove>().clickAble = true;
        //peekObject.GetComponent<Unit>().circulateCooldowns();
        moved = false;
        busy = false;
        attacked = false;
        //selected = false;

    }
    public void OnStatsClick()
    {
        SelectedFalseChange();
        UnitOptions.SetActive(false);
        UnitStats.SetActive(true);
        GetStats(peekObject);
        busy = true;
        iClicked = true;
    }
    public void OnAttacksClick()
    {
        
        //SelectedTrueChange();    //disabled c'z of new Attack UI  
        //UImenu.SetActive(false); 
        UnitOptions.SetActive(false);
        UnitAttacks.SetActive(true); //enable new ATTACK UI
        SetAttackButtonName(peekObject);
        DisableAttack();
        busy = true;
        iClicked = true;
        //attacked = true;

        

    }
    public void OnFirstAttackClick(int index) //when choosing first attack
    {
        OnAttackUIStatesChange();
        peekObject.GetComponent<AttackAHandler>().selectedAttack = index;
    }

    public void OnFirstAbilityClick(int index) //when choosing first ability//
    {
        OnAbilityUIStatesChange();
        peekObject.GetComponent<AbilityAHandler>().selectedAbility = index;
    }

    public void OnFirstCounterActionClick(int index) //when choosing first action, assigned from ui//
    {

        mcFoVtemp =GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().fieldOfView;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().fieldOfView=100; //this and the next 2 lines take the camera for a full field view.

        Vector3 caPos = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().transform.position; //moving camera away back
        caPos.x = (float)8.6666;
        caPos.z = -2;
        GameObject.FindGameObjectWithTag("MainCamera").transform.position = caPos;
        //if (Math.Abs(unPos.x-3)<Math.Abs(unPos.x-16)) maybe will do it later to center the camera acoording to map size

        GameObject.FindGameObjectWithTag("TurnText").GetComponent<ChangeTurnText>().OnPlayersToggle();
        GameObject.FindGameObjectWithTag("TurnText").GetComponent<ChangeTurnText>().dontFocus = false; //moved from first line.
        peekObject.GetComponent<AttackAHandler>().UseSelectedAttack(index);
    }


    //public void OnSecondAttackClick() //when choosing second attack
    //{
    //    OnAttackUIStatesChange();
    //    peekObject.GetComponent<AttackAHandler>().selectedAttack = 1;  
    //}
    //public void OnThirdAttackClick() //when choosing second attack
    //{
    //    OnAttackUIStatesChange();
    //    peekObject.GetComponent<AttackAHandler>().selectedAttack = 2;
    //}
    //public void OnFourthAttackClick() //when choosing second attack
    //{
    //    OnAttackUIStatesChange();
    //    peekObject.GetComponent<AttackAHandler>().selectedAttack = 3;
    //}
    public void OnAbilitiesClick()
    {
        UnitOptions.SetActive(false);
        UnitAbility.SetActive(true); //enable new ABILITY UI
        SetAbilitiesButtonName(peekObject);
        DisableAbility();
        busy = true;
        iClicked = true;
    }
    public void OnExitGUIClick()
    {
        SelectedFalseChange();
        UnitStats.SetActive(false);
        UnitAttacks.SetActive(false);
        UnitAbility.SetActive(false);
        if (UnitOptions.activeSelf == false && iClicked == true)
        { UImenu.SetActive(true); }
        else
        { busy = false; }
        UnitOptions.SetActive(true);

    }

    public void OnCounterActions(GameObject target) //The function is called by AttackHandler/UseAttack
    {
        UImenu.SetActive(true);
        UnitOptions.SetActive(false);
        GameObject.FindGameObjectWithTag("TurnText").GetComponent<ChangeTurnText>().dontFocus = true;
        GameObject.FindGameObjectWithTag("TurnText").GetComponent<ChangeTurnText>().OnPlayersToggle();
        UnitCounterActions.SetActive(true);
        DisableCounterAction(target);
        busy = true;
    }
    
    private void SelectedFalseChange()
    {
        peekObject.GetComponent<UnitMove>().selected = false;       
    }
    private void SelectedTrueChange()
    {
        peekObject.GetComponent<UnitMove>().selected = true;
    }

    public void GetStats(GameObject gameObject)
    {
        GameObject.Find("UnitNameInStats").GetComponent<Text>().text = gameObject.GetComponent<Unit>().getUnitName();
        GameObject.Find("Health").GetComponent<Text>().text = "Health : " + gameObject.GetComponent<Unit>().getCurrentHP().ToString()+"/"+gameObject.GetComponent<Unit>().getMaxHP().ToString();
        GameObject.Find("ManaPool").GetComponent<Text>().text = "Mana : " + gameObject.GetComponent<Unit>().getCurrentMP().ToString()+"/" + gameObject.GetComponent<Unit>().getMaxMP().ToString();
        GameObject.Find("Attack").GetComponent<Text>().text = "Attack : " + gameObject.GetComponent<Unit>().getAttackPower().ToString();
        GameObject.Find("Focus").GetComponent<Text>().text = "Focus : " + gameObject.GetComponent<Unit>().getFocus().ToString();
        GameObject.Find("Mobility").GetComponent<Text>().text = "Mobility : " + gameObject.GetComponent<Unit>().getMobility().ToString();
        GameObject.Find("KillStreak").GetComponent<Text>().text = "Killstreak : " + gameObject.GetComponent<Unit>().getKillStreak().ToString();
        GameObject.Find("StatusCondition").GetComponent<Text>().text = "Status : " + gameObject.GetComponent<Unit>().getStatusCondition();
        GameObject.Find("Defense").GetComponent<Text>().text = "Defense : " + gameObject.GetComponent<Unit>().getDefense().ToString();
        busy = true;
    }
    
    private void HealthBarChange(GameObject gameObject)
    {
        for (int i = 0; i < HealthBar.Length; i++)
        {
            HealthBar[i].fillAmount = (float)(gameObject.GetComponent<Unit>().getCurrentHP()) / (float)(gameObject.GetComponent<Unit>().getMaxHP());
        }
    }
    private void MPBarChange(GameObject gameObject)
    {
        for (int i = 0; i < HealthBar.Length; i++)
        {
            MPBar[i].fillAmount = (float)(gameObject.GetComponent<Unit>().getCurrentMP()) / (float)(gameObject.GetComponent<Unit>().getMaxMP());
        }
    }
    public void SetAttackButtonName(GameObject gameObject)
    {
        GameObject.Find("Attack1").GetComponentInChildren<Text>().text = gameObject.GetComponent<Unit>().getAttackList()[0].getName();
        GameObject.Find("Attack2").GetComponentInChildren<Text>().text = gameObject.GetComponent<Unit>().getAttackList()[1].getName();
        GameObject.Find("Attack3").GetComponentInChildren<Text>().text = gameObject.GetComponent<Unit>().getAttackList()[2].getName();
        GameObject.Find("Attack4").GetComponentInChildren<Text>().text = gameObject.GetComponent<Unit>().getAttackList()[3].getName();
        //Also text change
        GameObject.Find("UnitAttacks/Health").GetComponentInChildren<Text>().text = "Health : " + gameObject.GetComponent<Unit>().getCurrentHP().ToString() + "/" + gameObject.GetComponent<Unit>().getMaxHP().ToString();
        GameObject.Find("UnitAttacks/ManaPool").GetComponentInChildren<Text>().text = "Mana : " + gameObject.GetComponent<Unit>().getCurrentMP().ToString() + "/" + gameObject.GetComponent<Unit>().getMaxMP().ToString();

    }
    public void SetAbilitiesButtonName(GameObject gameObject)
    {
        GameObject.Find("Ability1").GetComponentInChildren<Text>().text = gameObject.GetComponent<Unit>().getAbilityList()[0].getName();
        GameObject.Find("Ability2").GetComponentInChildren<Text>().text = gameObject.GetComponent<Unit>().getAbilityList()[1].getName();
        GameObject.Find("Ability3").GetComponentInChildren<Text>().text = gameObject.GetComponent<Unit>().getAbilityList()[2].getName();
        GameObject.Find("Ability4").GetComponentInChildren<Text>().text = gameObject.GetComponent<Unit>().getAbilityList()[3].getName();
        //Also text change
        GameObject.Find("UnitAbilities/Health").GetComponentInChildren<Text>().text = "Health : " + gameObject.GetComponent<Unit>().getCurrentHP().ToString() + "/" + gameObject.GetComponent<Unit>().getMaxHP().ToString();
        GameObject.Find("UnitAbilities/ManaPool").GetComponentInChildren<Text>().text = "Mana : " + gameObject.GetComponent<Unit>().getCurrentMP().ToString() + "/" + gameObject.GetComponent<Unit>().getMaxMP().ToString();

    }
    private void OnAttackUIStatesChange()
    {
        peekObject.GetComponent<AttackAHandler>().selectedToAttack = true;
        UnitOptions.SetActive(true);
        UnitAttacks.SetActive(false);
        UImenu.SetActive(false);
        busy = true;
    }
    private void OnAbilityUIStatesChange()
    {
        peekObject.GetComponent<AbilityAHandler>().selectedToUseAbility = true;
        UnitOptions.SetActive(true);
        UnitAbility.SetActive(false);
        UImenu.SetActive(false);
        busy = true;
    }

    public void OnCounterActionUIStatesChange()
    {
        this.chosenIndex = 0;
        UImenu.SetActive(false);
        UnitOptions.SetActive(true);
        UnitCounterActions.SetActive(false);

    }

    public void CancelWithRightMouse()
    {
        if (Input.GetMouseButton(1))//Reset selected tiles by right clicking
        {
            if (peekObject.GetComponent<UnitMove>().selected || peekObject.GetComponent<AttackAHandler>().selectedToAttack || peekObject.GetComponent<AbilityAHandler>().selectedToUseAbility)
            {
                busy = false;
                peekObject.GetComponent<UnitMove>().selected = false;
                peekObject.GetComponent<AttackAHandler>().selectedToAttack = false;
                peekObject.GetComponent<AbilityAHandler>().selectedToUseAbility = false;
            }
        }
    }

    public void DisableButtons() //Also HP bar I guess?
    {
        peekObject = GameObject.Find("TurnsHandler").GetComponent<TurnsHandler>().unitsInQueue.Peek();
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 30f, layer) && !busy)
        {
            HealthBarChange(hit.collider.gameObject);
            MPBarChange(hit.collider.gameObject);
            if (hit.collider.gameObject == peekObject && Input.GetMouseButtonDown(0) && !busy)
            {
                UImenu.SetActive(true);
                busy = true;
                SelectedFalseChange();
                peekObject.GetComponent<UnitMove>().clickAble = false;
                if (moved)
                {
                    GameObject.Find("MoveButton").GetComponent<Button>().interactable = false;
                }
                if (attacked)
                {
                    GameObject.Find("AttacksButton").GetComponent<Button>().interactable = false;
                }

            }
            else if (!busy && (hit.collider.gameObject.tag == "Player2" || hit.collider.gameObject.tag == "Player1") && hit.collider.gameObject != peekObject && Input.GetMouseButtonDown(0))
            {
                UImenu.SetActive(true);
                UnitOptions.SetActive(false);
                UnitStats.SetActive(true);
                SelectedFalseChange();
                this.iClicked = false;
                GetStats(hit.collider.gameObject);

            }

        }
    }

    public void DisableAbility()
    {
       
        Unit unit1 = peekObject.GetComponent<Unit>();
        for (int unit1AbilityNum = 0; unit1AbilityNum < 4; unit1AbilityNum++)
        {
            Skill unit1Ability = unit1.getAbilityList()[unit1AbilityNum];
            if (!(unit1.getAbilitiesCurrentCooldowns(unit1AbilityNum + 1) == 0 && unit1.getCurrentMP() >= unit1Ability.getCost()))
            {
                GameObject.Find("Ability" + (unit1AbilityNum + 1).ToString()).GetComponent<Button>().interactable = false;

                //Color change cd priority>mp priority

                if (!(unit1.getCurrentMP() >= unit1Ability.getCost()))
                {
                    GameObject.Find("Ability" + (unit1AbilityNum + 1).ToString()).GetComponentInChildren<Text>().color = new Color32(255, 0, 0, 255);
                }

                if (!(unit1.getAbilitiesCurrentCooldowns(unit1AbilityNum + 1) == 0))
                {
                    GameObject.Find("Ability" + (unit1AbilityNum + 1).ToString()).GetComponentInChildren<Text>().color = new Color32(190, 0, 255, 255);
                }


            }
            else
            {
                GameObject.Find("Ability" + (unit1AbilityNum + 1).ToString()).GetComponent<Button>().interactable = true;
                GameObject.Find("Ability" + (unit1AbilityNum + 1).ToString()).GetComponentInChildren<Text>().color = new Color32(0, 0, 0, 255);

            }
        }

    }
    public void DisableAttack()
    {

        Unit unit1 = peekObject.GetComponent<Unit>();
        for (int unit1AttackNum = 0; unit1AttackNum < 4; unit1AttackNum++)
        {
            Skill unit1Attack = unit1.getAttackList()[unit1AttackNum];
            if (!(unit1.getAttacksCurrentCooldowns(unit1AttackNum + 1) == 0 && unit1.getCurrentMP() >= unit1Attack.getCost()))
            {
                GameObject.Find("Attack" + (unit1AttackNum + 1).ToString()).GetComponent<Button>().interactable = false;

                //Color change cd priority>mp priority
                if (!(unit1.getCurrentMP() >= unit1Attack.getCost()))
                {
                    GameObject.Find("Attack" + (unit1AttackNum + 1).ToString()).GetComponentInChildren<Text>().color = new Color32(255, 0, 0, 255);
                }

                if (!(unit1.getAttacksCurrentCooldowns(unit1AttackNum + 1) == 0))
                {
                    GameObject.Find("Attack" + (unit1AttackNum + 1).ToString()).GetComponentInChildren<Text>().color = new Color32(190, 0, 255, 255);
                }


            }
            else
            {
                GameObject.Find("Attack" + (unit1AttackNum + 1).ToString()).GetComponent<Button>().interactable = true;
                GameObject.Find("Attack" + (unit1AttackNum + 1).ToString()).GetComponentInChildren<Text>().color = new Color32(0, 0, 0, 255);


            }
        }
    }

    public void DisableCounterAction(GameObject target)
    {
       
        Unit unit1 = target.GetComponent<Unit>();
        int[] actionCost = { unit1.getMaxMP() / 3, 0,0};
        for (int chosenAction = 0; chosenAction < 3; chosenAction++)
        {
            if (!(unit1.getCurrentMP() >= actionCost[chosenAction]))
            {

                GameObject.Find("Action" + (chosenAction + 1).ToString()).GetComponent<Button>().interactable = false;
                GameObject.Find("Action" + (chosenAction + 1).ToString()).GetComponentInChildren<Text>().color = new Color32(255, 0, 0, 255);



            }
            else
            {
                GameObject.Find("Action" + (chosenAction + 1).ToString()).GetComponent<Button>().interactable = true;
                GameObject.Find("Action" + (chosenAction + 1).ToString()).GetComponentInChildren<Text>().color = new Color32(0, 0, 0, 255);


            }
        }
    }

}
