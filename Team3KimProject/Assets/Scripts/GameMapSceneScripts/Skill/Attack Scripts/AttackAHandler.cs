using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAHandler : RangeHandler
{
    
    public bool selectedToAttack = false;
    [SerializeField]
    private Skill[] attackList; //List<Skill> attackList;
    public int selectedAttack;
    GameObject currentTarget;
    public Quaternion currentRotation;
    Vector3 unPos;

    public delegate void ShootingProjectile(GameObject unit,GameObject target,int selectedAttack);
    public static event ShootingProjectile OnShoot;

    public void Awake()
    {
        attackList = this.gameObject.GetComponent<Unit>().getAttackList();
    }


    // Update is called once per frame
    void Update()
    {
       

        if (selectedToAttack)
        {
            FindAttackRange(attackList[selectedAttack]);
            CheckMouse();
        }
        else
        {
            RemoveSelectableTiles();
        }
    }

    public Skill getCurrentAttack()
    { return attackList[selectedAttack]; }

    public void FindAttackRange(Skill skill)//send selected attack specified by user from list of unit attacks
    {
        FindSelectableTiles(skill.getRange());//send attacks range to FindSelectable Tiles to see range on map.
    }

    void CheckMouse()
    {


        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            RaycastHit hit1;

            if (Physics.Raycast(ray, out hit))
            {
                if ((hit.collider.tag == "Player1" || hit.collider.tag == "Player2"))
                {
                    if(Physics.Raycast(hit.collider.gameObject.transform.position, Vector3.down, out hit1, 1))
                    {
                        if (hit1.collider.GetComponent<Tile>().selectable == true && !hit1.collider.GetComponent<Tile>().current)
                        {                            
                            //UseSelectedAttack(hit.collider.gameObject);
                            this.currentTarget = hit.collider.gameObject;
                            selectedToAttack = false;
                            GameObject.Find("InvokeAttack").GetComponent<AttackInvoker>().unit2 = hit.collider.gameObject.GetComponent<Unit>();
                            GameObject.Find("GUIHandlerV2").GetComponent<GUIHandlerv2>().OnCounterActions(hit.collider.gameObject);
                            
                        }
                    }
                }
            }
        }

    }

    public void UseSelectedAttack(int counterAction)//GameObject target)

    {    //GameObject.Find("TurnsHandler").GetComponent<TurnsHandler>().unitsInQueue.Peek() 
         //****Insted of using GameObject.Find and finding object on Peek just send this.gameObject to InvokeAttackOnce because you AttackAHandler attached to unit and he is using this attack****
        currentRotation = this.gameObject.transform.rotation;
        GameObject target = this.currentTarget;
        Vector3 direction = currentTarget.transform.position - this.gameObject.transform.position;
        this.gameObject.transform.rotation = Quaternion.LookRotation(direction);
        GameObject.Find("InvokeAttack").GetComponent<AttackInvoker>().ChooseCounterAction(counterAction);
        GameObject.Find("GUIHandlerV2").GetComponent<GUIHandlerv2>().attacked = true;
        GameObject.Find("GUIHandlerV2").GetComponent<GUIHandlerv2>().busy = false;

        //peekObject.GetComponent<AttackAHandler>().selectedToAttack = false;            

        GameObject.Find("GUIHandlerV2").GetComponent<GUIHandlerv2>().OnCounterActionUIStatesChange();
        
        //GameObject.Find("InvokeAttack").GetComponent<AttackInvoker>().InvokeAttackOnce(this.gameObject.GetComponent<Unit>(),target.GetComponent<Unit>(),selectedAttack+1); // need to find attacking unit
       
        //Debug.Log("My HP is = " + target.GetComponent<Unit>().getCurrentHP()+"/"+target.GetComponent<Unit>().getMaxHP());

        if (OnShoot != null)
        {
            OnShoot(this.gameObject,currentTarget,selectedAttack);
            unPos = GameObject.Find("TurnsHandler").GetComponent<TurnsHandler>().GetUnitOnPeek().transform.position; //camera vector to change 
            unPos += new Vector3(0, 8, -7); //makes sure the Y is in proper height
            Invoke(nameof(revertCam), 2.5f); //reverts the cam's zoom back after 2.5 sec          
        }
    }

    public void InvokeAttack()
    {
        GameObject.Find("InvokeAttack").GetComponent<AttackInvoker>().InvokeAttackOnce(this.gameObject.GetComponent<Unit>(), currentTarget.GetComponent<Unit>(), selectedAttack + 1);
    }


    void revertCam()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().fieldOfView = GameObject.Find("GUIHandlerV2").GetComponent<GUIHandlerv2>().mcFoVtemp;
        GameObject.FindGameObjectWithTag("MainCamera").transform.position = unPos;
    }
}
