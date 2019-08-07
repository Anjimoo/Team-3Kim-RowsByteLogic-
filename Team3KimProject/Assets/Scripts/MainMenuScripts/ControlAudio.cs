using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlAudio : MonoBehaviour
{
   public  GameObject au;

    // Start is called before the first frame update
    void Start()
    {
        au = GameObject.Find("App");
        gameObject.GetComponent<Slider>().value = 0.4f;
        au.GetComponent<AudioManagerNeo>().musicVol = gameObject.GetComponent<Slider>();
  
    }

    // Update is called once per frame
    void Update()
    {
        if(au==null) { Debug.Log("FIX!"); }
        else
        au.GetComponent<AudioManagerNeo>().music.volume = au.GetComponent<AudioManagerNeo>().musicVol.value; //gameObject.GetComponent<Slider>().value;
    }
}
