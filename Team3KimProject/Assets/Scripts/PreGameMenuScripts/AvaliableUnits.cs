using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvaliableUnits : MonoBehaviour
{
    public Text[] avaUnitNames;
    public GameObject[] avaUnits; 

    // Start is called before the first frame update
    void Start()
    {
        //avaUnits[0].GetComponent<Unit>().getUnitName();
        for(int i=0;i<avaUnitNames.Length;i++)
            avaUnitNames[i].text= avaUnits[i].GetComponent<Unit>().getUnitName();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
