using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{

    [SerializeField]
    string unitName;
    [SerializeField]
    int maxHP = 100; //change these later on
    [SerializeField]
    int currentHP = 100;
    [SerializeField]
    int maxMP;
    [SerializeField]
    int currentMP;
    [SerializeField]
    int attack;
    [SerializeField]
    int defense;
    [SerializeField]
    int focus;
    [SerializeField]
    public int mobility;
    [SerializeField]
    int killstreak;
    [SerializeField]
    Skill[] attackList;
    [SerializeField]
    Skill[] abilityList;
    [SerializeField]
    string flavorText;
    bool attackable = false;
   // [SerializeField]
    int[] abilitiesCurrentCooldowns = { 0, 0, 0, 0 }; // Abilities cooldowns.
    //[SerializeField]
    int[] attacksCurrentCooldowns = { 0, 0, 0, 0 }; // Some attacks might be too powerful and will need CD                                                   
    [SerializeField]
    string statusCondition;
    string[] statConditions = { "Normal", "", "", "" };
    [SerializeField]
    bool isPlayer1; //we might need this.
    //[SerializeField]
    //int overtimeEffect;
    //[SerializeField]
    //int overtimeEffectDuration;
    [SerializeField]
    int stunned = 0; //how many rounds of turns will be stunned
    [SerializeField]
    Sprite icon;
    [SerializeField]
    public GameObject[] projectiles;
    [SerializeField]
    GameObject damageTextOBJ;
    Text damageText;
    int lastKnownHP;
    public string critIndicator;
    int damageGap;
    






public void setUnitName(string unitName) { this.unitName = unitName; }
public string getUnitName() { return this.unitName; }
public void setMaxHP(int maxHP) { this.maxHP = maxHP; }
public int getMaxHP() { return this.maxHP; }
public void setCurrentHP(int currentHP) { this.currentHP += currentHP; }
public int getCurrentHP() { return this.currentHP; }
public void setMaxMP(int maxMP) { this.maxMP = maxMP; }
public int getMaxMP() { return maxMP; }
public void setCurrentMP(int currentMP) { this.currentMP += currentMP; }
public int getCurrentMP() { return this.currentMP; }
public void setDefense(int defense) { this.defense += defense; }
public int getDefense() { return this.defense; }
public void setMobility(int mobility) { this.mobility += mobility; }
public int getMobility() { return this.mobility; }
public void setFocus(int focus) { this.focus += focus; }
public int getFocus() { return this.focus; }
public void setKillStreak() { this.killstreak++; OnKillStreak(); }
public int getKillStreak() { return this.killstreak; }
public void setStatusCondition(string statusCondition) { this.statusCondition = statusCondition; }
public string getStatusCondition() { return this.statusCondition; }
public bool isThisPlayer1() { return isPlayer1; }
public void setAbilityList(Skill[] abilityList) { this.abilityList = abilityList; }
public Skill[] getAbilityList() { return this.abilityList; } 
public void setAttackList(Skill[] attackList) { this.attackList = attackList; }
public Skill[] getAttackList() { return this.attackList; }
public void setAttackPower(int attack) { this.attack += attack; }
public int getAttackPower() { return this.attack; }
public void setStunned(int turnsLeft) { this.stunned += turnsLeft; }
public int getStunned() { return this.stunned; }
public void setFlavorText(string flavorText) { this.flavorText = flavorText; }
public string getFlavorText() { return this.flavorText; }
public void toggleAttackableOn() { this.attackable = true; }
public void toggleAttackableOff() { this.attackable = true; }
public bool getAttackable() { return this.attackable; }
public void setAbilitiesCurrentCooldowns(int i, int cooldown) { this.abilitiesCurrentCooldowns[i-1] = cooldown; }
public int getAbilitiesCurrentCooldowns(int i) { return this.abilitiesCurrentCooldowns[i-1]; } 
public void setAttacksCurrentCooldowns(int i, int cooldown) { this.attacksCurrentCooldowns[i-1] = cooldown; }
public int getAttacksCurrentCooldowns(int i) { return this.attacksCurrentCooldowns[i-1]; }
public Sprite GetIcon() { return this.icon; }
    
public void circulateCooldowns()
    {
        abilitiesCurrentCooldowns[0] -= 1;
        abilitiesCurrentCooldowns[1] -= 1;
        abilitiesCurrentCooldowns[2]-= 1;
        abilitiesCurrentCooldowns[3] -= 1;
        attacksCurrentCooldowns[0] -= 1;
        attacksCurrentCooldowns[1] -= 1;
        attacksCurrentCooldowns[2] -= 1;
        attacksCurrentCooldowns[3] -= 1;
        stunned -= 1;
        this.currentMP +=50+ this.currentMP / 15;
    }

    void Awake()
    {
        if (this.gameObject.transform.tag == "Player1") { this.isPlayer1 = true; }
        if (this.gameObject.transform.tag == "Player2") { this.isPlayer1 = false; }
        this.attacksCurrentCooldowns[3] = 4;
        lastKnownHP = this.currentHP;
        damageText = damageTextOBJ.GetComponent<Text>();
    }




    void Update()
    {


        if (this.currentHP <= 0) { Invoke(nameof(OnDeath), 1); }
        if (this.lastKnownHP!=currentHP) { damageGap = lastKnownHP - currentHP; lastKnownHP = currentHP; OnDamage(); }

        currentHP = (int)Mathf.Clamp(currentHP, 0, maxHP);
        currentMP = (int)Mathf.Clamp(currentMP, 0, maxMP);

        abilitiesCurrentCooldowns[0] = (int)Mathf.Clamp(abilitiesCurrentCooldowns[0], 0, 100f);
        abilitiesCurrentCooldowns[1] = (int)Mathf.Clamp(abilitiesCurrentCooldowns[1], 0, 100f);
        abilitiesCurrentCooldowns[2] = (int)Mathf.Clamp(abilitiesCurrentCooldowns[2], 0, 100f);
        abilitiesCurrentCooldowns[3] = (int)Mathf.Clamp(abilitiesCurrentCooldowns[3], 0, 100f);
        attacksCurrentCooldowns[0] = (int)Mathf.Clamp(attacksCurrentCooldowns[0], 0, 100f);
        attacksCurrentCooldowns[1] = (int)Mathf.Clamp(attacksCurrentCooldowns[1], 0, 100f);
        attacksCurrentCooldowns[2] = (int)Mathf.Clamp(attacksCurrentCooldowns[2], 0, 100f);
        attacksCurrentCooldowns[3] = (int)Mathf.Clamp(attacksCurrentCooldowns[3], 0, 100);
        stunned = (int)Mathf.Clamp(stunned, 0, 100f);

        if (stunned > 0)
        {
            statusCondition = "Stunned (" + stunned.ToString() + ")";
            if (GameObject.Find("TurnsHandler").GetComponent<TurnsHandler>().unitsInQueue.Peek().GetComponent<Unit>() == this)
            {
                GameObject.Find("GUIHandlerV2").GetComponent<GUIHandlerv2>().OnWaitClick();
            }
        }

        else
        {
            statusCondition = "Normal";


        }


    }

    public void OnKillStreak()
    {
        this.attack= this.attack*11 / 10;
        this.defense= this.defense * 11 / 10;
        this.mobility= this.mobility * 11 / 10;
        this.focus= this.focus * 11 / 10;
        this.maxHP = this.maxHP * 11 / 10;
        this.currentHP = this.currentHP * 11 / 10;
        this.maxMP = this.maxMP * 11 / 10;
        this.currentMP = this.currentMP * 11 / 10;        
    }

    void OnDeath()
    {
        Destroy(gameObject);
    }

    void OnDamage()
    {
        if (damageGap>=0)
        {
           //damageGap = -damageGap;
            damageText.color = new Color32(255, 0, 0, 255);
            damageTextOBJ.SetActive(true);
            damageText.text = critIndicator+ damageGap.ToString();
            Invoke(nameof(OnDamageTextChange), 3f);
        }

    }
    public void iDodged()
    {
        if (!GameObject.Find("GUIHandlerV2").GetComponent<GUIHandlerv2>().UnitCounterActions.activeSelf)
        {
            damageText.color = new Color32(0, 255, 255, 255);
            damageTextOBJ.SetActive(true);
            damageText.text = "Dodge";
            Invoke(nameof(OnDamageTextChange), 3f);
        }
        
    }

    void OnDamageTextChange()
    {
        damageTextOBJ.SetActive(false);
        critIndicator = "";
        damageText.text = "";
        damageGap = 0;
    }
}
