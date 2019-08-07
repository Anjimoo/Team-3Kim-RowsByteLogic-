using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class IngamePauseMenu : MonoBehaviour
{
    
    public Button closeGame;
 
    // Start is called before the first frame update
    void Start()
    {
        closeGame.onClick.AddListener(OnCloseGameClick);
    }


    void OnCloseGameClick() {
     SceneManager.LoadScene(1);
        
    }

}
