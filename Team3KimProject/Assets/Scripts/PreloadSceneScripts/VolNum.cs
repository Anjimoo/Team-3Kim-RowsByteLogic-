using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolNum : MonoBehaviour
{
    public float volNum;

    // Start is called before the first frame update
    void Start()
    {
        this.volNum = PlayerPrefs.GetFloat("MusicVolume");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}