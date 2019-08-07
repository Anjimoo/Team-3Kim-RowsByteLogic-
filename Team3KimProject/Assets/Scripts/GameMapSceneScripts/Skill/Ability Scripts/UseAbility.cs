using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UseAbility : MonoBehaviour
{
    GameObject peekObject;
    

    public void InvokeAbility(Unit unit1, Unit unit2, int unit1AbilityNum)
    {
        Skill unit1Ability = unit1.getAbilityList()[unit1AbilityNum - 1];

        if (unit1.getAbilitiesCurrentCooldowns(unit1AbilityNum) == 0 && unit1.getCurrentMP() >= unit1Ability.getCost()) //Needs to check range too
        {

            //Mana cost and Cooldown for unit1
            unit1.setCurrentMP(-unit1Ability.getCost());
            unit1.setAbilitiesCurrentCooldowns(unit1AbilityNum, unit1Ability.getSkillCD()); //Cooldowns in abilities won't add+1, because multiple abilities can be used per
                                                                                            // turn  and the current turn will count as turn that's passed, unlike attack.

            //Actual effects on unit2
            unit2.setCurrentHP(unit1Ability.getHPmod());
            unit2.setCurrentMP(unit1Ability.getMPmod());
            unit2.setAttackPower(unit1Ability.getATKmod());
            unit2.setDefense(unit1Ability.getDEFmod());
            unit2.setFocus(unit1Ability.getFCSmod());
            unit2.setMobility(unit1Ability.getMOBmod());
            unit2.setStatusCondition(unit1Ability.getStatusMod());
            unit2.setStunned(unit1Ability.getTurnsLeftMod());
                 
            //Ability ignores defense. "True Damage"


            peekObject = GameObject.Find("TurnsHandler").GetComponent<TurnsHandler>().unitsInQueue.Peek();
            GameObject.Find("GUIHandlerV2").GetComponent<GUIHandlerv2>().busy = false;
            peekObject.GetComponent<AbilityAHandler>().selectedToUseAbility = false;


        }
        else
        {
            Debug.Log("Ability usage failed! " + unit1.getUnitName() + " is either out of mana or has a cooldown!");
        }
    }

    public void selfUse(Unit unit1, int  unit1AbilityNum)
    {
        Skill unit1Ability = unit1.getAbilityList()[unit1AbilityNum - 1];

        if (unit1.getAbilitiesCurrentCooldowns(unit1AbilityNum) == 0 && unit1.getCurrentMP() >= unit1Ability.getCost()) //Needs to check range too
        {

            //Mana cost and Cooldown for unit1
            unit1.setCurrentMP(-unit1Ability.getCost());
            unit1.setAbilitiesCurrentCooldowns(unit1AbilityNum, unit1Ability.getSkillCD()); //Cooldowns in abilities won't add+1, because multiple abilities can be used per
                                                                                            // turn  and the current turn will count as turn that's passed, unlike attack.

            //Actual effects on unit2
            unit1.setCurrentHP(unit1Ability.getHPmod());
            unit1.setCurrentMP(unit1Ability.getMPmod());
            unit1.setAttackPower(unit1Ability.getATKmod());
            unit1.setDefense(unit1Ability.getDEFmod());
            unit1.setFocus(unit1Ability.getFCSmod());
            unit1.setMobility(unit1Ability.getMOBmod());
            unit1.setStatusCondition(unit1Ability.getStatusMod());
            unit1.setStunned(unit1Ability.getTurnsLeftMod());

            //Ability ignores defense. "True Damage"


            peekObject = GameObject.Find("TurnsHandler").GetComponent<TurnsHandler>().unitsInQueue.Peek();
            GameObject.Find("GUIHandlerV2").GetComponent<GUIHandlerv2>().busy = false;
            peekObject.GetComponent<AbilityAHandler>().selectedToUseAbility = false;


        }
        else
        {
            Debug.Log("Ability usage failed! " + unit1.getUnitName() + " is either out of mana or has a cooldown!");
        }
    }

}