using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class UnitSpawner : MonoBehaviour
{
    [SerializeField]    
    UnitSpawnInfo unitsToSpawnData;
    [SerializeField]
    MapGeneratorInfo mapData;

    HashSet<int> randomNumbers1 = new HashSet<int>();
    HashSet<int> randomNumbers2 = new HashSet<int>();
    private static readonly System.Random random = new System.Random();
    private int xCoordinate;
    private int zCoordinate;
    private bool isPlayer1;
    //public void GetUnits() //Get units from Preload
    //{
    //    this.unitPrefab = GameObject.Find("/PreGameMenu/UnitListGenerator").GetComponent<UnitListGenerator>().GenerateList();

    //}

    private void Start()
    {
        //GetUnits(); //Disabled for now

        if (unitsToSpawnData.Player1units != null && unitsToSpawnData.Player1units != null)
        {
            for(int i = 0; i < unitsToSpawnData.Player1units.Length; i++)
            {
                Spawn(isPlayer1 = true, unitsToSpawnData.Player1units[i]);
                Spawn(isPlayer1 = false, unitsToSpawnData.Player2units[i]);
            }
            
        }
    }
   

    private void Spawn(bool isPlayer,GameObject myunit)
    {
        
        if(isPlayer1)
        {
            myunit.tag = "Player1";
            xCoordinate = random.Next(4);
            while (!randomNumbers1.Add(zCoordinate = random.Next(mapData.mapSizeX))) ;
            GameObject unit = Instantiate(myunit, (new Vector3((float)xCoordinate, 1.25f, (float)zCoordinate)), myunit.transform.rotation);
            GameObject.Find("TurnsHandler").GetComponent<TurnsHandler>().firstPlayerQueue.Enqueue(unit);
            
        }
        else
        {
            myunit.tag = "Player2";

            xCoordinate = random.Next(4) + (mapData.mapSizeX-4);
            while (!randomNumbers2.Add(zCoordinate = random.Next(mapData.mapSizeX))) ;
            GameObject unit = Instantiate(myunit, (new Vector3((float)xCoordinate, 1.25f, (float)zCoordinate)), myunit.transform.rotation);
            GameObject.Find("TurnsHandler").GetComponent<TurnsHandler>().secondPlayerQueue.Enqueue(unit);
        }
       
        
        
    }
    
    


}
