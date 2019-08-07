using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UseAttack : MonoBehaviour
{
    GameObject peekObject;
    int[] action = { 0, 0, 0 };
    private static readonly System.Random randomForHitAndCrit = new System.Random();
    int incomingDamage;
    float ifDodging, ifNotDodging;
    Unit attackedUnit;


    //public void InvokeFullAttack(Unit unit1, Unit unit2, int unit1AttackNum, int unit2CounterAttackNum) //Skill unit1Attack, Skill unit2CounterAttack)
    //{
    //    Skill unit1Attack = unit1.getAttackList()[unit1AttackNum-1];
    //    Skill unit2CounterAttack = unit2.getAttackList()[unit2CounterAttackNum-1];



    //    //Unit1 attacks Unit2
    //    InvokeAttackOnce(unit1, unit2, unit1AttackNum);


    //    //Counter attack from Unit2 on Unit1
    //    InvokeAttackOnce(unit2, unit1, unit2CounterAttackNum);


    //}


    public void InvokeAttackOnce(Unit unit1, Unit unit2, int unit1AttackNum) 
    {
        Skill unit1Attack = unit1.getAttackList()[unit1AttackNum - 1];

        if (unit1.getAttacksCurrentCooldowns(unit1AttackNum) == 0 && unit1.getCurrentMP() >= unit1Attack.getCost()) //Needs to check range too
        {

            //Mana cost and Cooldown for unit1
            unit1.setCurrentMP(-unit1Attack.getCost());
            unit1.setAttacksCurrentCooldowns(unit1AttackNum, unit1Attack.getSkillCD()+1); //Cooldowns in abilities won't add+1, because multiple abilities can be used per
                                                                                          // turn  and the current turn will count as turn that's passed, unlike attack.

            //Actual effects on unit2
            incomingDamage = (int)finalDamageCalculator(unit1, unit2, unit1Attack, "calc");
            //here active the wait for seonds in text incoming damage
            unit2.setCurrentHP(incomingDamage);
            if (unit2.getCurrentHP()<=0) { unit1.setKillStreak(); }

            unit2.setCurrentMP(unit1Attack.getMPmod());
            unit2.setAttackPower(unit1Attack.getATKmod());
            unit2.setDefense(unit1Attack.getDEFmod());
            unit2.setFocus(unit1Attack.getFCSmod());
            unit2.setMobility(unit1Attack.getMOBmod());
            unit2.setStatusCondition(unit1Attack.getStatusMod());
            unit2.setStunned(unit1Attack.getTurnsLeftMod());
            



            peekObject = GameObject.Find("TurnsHandler").GetComponent<TurnsHandler>().unitsInQueue.Peek();
            //GameObject.Find("GUIHandlerV2").GetComponent<GUIHandlerv2>().attacked = true;
            //GameObject.Find("GUIHandlerV2").GetComponent<GUIHandlerv2>().busy = false;
            ////peekObject.GetComponent<AttackAHandler>().selectedToAttack = false;            
            //GameObject.Find("GUIHandlerV2").GetComponent<GUIHandlerv2>().OnCounterActionUIStatesChange();
            //Debug.Log("Dodge: "+this.action[0].ToString());
            //Debug.Log("Guard: "+this.action[1].ToString());
            //Debug.Log("Do Nothing: "+this.action[2].ToString());

            ResetAction();





        }
        else
        {
            Debug.Log("Attack failed! "+unit1.getUnitName()+" is out of mana!");
        }


    }


    private float finalDamageCalculator(Unit attacker, Unit defender, Skill attackUsed, string action)
    {
        this.attackedUnit = defender;
        int finalDamage=0;
        float hitRan = randomForHitAndCrit.Next(100);
        float critRan = randomForHitAndCrit.Next(99);
        float crit, hit=0;
        int attackerATK = attacker.getAttackPower(), attackerFCS = attacker.getFocus();
        float scaling = attackUsed.getDamageScaling();
        int attackBaseDamage = -attackUsed.getHPmod();

        int defenderDEF = defender.getDefense(), defenderMOB = defender.getMobility();

        float totalBaseDamage = attackBaseDamage + attackerATK * scaling;
        
        //dodge calculations base on attacker focus and defender mobility, similar to defense formula. Only change last numbers (2 or 15) 
        ifNotDodging = 100 - attackerFCS * attackerFCS / (attackerFCS + 2 * defenderMOB);                        //if you wanna mess around nothing else.
        ifDodging = 100 - (attackerFCS * attackerFCS / (attackerFCS + 15 * defenderMOB));
        hit =100- ifNotDodging;

        //damage reduction from DEF
        finalDamage = (int)(totalBaseDamage * totalBaseDamage / (defenderDEF + totalBaseDamage)) +1; //defense formula, saw online.

        //crit calculation
        crit = (attackerFCS / 9) * (1 - scaling);
        if (critRan < crit) { finalDamage = finalDamage * 15 / 10; defender.gameObject.GetComponent<Unit>().critIndicator = "Crit!\n"; } //crit formula, very simple.
        else { defender.gameObject.GetComponent<Unit>().critIndicator = ""; }
        //Debug.Log("Crit rate Randomizer: " + critRan.ToString());

        //ACTIONS

        if (this.action[0] == 1) //if dodging
        {
            Debug.Log("Dodging");
            finalDamage = finalDamage * 1; //Do not touch this, otherwise dodging won't work, no idea why.
            hit = 100 - ifDodging; //defender dodges
            defender.setCurrentMP(-defender.getMaxMP() / 4);
        }

        if (this.action[1] == 1) //if guarding
        {
            Debug.Log("Guarding");
            finalDamage = finalDamage / 2; //Should cut incoming damage by half and take half of it as mana loss.
            hit = 100; //cannot dodge if guarding
            int carry = 0;
            if (defender.getCurrentMP() < finalDamage) //Check if half the incoming damage can't be taken as mana instead.
            {
                carry = finalDamage - defender.getCurrentMP(); //Won't reduce not enough mana.
                finalDamage += carry;
            }
            defender.setCurrentMP(-finalDamage / 2); 
        }



        if (crit > 100) crit = 100;
        if (crit < 0) crit = 0;
        if (hit > 100) hit = 100;
        if (crit < 0) crit = 0;
        if (ifDodging > 100) ifDodging = 100;
        if (ifDodging < 0) ifDodging = 0;
        

        if (hitRan > hit) { finalDamage = 0; if (attacker.GetComponent<AttackAHandler>().selectedToAttack == false){ defender.gameObject.GetComponent<Unit>().iDodged(); }} //miss


        //Debug.Log("Hit rate Randomizer: " + hitRan.ToString());

        if (action == "dodge")
            return ifDodging;
            

            if (action == "crit")
            return crit;
        if (action == "hit")
        {
            Debug.Log("hit: " + hit.ToString());
            return hit;
        }
        if (action == "calc")
        {
            Debug.Log("Dodge: " + this.action[0].ToString());
            Debug.Log("Guard: " + this.action[1].ToString());
            Debug.Log("Do Nothing: " + this.action[2].ToString());
            Debug.Log("finalHit: " + hit.ToString());
            return -finalDamage;
        }
        else return 0;
    }

    public float getCrit(Unit attacker, Unit defender, Skill attackUsed) { return finalDamageCalculator(attacker, defender, attackUsed, "crit"); }
    public float getHit(Unit attacker, Unit defender, Skill attackUsed) { return finalDamageCalculator(attacker, defender, attackUsed, "hit"); }
    public float getDodge(Unit attacker, Unit defender, Skill attackUsed) { return finalDamageCalculator(attacker, defender, attackUsed, "dodge"); }
    public float getDodgeNormal(Unit attacker, Unit defender, Skill attackUsed) { return 100-getHit(attacker, defender, attackUsed); }





    public void ChooseCounterAction(int index)
    {
        this.action[index] = 1;
    }

    public void ResetAction()
    {
        this.action[0] = 0;
        this.action[1] = 0;
        this.action[2] = 0;
    }


}
