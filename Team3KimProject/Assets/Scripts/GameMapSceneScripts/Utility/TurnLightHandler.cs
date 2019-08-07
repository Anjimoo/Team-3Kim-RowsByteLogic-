using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnLightHandler : MonoBehaviour
{
    private bool isInPosition;
    private GameObject instance;
    [SerializeField]
    private GameObject firstPlayerLight;
    [SerializeField]
    private GameObject secondPlayerLight;

    private void Start()
    {
        firstPlayerLight = Instantiate(firstPlayerLight, new Vector3(-20, -20, -20), Quaternion.identity);
        firstPlayerLight.SetActive(true);
        secondPlayerLight = Instantiate(secondPlayerLight, new Vector3(-20, -20, -20), Quaternion.identity);
        secondPlayerLight.SetActive(false);

    }
    private void Update()
    {
        if(GameObject.Find("TurnsHandler").GetComponent<TurnsHandler>().unitsInQueue.Peek().tag == "Player1")
        {
            secondPlayerLight.SetActive(false);
            firstPlayerLight.SetActive(true);
            PositionCheck(firstPlayerLight);
        }
        else
        {
            firstPlayerLight.SetActive(false);
            secondPlayerLight.SetActive(true);
            PositionCheck(secondPlayerLight);
        }
        


        //else if(instance.transform.position.x != GameObject.Find("TurnsHandler").GetComponent<TurnsHandler>().unitsInQueue.Peek().transform.position.x || instance.transform.position.z != GameObject.Find("TurnsHandler").GetComponent<TurnsHandler>().unitsInQueue.Peek().transform.position.z)
        //{
        //    isInPosition = false;
        //}
        
    }
    private void PositionCheck(GameObject light)
    {
        if (!isInPosition)
        {
            light.gameObject.transform.position = new Vector3(GameObject.Find("TurnsHandler").GetComponent<TurnsHandler>().unitsInQueue.Peek().transform.position.x, 0.75f, GameObject.Find("TurnsHandler").GetComponent<TurnsHandler>().unitsInQueue.Peek().transform.position.z);
            isInPosition = true;
        }
        else if (GameObject.Find("TurnsHandler").GetComponent<TurnsHandler>().unitsInQueue.Peek() != null)
        {
            if (light.transform.position.x != GameObject.Find("TurnsHandler").GetComponent<TurnsHandler>().unitsInQueue.Peek().transform.position.x || light.transform.position.z != GameObject.Find("TurnsHandler").GetComponent<TurnsHandler>().unitsInQueue.Peek().transform.position.z)
            {
                isInPosition = false;
            }
        }
    }
}
