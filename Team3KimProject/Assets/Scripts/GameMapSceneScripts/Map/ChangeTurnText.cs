using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTurnText : MonoBehaviour
{
    bool togglePlayers=false;
    [SerializeField]
    GameObject menu1, menu2, menu3, menu4;
    [SerializeField]
    GameObject camera;
    [SerializeField]
    GameObject start;
    public bool dontFocus;

    public void Start()
    {
        start.SetActive(true);
        GameObject.Find("GUIHandlerV2").GetComponent<GUIHandlerv2>().busy = true; //can't move until we remove text
        Invoke(nameof(DelayedStart), 1);
        Invoke(nameof(RemoveText), 2);
    }

    private void RemoveText()
    {
        start.SetActive(false);
        GameObject.Find("GUIHandlerV2").GetComponent<GUIHandlerv2>().busy = false;
    }

    private void DelayedStart()
    {
        Vector3 caPos = GameObject.Find("TurnsHandler").GetComponent<TurnsHandler>().GetUnitOnPeek().transform.position; //camera's position
        caPos.y = 8;
        caPos.z -= 6;
        camera.transform.position = caPos; //camera start position

    }

    private void OnEnable()
    {
        TurnsHandler.OnSwitchTurn += OnPlayersToggle1;
    }
    private void OnDisable()
    {
        TurnsHandler.OnSwitchTurn -= OnPlayersToggle1;
    }

    //private void ChangeMyText()
    //{

    //    this.gameObject.GetComponent<Text>().text = GameObject.Find("TurnsHandler").GetComponent<TurnsHandler>().GetUnitOnPeek().tag + " turn";
    //}

    private void OnPlayersToggle1()
    {
        togglePlayers = !togglePlayers;
        if (togglePlayers)
        {
            this.gameObject.GetComponent<Text>().text = "User 1";
            this.gameObject.GetComponentInParent<Image>().color = new Color32(0, 255, 125, 135);
            changeMenus();
        }
        if (!togglePlayers)
        {
            this.gameObject.GetComponent<Text>().text = "User 2";
            this.gameObject.GetComponentInParent<Image>().color = new Color32(0, 255, 255, 135);
            changeMenus();
        }
        Vector3 caPos = GameObject.Find("TurnsHandler").GetComponent<TurnsHandler>().GetUnitOnPeek().transform.position; //camera's position
        caPos.y = 8; 
        caPos.z -= 6;

        if (!dontFocus) //if not currently in actions turn menu - only after "wait" click
            camera.transform.position = caPos;
    }

    public void OnPlayersToggle()
    { OnPlayersToggle1(); } 

    public void changeMenus()
    {
        Color32 col;
        if (togglePlayers)
        col = new Color32(175, 220, 125, 130);
        else
        col = new Color32(255, 255, 255, 130);

        menu1.GetComponent<Image>().color = col;
        menu2.GetComponent<Image>().color = col;
        menu3.GetComponent<Image>().color = col;
        menu4.GetComponent<Image>().color = col;
    }
}
