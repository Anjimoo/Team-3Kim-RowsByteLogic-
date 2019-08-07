using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class TestOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject button;
    public GameObject object1;
    private bool isOver = false;

    void Start()
    {
        if (object1 != null && button != null)
        {
            button.gameObject.SetActive(false);
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (object1 != null && button != null)
            {
                if (!object1.GetComponent<PregameGUIHandler>().getIfBusy()) //Can't touch if another menu is open. Also getting rid of some errors.
                {
                    button.gameObject.SetActive(true);
                    Debug.Log("Mouse enter");
                    isOver = true;
                }
            }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (object1 != null&&button!=null)
        {
            button.gameObject.SetActive(false);
            Debug.Log("Mouse exit");
            isOver = false;
        }
    }
}