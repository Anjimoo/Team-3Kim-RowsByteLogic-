using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnsHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject turnsInfo;

    [SerializeField]
    private Text turnText;

    public delegate void ChangeText();
    public static event ChangeText OnSwitchTurn;

    public Queue<GameObject> firstPlayerQueue = new Queue<GameObject>(); 
    public Queue<GameObject> secondPlayerQueue = new Queue<GameObject>();
    public Queue<GameObject> unitsInQueue = new Queue<GameObject>();
    private void OnDisable()
    {
        firstPlayerQueue.Clear();
        secondPlayerQueue.Clear();
        unitsInQueue.Clear();
    }
    private void Start()
    {      
        unitsInQueue.Enqueue(firstPlayerQueue.Peek());
        turnsInfo.SetActive(true);
        SwitchTurn();
    }
    private void Update()
    {
        if(firstPlayerQueue.Peek() == null && unitsInQueue.Peek() == null)
        {            
            firstPlayerQueue.Dequeue();
            unitsInQueue.Enqueue(firstPlayerQueue.Peek());
            unitsInQueue.Dequeue();
            SwitchTurn();
        }
        if(secondPlayerQueue.Peek() == null && unitsInQueue.Peek() == null)
        {
            secondPlayerQueue.Dequeue();
            unitsInQueue.Enqueue(secondPlayerQueue.Peek());
            unitsInQueue.Dequeue();
            SwitchTurn();
            
        }
        //this.gameObject.GetComponentInChildren<Text>().text = unitsInQueue.Peek().gameObject.tag + " turn";
        
    }

    public void Enquing(GameObject gameObject)
    {
        if (gameObject == firstPlayerQueue.Peek())
        {
            firstPlayerQueue.Dequeue();
            unitsInQueue.Dequeue();
            firstPlayerQueue.Enqueue(gameObject);
            
            while (secondPlayerQueue.Peek() == null)
            {
                secondPlayerQueue.Dequeue();  
            }
            unitsInQueue.Enqueue(secondPlayerQueue.Peek());
            SwitchTurn();
        }
        else if (gameObject == secondPlayerQueue.Peek())
        {
            secondPlayerQueue.Dequeue();
            unitsInQueue.Dequeue();
            secondPlayerQueue.Enqueue(gameObject);
            while (firstPlayerQueue.Peek() == null)
            {
                firstPlayerQueue.Dequeue();
            }
            unitsInQueue.Enqueue(firstPlayerQueue.Peek());
            SwitchTurn();
        }
        //unitsInQueue.Dequeue();
        //unitsInQueue.Enqueue(gameObject);
        ////unitsInQueue.Peek().GetComponent<Unit>().circulateCooldowns();
 

    }
    public bool CheckQueue(GameObject gameObject)
    {
        if (gameObject == unitsInQueue.Peek())
        {
            return true;
        } 
        else
        {
            return false;
        }

    }
    private void SwitchTurn()
    {
        if (OnSwitchTurn != null)
        {
            OnSwitchTurn();
        }
    }

    public GameObject GetUnitOnPeek()
    {
        return unitsInQueue.Peek();
    }

}
