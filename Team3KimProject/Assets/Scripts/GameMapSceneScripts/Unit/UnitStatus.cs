using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitStatus : MonoBehaviour
{
    [SerializeField]
    private Image bar; //hp and mp  bars 
    [SerializeField]
    private Image playerIndicator;

    private Camera _mycamera;
    [SerializeField]
    private Canvas unitCanvas;
    // Start is called before the first frame update
    void Start()
    {
        _mycamera =  GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); //Camera.main;
        if (this.gameObject.tag == "Player1")
        {
            playerIndicator.color = new Color32(0, 255, 125, 135);
        }
        else if (this.gameObject.tag == "Player2")
        {
            playerIndicator.color = new Color32(0, 255, 255, 135);
        }
    }

    // Update is called once per frame
    void Update()
    {
        bar.fillAmount = (float)this.gameObject.GetComponent<Unit>().getCurrentHP() / (float)this.gameObject.GetComponent<Unit>().getMaxHP();
        
        unitCanvas.transform.LookAt(unitCanvas.transform.position + _mycamera.transform.rotation * Vector3.back, _mycamera.transform.rotation * Vector3.down);    
    }
}
