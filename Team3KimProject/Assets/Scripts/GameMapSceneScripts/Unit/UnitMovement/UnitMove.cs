using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitMove : MovementHandler
{
    public bool selected = false;
    public bool clickAble = true;
    private GameObject popUpWindow;
    float radius, radiusUpdate;
    double tilesNum;


    private int findTilesNum()
    {
        int counter = 0;
        foreach (GameObject t in GameObject.FindGameObjectsWithTag("Tile"))
        {
            counter++;
        }
        return counter;
    }
    
    void Start()
    {        
        Init();
        tilesNum = Math.Sqrt(findTilesNum())*2;
        radiusUpdate = this.gameObject.GetComponent<Unit>().mobility;
        radius = radiusUpdate / (float)tilesNum;
    }

    
    void Update()
    {
        if (this.gameObject.GetComponent<Unit>().mobility != radiusUpdate)
        {
            radiusUpdate = this.gameObject.GetComponent<Unit>().mobility;
            radius = radiusUpdate / (float)tilesNum;
        }


        if (!moving && selected)
            {
                
                FindSelectableTiles((int)radius);
                CheckMouse();
            
            }
            else
            {
                selected = Move();
                
            }
        
    }

    

    void CheckMouse()
    {
        

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Tile")
                    {
                        Tile t = hit.collider.GetComponent<Tile>();
                        if (t.selectable)
                        {
                            MoveToTile(t);

                        }
                    }
                }
            }
        
    }

}
