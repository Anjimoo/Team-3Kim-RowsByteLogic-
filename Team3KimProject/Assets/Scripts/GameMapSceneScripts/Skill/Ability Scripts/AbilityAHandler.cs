using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityAHandler : RangeHandler
{
    public bool selectedToUseAbility = false;
    private Skill[] abilityList; //List<Skill> abilityList;
    public int selectedAbility;
    public Skill currentlyUsingSkill;
    [SerializeField]
    GameObject AbilityPrefab;

    public void Awake()
    {
        abilityList = this.gameObject.GetComponent<Unit>().getAbilityList();
    }


    // Update is called once per frame
    void Update()
    {


        if (selectedToUseAbility)
        {
            currentlyUsingSkill = this.gameObject.GetComponent<Unit>().getAbilityList()[selectedAbility];
            if (!currentlyUsingSkill.getSelfUse())
            {
                FindAbilityRange(abilityList[selectedAbility]);
                CheckMouse();
            }
            else
            {
                GameObject.Find("InvokeAbility").GetComponent<AbilityInvoker>().selfUse(gameObject.GetComponent<Unit>(),selectedAbility+1);
                

            }
        }
        else
        {
            RemoveSelectableTiles();
        }
    }


    public void FindAbilityRange(Skill skill)//send selected ability specified by user from list of unit abilitys
    {
        FindSelectableTiles(skill.getRange());//send abilitys range to FindSelectable Tiles to see range on map.
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
                    if (Physics.Raycast(hit.collider.gameObject.transform.position, Vector3.down, out hit1, 1))
                    {
                        if (hit1.collider.GetComponent<Tile>().selectable == true && !hit1.collider.GetComponent<Tile>().current)
                        {
                            UseSelectedability(hit.collider.gameObject);
                        }
                    }
                }
            }
        }

    }

    public void UseSelectedability(GameObject target) 

    {
        GameObject.Find("InvokeAbility").GetComponent<AbilityInvoker>().InvokeAbility(GameObject.Find("TurnsHandler").GetComponent<TurnsHandler>().unitsInQueue.Peek().GetComponent<Unit>(), target.GetComponent<Unit>(), selectedAbility + 1); // need to find abilitying unit
        AbilityPrefab = Instantiate(AbilityPrefab, this.gameObject.transform.position, Quaternion.identity);
        Destroy(AbilityPrefab, 1.5f);
        Debug.Log("My HP is = " + target.GetComponent<Unit>().getCurrentHP() + "/" + target.GetComponent<Unit>().getMaxHP());
    }

}
