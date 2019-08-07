using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Material[] materials;

    public bool walkable;
    public bool current = false;
    public bool target = false;
    public bool selectable = false;

    public List<Tile> adjacencyList = new List<Tile>();

    //Needed BFS
    public bool visited = false;
    public Tile parent = null;
    public int distance = 0;


    void Update()
    {
        if (current)
        {
            GetComponent<Renderer>().material = materials[0];
        }else if (target)
        {
            GetComponent<Renderer>().material = materials[1];
        }else if (selectable)
        {
            GetComponent<Renderer>().material = materials[2];
        }
        else
        {
            GetComponent<Renderer>().material = materials[3];
        }
    }

    public void Reset()
    {
        adjacencyList.Clear();
        current = false;
        target = false;
        selectable = false;

        visited = false;
        parent = null;
        distance = 0;
    }

    public void FindNeighbors()
    {
        Reset();

        CheckTile(Vector3.forward);
        CheckTile(-Vector3.forward);
        CheckTile(Vector3.right);
        CheckTile(-Vector3.right);
    }

    public void CheckTile(Vector3 direction)
    {
        Vector3 halfExtents = new Vector3(0.25f,(1) / 2.0f,0.25f);
        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents);
        GameObject peekObject= GameObject.Find("TurnsHandler").GetComponent<TurnsHandler>().unitsInQueue.Peek(); 

        foreach(Collider item in colliders)
        {
            Tile tile = item.GetComponent<Tile>();
            if(tile != null && tile.walkable)
            {
                RaycastHit hit;
                if (!Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1))
                {
                    adjacencyList.Add(tile);
                }
                else
                {
                    if (peekObject.GetComponent<AttackAHandler>().selectedToAttack && Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1))//If first unit in queue is selectedToAttack and raycas from tile is hitting something
                    {
                        if ((hit.collider.gameObject != peekObject) && hit.collider.gameObject.tag != peekObject.tag)//if object on tile is not first unit in queue and his tag is different from first unit in queue
                        {
                            adjacencyList.Add(tile);
                        }
                    }
                    if (peekObject.GetComponent<AbilityAHandler>().selectedToUseAbility && Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1))//If first unit in queue is selectedToAttack and raycas from tile is hitting something
                    {
                        if ((hit.collider.gameObject != peekObject) && (hit.collider.gameObject.tag != peekObject.tag)&&(peekObject.GetComponent<AbilityAHandler>().currentlyUsingSkill.getIsAggressive()))//if object on tile is not first unit in queue and his tag is different from first unit in queue
                        {
                            adjacencyList.Add(tile);
                        }
                        if ( hit.collider.gameObject.tag == peekObject.tag && !(peekObject.GetComponent<AbilityAHandler>().currentlyUsingSkill.getIsAggressive()))//if object on tile is not first unit in queue and his tag is the same as unit in queue
                        {
                            adjacencyList.Add(tile);
                        }
                    }
                }

                
            }
        }
    }



}
