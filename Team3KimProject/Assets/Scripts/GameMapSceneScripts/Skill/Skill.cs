using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Skill Data")]
public class Skill : ScriptableObject //: MonoBehaviour
{

    [SerializeField]
    private  string skillName;

    [SerializeField]
    private  int skillCost;

    [SerializeField]
    private int skillCD;

    [SerializeField]
    private  int skillTilesRange;

    [SerializeField]
    private  int modifyHP;

    [SerializeField]
    private float modifyHPScaling;

    [SerializeField]
    private  int modifyMP;

    [SerializeField]
    private  int modifyATK;

    [SerializeField]
    private  int modifyDEF;

    [SerializeField]
    private  int modifyFCS;

    [SerializeField]
    private  int modifyMOB;

    [SerializeField]
    private  int modifyTurnsLeft;

    [SerializeField]
    private string modifyStatus;

    [SerializeField]
    private string flavorText;

    [SerializeField]
    private bool isAggressive;

    [SerializeField]
    private bool selfUse;

    [SerializeField]
     private GameObject animationModel; 

    public Skill()
    {
       this.skillName="";
       this.skillCost=0;
       this.skillTilesRange=0;
       this.modifyHP=0;
       this.modifyMP=0;
       this.modifyATK=0;
       this.modifyDEF=0;
       this.modifyFCS=0;
       this.modifyMOB=0;
       this.modifyTurnsLeft=0;
       this.modifyStatus="";
       this.flavorText="";
       this.isAggressive=true;
       this.skillCD=0;

    }


//**Might put those here instead of Skill Handler since it's not a displayable object**// 

public void setName(string skillName) { this.skillName = skillName; }
public string getName() { return this.skillName; }
public void setCost(int skillCost) { this.skillCost = skillCost; }
public int getCost() { return this.skillCost; }
public void setRange(int skillTilesRange) {  this.skillTilesRange =skillTilesRange; }
public int getRange() { return this.skillTilesRange; }
public void setHPmod(int modifyHP) { this.modifyHP = modifyHP; }
public int getHPmod() { return this.modifyHP; }
public void setMPmod(int modifyMP) { this.modifyMP = modifyMP; }
public int getMPmod() { return this.modifyMP; }
public void setATKmod(int modifyATK) { this.modifyATK = modifyATK; }
public int getATKmod() { return this.modifyATK; }
public void setDEFmod(int modifyDEF) { this.modifyDEF = modifyDEF; }
public int getDEFmod() { return this.modifyDEF; }
public void setFCSmod(int modifyFCS) { this.modifyFCS = modifyFCS; }
public int getFCSmod() { return this.modifyFCS; }
public void setMOBmod(int modifyMOB) { this.modifyMOB = modifyMOB; }
public int getMOBmod() { return this.modifyMOB; }
public void setTurnsLeftMod(int modifyTurnsLeft) { this.modifyTurnsLeft = modifyTurnsLeft; }
public int getTurnsLeftMod() { return this.modifyTurnsLeft; }
public void setStatusMod(string modifyStatus) { this.modifyStatus = modifyStatus; }
public string getStatusMod() { return this.modifyStatus; }
public void setFlavorText(string flavorText) { this.flavorText = flavorText; }
public string getFlavorText() { return this.flavorText; }
public void setIsAggressive(bool isAggressive ) { this.isAggressive = isAggressive; }
public bool getIsAggressive() { return this.isAggressive; }
public void setSkillCD(int skillCD) { this.skillCD = skillCD; }
public int getSkillCD() { return this.skillCD; }
public bool getSelfUse() { return selfUse; }
public float getDamageScaling() { return this.modifyHPScaling; }

}