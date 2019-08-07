using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;


public class GameInfo : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject a, b;
    int currentp1, currentp2;
    void Start()
    {
        currentp1 = GameObject.FindGameObjectsWithTag("Player1").Length;
        currentp2 = GameObject.FindGameObjectsWithTag("Player2").Length;

        Debug.Log("Number of Player 1 Units: " + currentp1);
        Debug.Log("Number of Player 2 Units: " + currentp2);

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Player1").Length != currentp1)
        {
            Debug.Log("Number of Player 1 Units: " + GameObject.FindGameObjectsWithTag("Player1").Length);
            currentp1 = GameObject.FindGameObjectsWithTag("Player1").Length;
        }

        if (GameObject.FindGameObjectsWithTag("Player2").Length != currentp2)
        {
            Debug.Log("Number of Player 2 Units: " + GameObject.FindGameObjectsWithTag("Player2").Length);
            currentp2 = GameObject.FindGameObjectsWithTag("Player2").Length;
        }

        if (currentp1 == 0)
            OnP2Win();


        if (currentp2 == 0)
            OnP1Win();



    }
    void OnP1Win() { a.SetActive(true); Invoke(nameof(OnEndGame), 7); }
    void OnP2Win() { b.SetActive(true); Invoke(nameof(OnEndGame), 7); }
    void OnEndGame() { SceneManager.LoadScene(1); }
}
