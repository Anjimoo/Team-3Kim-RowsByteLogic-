using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : RangeHandler
{   

    Stack<Tile> path = new Stack<Tile>();
    public bool moving = false;
    public float moveSpeed = 2;

    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3();


    public void MoveToTile(Tile tile)
    {
        path.Clear();
        tile.target = true;
        moving = true;
        
        Tile next = tile;
        while(next!= null)
        {
            path.Push(next);
            next = next.parent;

            
        }
        
    }

    public bool Move()
    {
        if(path.Count > 0)
        {
            Tile t = path.Peek();
            Vector3 target = t.transform.position;

            target.y += halfHeight + t.GetComponent<Collider>().bounds.extents.y;
            
            if(Vector3.Distance(transform.position, target) >= 0.05f)
            {
                CalculateHeading(target);
                SetHorizontalVelocity();

                transform.forward = heading;
                transform.position += velocity * Time.deltaTime;
                GameObject.Find("GUIHandlerV2").GetComponent<GUIHandlerv2>().busy = true;
                GameObject.Find("GUIHandlerV2").GetComponent<GUIHandlerv2>().moved = true;
            }
            else
            {
                transform.position = target;
                path.Pop();
                GameObject.Find("GUIHandlerV2").GetComponent<GUIHandlerv2>().busy = false;
                
            }
        }
        else
        {
            RemoveSelectableTiles();
            moving = false;
            
        }
        return false;
    }

    void CalculateHeading(Vector3 target)
    {
        heading = target - transform.position;
        heading.Normalize();
    }

    void SetHorizontalVelocity()
    {
        velocity = heading * moveSpeed;

    }



}