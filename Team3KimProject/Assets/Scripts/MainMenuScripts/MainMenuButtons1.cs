using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class MainMenuButtons1 : MonoBehaviour
{

    public Button button1;
    public Button button2;
    public Button button3;
    //Options and about
    public Button buttonAbout;
    public GameObject optionsMenu, aboutUs, mmButtons;
    public Button closeOptions, closeAbout;


    private void Start()
    {
        button1.onClick.AddListener(OnNewGameClick);
        button2.onClick.AddListener(OnOptionsClick);
        button3.onClick.AddListener(OnExitClick);
        buttonAbout.onClick.AddListener(OnAboutClick);
        closeOptions.onClick.AddListener(CloseOptionsButton);
        closeAbout.onClick.AddListener(CloseAboutClick);
        optionsMenu.SetActive(false);
        aboutUs.SetActive(false);
    }
        

    void OnNewGameClick() { SceneManager.LoadScene(2); }
    void OnOptionsClick() { optionsMenu.SetActive(true); mmButtons.SetActive(false);  }
    void OnExitClick() { Application.Quit(); }
    void CloseOptionsButton() { optionsMenu.SetActive(false); mmButtons.SetActive(true); }
    void OnAboutClick() { aboutUs.SetActive(true); mmButtons.SetActive(false); }
    void CloseAboutClick() { aboutUs.SetActive(false); mmButtons.SetActive(true); }
}